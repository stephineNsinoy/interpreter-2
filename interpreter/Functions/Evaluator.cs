using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Tree;
using interpreter.Grammar;
using interpreter.Variables;

namespace interpreter
{
    public class Evaluator
    {
        /************************************
              EVALUATION FOR DELIMITERS
        *************************************/

        /// <summary>
        /// Evaluates delimiters
        /// </summary>
        public static bool EvaluateDelimiter(CodeParser.LineBlockContext context)
        {
            string beginDelimiter = "BEGIN CODE";
            string endDelimiter = "END CODE";
            string? beginCode = context.BEGIN_CODE()?.GetText();
            string? endCode = context.END_CODE()?.GetText();

            //Both delimiters are present in the code
            if (beginCode != null && endCode != null && beginCode.Equals(beginDelimiter) && endCode.Equals(endDelimiter))
            {
                // Visit the declarations and lines only if the delimiters are at the beginning and end of the program
                if (context.declaration().Length == 0 && context.line().Length == 0)
                {
                    Console.WriteLine("CODE not recognized \n" +
                                       "Cannot be executed");
                    Environment.Exit(1);
                }

                // Declaration contains BEGIN CODE
                if (context.declaration().Length > 0 && context.declaration().Any(d => d.GetText().Contains(beginDelimiter)))
                {
                    Console.WriteLine($"{beginDelimiter} must only be at the beginning of the program");
                    Environment.Exit(1);
                }
                //IEnumerable<string> lines = context.line().Select(l => l.GetText());
                //foreach (var line in context.line())
                //{
                //    Console.WriteLine(line.GetText());
                //}

                // Line contains BEGIN CODE
                if (context.line().Length > 0 && context.line().Any(l => l.GetText().Contains(beginDelimiter)))
                {
                    Console.WriteLine($"{beginDelimiter} must be only at the beginning of the program");
                    Environment.Exit(1);
                }

                //_isBeginCodeVisited = true;

                // NOT WORKING
                // Declaration contains END CODE
                if (context.declaration().Length > 0 && context.declaration().Any(d => d.GetText().Contains(endDelimiter) && d.GetText().IndexOf(endDelimiter) > 0))
                {
                    Console.WriteLine($"{endDelimiter} must be only at the end of the program");
                    Environment.Exit(1);
                }

                // Line contains END CODE
                if (context.line().Length > 0 && context.line().Any(l => l.GetText().Contains(endDelimiter) && l.GetText().IndexOf(endDelimiter) < l.GetText().Length - endDelimiter.Length))
                {
                    Console.WriteLine($"{endDelimiter} must be only at the end of the program");
                    Environment.Exit(1);
                }

                return true;
            }
            else if ((beginCode == null || !beginCode.Equals(beginDelimiter)) && (endCode != null && endCode.Equals(endDelimiter)))
            {
                // Only the end delimiter is present
                Console.WriteLine("Missing BEGIN CODE delimiter");
                Environment.Exit(1);
            }

            else if ((beginCode != null && beginCode.Equals(beginDelimiter)) && (endCode == null || !endCode.Equals(endDelimiter)))
            {
                // Only the begin delimiter is present
                Console.WriteLine("Missing END CODE delimiter");
                Environment.Exit(1);
            }

            else
            {
                // Neither delimiter is present
                Console.WriteLine("Missing delimiters");
                Environment.Exit(1);
            }


            return false;
        }

        /// <summary>
        /// Checks if statements are placed after BEGIN CODE
        /// </summary>
        public static void EvaluateAfterBeginCodeStatements(bool isBeginCodeVisited)
        {
            if (isBeginCodeVisited)
            {
                Console.WriteLine("Statements must be placed after BEGIN CODE");
                Environment.Exit(1);
            }
        }


        /************************************
              EVALUATION FOR DATA TYPES
        *************************************/

        /// <summary>
        /// GOODS
        /// Evaluates if value conforms to data type
        /// </summary>
        private static bool EvaluateDataType(string dataType, object? value)
        {
            if (value == null)
            {
                return true; // Null can be assigned to any data type
            }

            return dataType switch
            {
                "INT" => value is int,
                "FLOAT" => value is float,
                "BOOL" => value is bool,
                "STRING" => value is string,
                "CHAR" => value is char,
                _ => false,// Invalid data type
            };
        }


        /************************************
              EVALUATION FOR DECLARATION
        *************************************/

        /// <summary>
        /// Evaluates declaration
        /// TODO:
        ///     Needs more testing for other cases
        /// </summary>
        public static void EvaluateDeclaration(string dataType, string[] varNames, object? value)
        {
            bool isValid = true;

            foreach (var name in varNames)
            {
                // Checks if only data type is specified no identifier
                if (name == "<missing IDENTIFIER>")
                {
                    Console.WriteLine("Missing IDENTIFIER!");
                    Environment.Exit(1);
                }

                // Checks if value assigned corresponds to data type
                if (!EvaluateDataType(dataType, value))
                {
                    Console.WriteLine($"Invalid value assigned to variable {name}");
                    isValid = false;
                }
            }
            if (!isValid)
            {
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// GOODS:
        ///     INT  y=, -> ERROR
        ///     INT test =  -> ERROR
        /// TODO:
        ///     this statement must be error:
        ///             INT hehe = 3
        ///             INT y= 2, z = 2, r= 
        ///             DISPLAY: hehe          
        /// </summary>
        public static void EvaluateDeclarationContent(List<VariableDeclaration> varDeclarations, List<IParseTree> content)
        {
            var secondValue = content[1].GetText();
            var lastValue = content[content.Count() - 1].GetText();

            if (varDeclarations.Count == 0 || secondValue != varDeclarations[0].Name ||
                string.IsNullOrWhiteSpace(lastValue) || lastValue == "," || lastValue == "=")
            {
                Console.WriteLine("Invalid declaration statement");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Checks if there is no value in declaring a variable
        /// TODO:
        ///     Needs more testing for other cases
        /// </summary>
        public static void EvaluateIsValuePresent(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression) || expression == "," || expression == "=")
            {
                Console.WriteLine("Value is missing");
                Environment.Exit(1);
            }
        }

        ///// <summary>
        ///// GOODS
        ///// Checks if variable is redeclared
        ///// </summary>
        public static void EvaluateIsVariableReDeclared(List<VariableDeclaration> varDeclarations, Dictionary<string, object?> Variable)
        {
            var redeclaredVars = varDeclarations.Select(x => x.Name).Where(x => Variable.ContainsKey(x!)).ToList();

            if (redeclaredVars.Any())
            {
                Console.WriteLine($"Variable {string.Join(" - ", redeclaredVars)} is already declared");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Checks if declaration is valid with expression already
        /// TODO:
        ///     Include other unknown characters
        ///     INT test=2, haha=haha -> ERROR
        ///     INT test=2, haha=test -> NOT ERROR
        ///     INT test =2, , haha=3 -> ERROR
        /// </summary>
        public static void EvaluateIsDeclarationValid(string content)
        {
            if (content == "=")
            {
                Console.WriteLine("Invalid declaration statement");
                Environment.Exit(1);
            }
        }

        /************************************
              EVALUATION FOR ASSIGNMENT
        *************************************/

        /// <summary>
        /// Checks if assignment is incomplete, no expression
        /// </summary>
        public static void EvaluateAssignment(string[] varNameArray, object? value)
        {
            if (value == null)
            {
                string variables = string.Join("=", varNameArray);
                Console.WriteLine($"Incorrect statement {variables}=?");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// GOODS
        /// Checks if variable is not defined
        /// </summary>
        public static void EvaluateIsVariableDefined(string varName, Dictionary<string, object?> Variable)
        {
            if (!Variable.ContainsKey(varName))
            {
                Console.WriteLine($"Variable {varName} is not defined");
                Environment.Exit(1);
            }
        }


        /************************************
              EVALUATION FOR FUNCTIONS
        *************************************/

        /// <summary>
        /// Checks if function is not defined or if variable is not a function
        /// </summary>
        public static void EvaluateIsFunctionDefined(string name, Dictionary<string, object?> Variable)
        {
            if (!Variable.ContainsKey(name))
            {
                Console.WriteLine($"Function {name} is not defined.");
                Environment.Exit(1);
            }

            if (Variable[name] is not Func<object?[], object?> func)
            {
                Console.WriteLine($"Variable {name} is not a function.");
                Environment.Exit(1);
            }
        }

        /************************************
              EVALUATION FOR STRING TO BOOLEAN
        *************************************/
        
        /// <summary>
        /// Parses the string TRUE and FALSE to their corresponding 
        /// boolean values
        /// </summary>
        public static bool EvaluateBool(string str)
        {
            return str.Equals("\"TRUE\"");
        }
    }
}

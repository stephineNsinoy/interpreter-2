using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Dfa;
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
        public static bool EvaluateCodeDelimiter(CodeParser.LineBlockContext context)
        {
            string beginDelimiter = "BEGIN CODE";
            string endDelimiter = "END CODE";
            string? beginCode = context.BEGIN_CODE()?.GetText();
            string? endCode = context.END_CODE()?.GetText();
            string missingBeginCode = "<missing BEGIN_CODE>";
            string missingEndCode = "<missing END_CODE>";

            //Both delimiters are present in the code
            if (beginCode != null && endCode != null && beginCode != missingBeginCode && endCode != missingEndCode)
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
                    Console.WriteLine($"BEGIN CODE must only be at the beginning of the program");
                    Environment.Exit(1);
                }

                //_isBeginCodeVisited = true;

                // Declaration contains END CODE
                if (context.declaration().Length > 0 && context.declaration().Any(d => d.GetText().Contains(endDelimiter) && d.GetText().IndexOf(endDelimiter) > 0))
                {
                    Console.WriteLine($"END CODE must be only at the end of the program");
                    Environment.Exit(1);
                }

                return true;
            }
            else if ((beginCode == null || beginCode == missingBeginCode) && (endCode != null || endCode != missingEndCode))
            {
                // Only the end delimiter is present
                Console.WriteLine("Missing BEGIN CODE delimiter");
                Environment.Exit(1);
            }

            else if ((beginCode != null || beginCode != missingBeginCode) && (endCode == null || endCode == missingEndCode))
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
        /// Evaluates the delimiters in the IF block
        /// </summary>
        public static bool EvaluateIfBlockDelimiters(CodeParser.IfBlockContext context)
        {
            string beginIfDelimiter = "BEGIN IF";
            string endIfDelimiter = "END IF";

            string? beginIf = context.BEGIN_IF()?.GetText();
            string? endIf = context.END_IF()?.GetText();

            if ((beginIf == null || !beginIf.Equals(beginIfDelimiter)) && (endIf != null && endIf.Equals(endIfDelimiter)))
            {
                Console.WriteLine("Missing BEGIN IF delimiter in IF BLOCK");
                Environment.Exit(1);
            }

            else if ((beginIf != null && beginIf.Equals(beginIfDelimiter)) && (endIf == null || !endIf.Equals(endIfDelimiter)))
            {
                Console.WriteLine("Missing END IF delimiter in IF BLOCK");
                Environment.Exit(1);
            }

            else if (beginIf == null && endIf == null)
            {
                Console.WriteLine("Missing BEGIN IF and END IF delimiters in IF BLOCK");
                Environment.Exit(1);
            }
            return false;
        }

        /// <summary>
        /// Evaluates the delimiter in the ELSE IF block
        /// </summary>
        public static bool EvaluateElseIfBlockDelimiters(CodeParser.ElseIfBlockContext context)
        {
            string beginIfDelimiter = "BEGIN IF";
            string endIfDelimiter = "END IF";

            string? beginIf = context.BEGIN_IF()?.GetText();
            string? endIf = context.END_IF()?.GetText();

            if ((beginIf == null || !beginIf.Equals(beginIfDelimiter)) && (endIf != null && endIf.Equals(endIfDelimiter)))
            {
                Console.WriteLine("Missing BEGIN IF delimiter in IF ELSE BLOCK");
                Environment.Exit(1);
            }

            else if ((beginIf != null && beginIf.Equals(beginIfDelimiter)) && (endIf == null || !endIf.Equals(endIfDelimiter)))
            {
                Console.WriteLine("Missing END IF delimiter in IF ELSE BLOCK");
                Environment.Exit(1);
            }

            else if (beginIf == null && endIf == null)
            {
                Console.WriteLine("Missing BEGIN IF and END IF delimiters in IF ELSE BLOCK");
                Environment.Exit(1);
            }
            return false;
        }

        /// <summary>
        /// Evaluates the delimiter in the ELSE block
        /// </summary>
        public static bool EvaluateElseBlockDelimiters(CodeParser.ElseBlockContext context)
        {
            string beginIfDelimiter = "BEGIN IF";
            string endIfDelimiter = "END IF";

            string? beginIf = context.BEGIN_IF()?.GetText();
            string? endIf = context.END_IF()?.GetText();

            if ((beginIf == null || !beginIf.Equals(beginIfDelimiter)) && (endIf != null && endIf.Equals(endIfDelimiter)))
            {
                Console.WriteLine("Missing BEGIN IF delimiter in ELSE BLOCK");
                Environment.Exit(1);
            }

            else if ((beginIf != null && beginIf.Equals(beginIfDelimiter)) && (endIf == null || !endIf.Equals(endIfDelimiter)))
            {
                Console.WriteLine("Missing END IF delimiter in ELSE BLOCK");
                Environment.Exit(1);
            }

            else if (beginIf == null && endIf == null)
            {
                Console.WriteLine("Missing BEGIN IF and END IF delimiters in ELSE BLOCK");
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

            if (varDeclarations.Count == 0 || secondValue != varDeclarations[0].Name 
                || lastValue == "," || lastValue == "=" || lastValue == "<missing NEWLINE>")
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

        /************************************
               EVALUATION WHILE LOOP
        *************************************/

        // IN-PROGRESS
        public static void EvaluateWhileDelimiter(CodeParser.WhileBlockContext context)
        {
            string beginDelimiter = "BEGIN WHILE";
            string endDelimiter = "END WHILE";
            string? beginWhile = context.BEGIN_WHILE()?.GetText();
            string? endWhile = context.END_WHILE()?.GetText();

            //Both delimiters are present in the code
            if (beginWhile != null && endWhile != null && beginWhile.Equals(beginDelimiter) && endWhile.Equals(endDelimiter))
            {
                // Empty while loop
                if (context.line().Length == 0)
                {
                    Console.WriteLine("Missing WHILE loop content");
                    Environment.Exit(1);
                }

                //// Line contains BEGIN WHILE or END WHILE
                //foreach (var line in context.line())
                //{
                //    var lineText = line.GetText();
                //    if (lineText.Contains(beginDelimiter) && !lineText.Trim().Equals(beginDelimiter))
                //    {
                //        Console.WriteLine($"{beginDelimiter} must be only at the beginning of the WHILE loop");
                //        Environment.Exit(1);
                //    }
                //    else if (lineText.Contains(endDelimiter) && !lineText.Trim().Equals(endDelimiter))
                //    {
                //        Console.WriteLine($"{endDelimiter} must be only at the end of the WHILE loop");
                //        Environment.Exit(1);
                //    }
                //}

                
                // NOT WORKING
                // Line contains BEGIN WHILE more than once
                if (context.line().Count(l => l.GetText().Contains(beginDelimiter)) > 1)
                {
                    Console.WriteLine($"{beginDelimiter} can only be used once in a WHILE loop");
                    Environment.Exit(1);
                }

                // Line contains END WHILE more than once
                if (context.line().Count(l => l.GetText().Contains(endDelimiter) && l.GetText().IndexOf(endDelimiter) < l.GetText().Length - endDelimiter.Length) > 1)
                {
                    Console.WriteLine($"{endDelimiter} can only be used once in a WHILE loop");
                    Environment.Exit(1);
                }

                // Line contains END WHILE before BEGIN WHILE
                if (context.line().Any(l => l.GetText().Contains(endDelimiter) && l.GetText().IndexOf(endDelimiter) < l.GetText().IndexOf(beginDelimiter)))
                {
                    Console.WriteLine($"{beginDelimiter} must be used before {endDelimiter} in a WHILE loop");
                    Environment.Exit(1);
                }
            }


            // NOT ALWAYS WORKING because if BEGIN WHILE is missing, statements inside WHILE will be not recognized
            else if ((beginWhile != null && beginWhile.Equals(beginDelimiter)) && (endWhile == null || !endWhile.Equals(endDelimiter)))
            {
                // Only the begin delimiter is present
                Console.WriteLine("Missing END WHILE delimiter");
                Environment.Exit(1);
            }

            else if ((beginWhile == null || !beginWhile.Equals(beginDelimiter)) && (endWhile != null && endWhile.Equals(endDelimiter)))
            {
                // Only the end delimiter is present
                Console.WriteLine("Missing BEGIN WHILE delimiter");
                Environment.Exit(1);
            }

            else
            {
                // NOT WORKING
                // Check for infinite loops
                bool hasContent = context.line().Any(l => !string.IsNullOrWhiteSpace(l.GetText()));
                if (!hasContent)
                {
                    Console.WriteLine("Infinite loop detected");
                    Environment.Exit(1);
                }


                // Neither delimiter is present or BEGIN WHILE is missing
                Console.WriteLine("Missing WHILE delimiters");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Evaluates if the IF BLOCK is present
        /// </summary>
        public static bool? EvaluateIsIfBLockPresent(bool isIfBlockPresent)
        {
            if (isIfBlockPresent == false)
            {
                Console.WriteLine("Missing IF Block");
                Environment.Exit(1);
                return null;
            }
            return null;
        } 

        /// <summary>
        /// Evaluates if the condition is null
        /// </summary>
        public static bool? EvaluateConditon(bool? condition)
        {
            if(condition == null)
            {
                Console.WriteLine("Condition cannot be null");
                Environment.Exit(1);
                return null;
            }
            return null;
        }

        /// <summary>
        /// Evaluates the else block if it is present
        /// </summary>
        public static bool? EvaluateElseBlock(CodeParser.ElseBlockContext context)
        {
            if(context == null)
            {
                Console.WriteLine("Error: No ELSE block found");
                Environment.Exit(1);
            }
            return null;
        }


        // IN-PROGRESS
        public static void EvaluateNewLine(string content)
        {
            if (content.Contains("<missing NEWLINE>"))
            {
                Console.WriteLine("Every line must contain only one statement");
                Environment.Exit(1);
            }
        }

        // IN-PROGRESS
        public static void EvaluateNotValidDeclaration<T>(T context) where T : ParserRuleContext
        {
            bool hasInt = context.GetText().Contains("INT");
            bool hasChar = context.GetText().Contains("CHAR");
            bool hasBool = context.GetText().Contains("BOOL");
            bool hasString = context.GetText().Contains("STRING");
            bool hasFloat = context.GetText().Contains("FLOAT");

            if (hasInt || hasBool || hasChar || hasString || hasFloat)
            {
                Console.WriteLine("Declaration must only be placed after BEGIN CODE");
                Environment.Exit(1);
            }
        }
    }
}

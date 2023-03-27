using interpreter.Grammar;

namespace interpreter.Functions.Evaluators
{
    internal class Delimiter
    {
        /************************************
              EVALUATION FOR DELIMITERS
        *************************************/

        /// <summary>
        /// Evaluates BEGIN CODE and END CODE delimiters
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
                    Console.WriteLine("SYNTAX ERROR: CODE not recognized - Cannot be executed");
                    Environment.Exit(1);
                }

                // Declaration contains BEGIN CODE
                if (context.declaration().Length > 0 && context.declaration().Any(d => d.GetText().Contains(beginDelimiter)))
                {
                    Console.WriteLine($"SYNTAX ERROR: BEGIN CODE must only be at the beginning of the program");
                    Environment.Exit(1);
                }

                //_isBeginCodeVisited = true;

                // Declaration contains END CODE
                if (context.declaration().Length > 0 && context.declaration().Any(d => d.GetText().Contains(endDelimiter) && d.GetText().IndexOf(endDelimiter) > 0))
                {
                    Console.WriteLine($" SYNTAX ERROR:END CODE must be only at the end of the program");
                    Environment.Exit(1);
                }

                return true;
            }
            else if ((beginCode == null || beginCode == missingBeginCode) && (endCode != null || endCode != missingEndCode))
            {
                // Only the end delimiter is present
                Console.WriteLine("SYNTAX ERROR - MISSING: BEGIN CODE delimiter");
                Environment.Exit(1);
            }

            else if ((beginCode != null || beginCode != missingBeginCode) && (endCode == null || endCode == missingEndCode))
            {
                // Only the begin delimiter is present
                Console.WriteLine("SYNTAX ERROR - MISSING: END CODE delimiter");
                Environment.Exit(1);
            }

            else
            {
                // Neither delimiter is present
                Console.WriteLine("SYNTAX ERROR - MISSING: BEGIN CODE and END CODE delimiters");
                Environment.Exit(1);
            }

            return false;
        }

        /// <summary>
        /// Checks if statements are placed after BEGIN CODE
        /// CURRENTLY NOT WORKING
        /// </summary>
        public static void EvaluateAfterBeginCodeStatements(bool isBeginCodeVisited)
        {
            if (isBeginCodeVisited)
            {
                Console.WriteLine("SYNTAX ERROR: Statements must be placed after BEGIN CODE");
                Environment.Exit(1);
            }
        }

 
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
                    Console.WriteLine("SYNTAX ERROR - MISSING: WHILE loop content");
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
                    Console.WriteLine($"SYNTAX ERROR: BEGIN WHILE can only be used once in a WHILE loop");
                    Environment.Exit(1);
                }

                // Line contains END WHILE more than once
                if (context.line().Count(l => l.GetText().Contains(endDelimiter) && l.GetText().IndexOf(endDelimiter) < l.GetText().Length - endDelimiter.Length) > 1)
                {
                    Console.WriteLine($"SYNTAX ERROR: END WHILE can only be used once in a WHILE loop");
                    Environment.Exit(1);
                }

                // Line contains END WHILE before BEGIN WHILE
                if (context.line().Any(l => l.GetText().Contains(endDelimiter) && l.GetText().IndexOf(endDelimiter) < l.GetText().IndexOf(beginDelimiter)))
                {
                    Console.WriteLine($"SYNTAX ERROR: BEGIN WHILE must be used before END WHILE in a WHILE loop");
                    Environment.Exit(1);
                }
            }


            // NOT ALWAYS WORKING because if BEGIN WHILE is missing, statements inside WHILE will be not recognized
            else if ((beginWhile != null && beginWhile.Equals(beginDelimiter)) && (endWhile == null || !endWhile.Equals(endDelimiter)))
            {
                // Only the begin delimiter is present
                Console.WriteLine("SYNTAX ERROR - MISSING: END WHILE delimiter");
                Environment.Exit(1);
            }

            else if ((beginWhile == null || !beginWhile.Equals(beginDelimiter)) && (endWhile != null && endWhile.Equals(endDelimiter)))
            {
                // Only the end delimiter is present
                Console.WriteLine("SYNTAX ERROR - MISSING: BEGIN WHILE delimiter");
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
                Console.WriteLine("SYNTAX ERROR - MISSING: WHILE delimiters");
                Environment.Exit(1);
            }
        }

    }
}

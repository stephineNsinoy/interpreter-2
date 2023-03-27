using interpreter.Grammar;

namespace interpreter.Functions.Evaluators
{
    internal class Delimiter
    {
        /************************************
              EVALUATION FOR DELIMITERS
        *************************************/

        /// <summary>
        /// Evaluates BEGIN CODE and END CODE delimeters for program
        /// </summary>
        public static void EvaluateProgramDelimiter(CodeParser.ProgramContext context)
        {
            var content = context.GetText();
            var start = context.start.Text;
            var end = context.stop.Text;
            string beginDelimiter = "BEGIN CODE";
            string endDelimiter = "END CODE";
            string EOF = "<EOF>";
            int beginCodeCount = content.Split("BEGIN CODE").Length - 1;
            int endCodeCount = content.Split("END CODE").Length - 1;

            // more than 1 BEGIN CODE
            if (beginCodeCount > 1 && endCodeCount == 1)
            {
                Console.WriteLine($"SYNTAX ERROR: {beginCodeCount} BEGIN CODE delimeter found");
                Environment.Exit(1);
            }

            // More than 1 END CODE
            else if (beginCodeCount == 1 && endCodeCount > 1)
            {
                Console.WriteLine($"SYNTAX ERROR: {endCodeCount} END CODE delimeter found");
                Environment.Exit(1);
            }

            // More than 1 BEGIN CODE and END CODE
            else if (beginCodeCount > 1 && endCodeCount > 1)
            {
                Console.WriteLine("SYNTAX ERROR: Multiple CODE delimeters found");
                Environment.Exit(1);
            }

            // Starting delimeter is not BEGIN CODE
            if (!(start.Equals(beginDelimiter) || (start.Contains("\r\n") && start.Contains(beginDelimiter))))
            {
                Console.WriteLine("SYNTAX ERROR: Invalid starting delimeter");
                Environment.Exit(1);
            }

            // Ending delimeter is not END CODE
            if (!(end.Equals(endDelimiter) || end.Equals(EOF) || end.Contains("\r\n")))
            {
                Console.WriteLine("SYNTAX ERROR: Invalid ending delimeter");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Evaluates BEGIN CODE and END CODE delimiters in lineBlock
        /// </summary>
        public static bool EvaluateCodeDelimiter(CodeParser.LineBlockContext context)
        {
            string beginDelimiter = "BEGIN CODE";
            string? beginCode = context.BEGIN_CODE()?.GetText();
            string? endCode = context.END_CODE()?.GetText();
            string missingBeginCode = "<missing BEGIN_CODE>";
            string missingEndCode = "<missing END_CODE>";
            string start = context.start.Text;

            if (!(start.Equals(beginDelimiter) || (start.Contains("\r\n") && start.Contains(beginDelimiter))))
            {
                Console.WriteLine("SYNTAX ERROR: Invalid starting delimeter");
                Environment.Exit(1);
            }

            // Both delimiters are present in the code
            if (beginCode != null && endCode != null && beginCode != missingBeginCode && endCode != missingEndCode)
            {
                // Visit the declarations and lines only if the delimiters are at the beginning and end of the program
                if (context.declaration().Length == 0 && context.line().Length == 0)
                {
                    Console.WriteLine("SYNTAX ERROR: CODE not recognized - Cannot be executed");
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

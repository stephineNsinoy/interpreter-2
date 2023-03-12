using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpreter
{
    public class Evaluator
    {
        public bool CheckDataType(string dataType, object? value)
        {
            if (value == null)
            {
                return true; // Null can be assigned to any data type
            }

            switch (dataType)
            {
                case "INT":
                    return value is int;
                case "FLOAT":
                    return value is float;
                case "BOOL":
                    return value is bool;
                case "STRING":
                    return value is string;
                case "CHAR":
                    return value is char;
                default:
                    return false; // Invalid data type
            }
        }

        public void CheckDeclaration(string dataType, string[] varNames, object? value)
        {
            bool isValid = true;

            foreach (var name in varNames)
            {
                if (!CheckDataType(dataType, value))
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
    }
}

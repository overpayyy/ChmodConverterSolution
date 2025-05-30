using System;
using ChmodConverterLib;

namespace ChmodConverterConsole
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: chmodconverter <permissions>");
                Console.WriteLine("Example: chmodconverter rwxr-xr-x");
                Console.WriteLine("Example: chmodconverter 755");
                return 1;
            }

            string input = args[0];

            try
            {
                if (IsSymbolicMode(input))
                {
                    string numeric = ChmodConverter.SymbolicToNumeric(input);
                    Console.WriteLine(numeric);
                    return 0;
                }
                else if (IsNumericMode(input))
                {
                    string symbolic = ChmodConverter.NumericToSymbolic(input);
                    Console.WriteLine(symbolic);
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error: Invalid input format. Use symbolic (e.g., rwxr-xr-x) or numeric (e.g., 755) mode.");
                    return 1;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 1;
            }
        }

        private static bool IsSymbolicMode(string input)
        {
            if (input.Length != 9)
                return false;

            for (int i = 0; i < 9; i += 3)
            {
                if (!((input[i] == 'r' || input[i] == '-') &&
                      (input[i + 1] == 'w' || input[i + 1] == '-') &&
                      (input[i + 2] == 'x' || input[i + 2] == '-')))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsNumericMode(string input)
        {
            if (input.Length != 3)
                return false;

            return input.All(c => char.IsDigit(c) && c >= '0' && c <= '7');
        }
    }
}
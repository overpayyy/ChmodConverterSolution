namespace ChmodConverterLib
{
    public static class ChmodConverter
    {
        public static string SymbolicToNumeric(string symbolic)
        {
            if (string.IsNullOrEmpty(symbolic))
                throw new ArgumentException("Symbolic mode cannot be null or empty.");
            if (symbolic.Length != 9)
                throw new ArgumentException("Symbolic mode must be 9 characters long.");

            for (int i = 0; i < 9; i += 3)
            {
                if (!IsValidPermissionSet(symbolic[i], symbolic[i + 1], symbolic[i + 2]))
                    throw new ArgumentException("Invalid symbolic mode characters.");
            }

            string result = string.Empty;
            for (int i = 0; i < 9; i += 3)
            {
                int value = 0;
                if (symbolic[i] == 'r') value += 4;
                if (symbolic[i + 1] == 'w') value += 2;
                if (symbolic[i + 2] == 'x') value += 1;
                result += value.ToString();
            }

            return result;
        }

        public static string NumericToSymbolic(string numeric)
        {
            if (string.IsNullOrEmpty(numeric))
                throw new ArgumentException("Numeric mode cannot be null or empty.");
            if (numeric.Length != 3)
                throw new ArgumentException("Numeric mode must be 3 digits long.");

            if (!numeric.All(c => char.IsDigit(c) && c >= '0' && c <= '7'))
                throw new ArgumentException("Numeric mode must contain digits from 0 to 7.");

            string result = string.Empty;
            foreach (char digit in numeric)
            {
                int value = int.Parse(digit.ToString());
                result += (value & 4) == 4 ? "r" : "-";
                result += (value & 2) == 2 ? "w" : "-";
                result += (value & 1) == 1 ? "x" : "-";
            }

            return result;
        }

        private static bool IsValidPermissionSet(char read, char write, char execute)
        {
            return (read == 'r' || read == '-') &&
                   (write == 'w' || write == '-') &&
                   (execute == 'x' || execute == '-');
        }
    }
}
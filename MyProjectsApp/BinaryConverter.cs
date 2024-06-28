using System;

public class BinaryConverter
{
    public BinaryConverter()
    {
    }

    public static double GetDecimalFromBinary(string binaryInput)
    {
        double totalValue = 0;

        int inputLength = binaryInput.Length;

        for (int i = 0; i < inputLength; i++)
        {
            double currentBinaryDigit = Convert.ToDouble(binaryInput[i].ToString());

            if (currentBinaryDigit == 0)
                continue;

            double actualDigitValue = Math.Pow(2, Math.Abs(7 - i));

            totalValue += actualDigitValue;
        }

        return totalValue;
    }
}

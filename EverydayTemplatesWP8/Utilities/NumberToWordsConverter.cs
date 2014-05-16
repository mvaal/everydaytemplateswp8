using BugSense;
using BugSense.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverydayTemplatesWP8.Utilities
{
    public class NumberToWordsConverter
    {
        private static String[] numbers = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        private static String[] tens = { "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninty" };
        private static String[] suffixes = { "Hundred", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion", "Undecillion", "Duodecillion", "Tredecillion", "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septdecillion", "Octodecillion", "Novemdecillion", "Vigintillion" };
        private static String[] decimals = { "", "Tenths", "Hundredths", "Thousandths", "Ten-Thousandths", "Hundred-Thousandths", "Millionths", "Ten-Millionths"};

        private static void SplitNumber(double number, out double wholePart, out double decimalPart) {
            wholePart = 0;
            decimalPart = 0;

            if (number == 0)
            {
                return;
            }

            try
            {
                string strNumber = number.ToString();
                string[] split = strNumber.Split('.');
                wholePart = double.Parse(split[0]);
                decimalPart = double.Parse(split[1]);
            }
            catch
            {
                wholePart = number;
            }
        }

        public static string ConvertToMoney(double number)
        {
            if (number == 0)
            {
                return numbers[0];
            }

            double wholePart;
            double decimalPart;

            SplitNumber(Math.Round(number, 2), out wholePart, out decimalPart);

            string result = "[Unable To Convert]";
            try
            {
                StringBuilder sb = new StringBuilder(NumWords(wholePart));

                if (sb.Length > 0)
                {
                    sb.Append(" and ");
                }
                
                sb.Append((int)decimalPart).Append("/").Append("100");
                result = sb.ToString();
            }
            catch (Exception ex)
            {
                LimitedCrashExtraDataList extraDataList = new LimitedCrashExtraDataList
                {
                    new CrashExtraData("Number", number.ToString())
                };
                BugSenseLogResult bsResult = BugSenseHandler.Instance.LogException(ex, extraDataList);
                Debug.WriteLine("Client Request: {0}", bsResult.ClientRequest);
            }

            return result;
        }

        public static string ConvertToDecimal(double number)
        {
            if (number == 0)
            {
                return numbers[0];
            }

            double wholePart;
            double decimalPart;

            SplitNumber(number, out wholePart, out decimalPart);

            string result = "[Unable To Convert]";
            try
            {
                StringBuilder sb = new StringBuilder(NumWords(wholePart));

                if (sb.Length > 0)
                {
                    sb.Append(" and ");
                }
                sb.Append(NumWords(decimalPart)).Append(" ").Append(decimals[decimalPart.ToString().Length]);
                result = sb.ToString();
            }
            catch (Exception ex)
            {
                LimitedCrashExtraDataList extraDataList = new LimitedCrashExtraDataList
                {
                    new CrashExtraData("Number", number.ToString())
                };
                BugSenseLogResult bsResult = BugSenseHandler.Instance.LogException(ex, extraDataList);
                Debug.WriteLine("Client Request: {0}", bsResult.ClientRequest);
            }

            return result;
        }

        public static string NumWords(double number)
        {
            StringBuilder sb = new StringBuilder();
           
            if (number < 0)
            {
                sb.Append("negative ");
                number *= -1;
            }

            int power = suffixes.Length * 3;
            while (power >= 3)
            {
                double pow = Math.Pow(10, power);
                if (number >= pow)
                {
                    sb.Append(NumWords(Math.Floor(number / pow))).Append(" ").Append(suffixes[(power / 3)]);
                    if (number % pow > 0)
                    {
                        sb.Append(", ");
                    }
                    number %= pow;
                }
                power -= 3;
            }
            if (0 <= number && number <= 999)
            {
                if ((int)number / 100 > 0)
                {
                    sb.Append(NumWords(Math.Floor(number / 100))).Append(" ").Append(suffixes[0]);
                    
                    number %= 100;
                }
                if ((int)number / 10 > 1)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                    }
                    sb.Append(tens[(int)number / 10 - 2]);
                    number %= 10;
                }

                if (number < 20 && number > 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                    }
                    sb.Append(numbers[(int)number]);
                    number -= Math.Floor(number);
                }
            }

            return sb.ToString();
        }

    }
}

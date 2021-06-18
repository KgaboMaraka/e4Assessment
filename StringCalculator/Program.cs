using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Program
    {
        public static string negativeNumbers = string.Empty;
        static void Main(string[] args)
        {
        }

        public static int Add(string numbers)
        {
            try
            {
                int sum = 0;
                if (string.IsNullOrWhiteSpace(numbers))
                {
                    Console.WriteLine(0);
                    return 0;
                }
                else
                {
                    string delimiter = string.Empty;
                    if (numbers.Length > 1)
                    {
                        if (numbers.Substring(0, 2) == "//")
                        {
                            delimiter = numbers.Substring(2, 1);
                            numbers = numbers.Remove(0, 3);
                        }
                        else
                        {
                            string pattern = @"-?[0-9]+";

                            var delimiters = Regex
                              .Split(numbers, pattern)
                              .Select(item => item.Trim())
                              .Where(item => !string.IsNullOrEmpty(item))
                              .Distinct()
                              .ToArray();

                            if (delimiters.Count() != 0)
                            {
                                delimiter = delimiters[0];
                                numbers = numbers.Replace(Environment.NewLine, delimiter);
                            }
                            else
                            {
                                if (numbers.Contains(Environment.NewLine))
                                {
                                    delimiter = ",";
                                    numbers = numbers.Replace(Environment.NewLine, delimiter);
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(delimiter))
                    {
                        var stringArray = numbers.Split(char.Parse(delimiter));

                        var negatives = Array.FindAll(stringArray, s => s.StartsWith("-"));
                        if (negatives.Count() > 0)
                        {
                            sum = -1;
                            negativeNumbers = string.Join(",", negatives);
                        }
                        else
                        {
                            sum = stringArray.Sum(int.Parse);
                        }
                    }
                    else
                    {
                        sum = int.Parse(numbers);
                    }

                    return sum;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string AddCaller(string numbers)
        {
            int result = Add(numbers);
            return result == -1 ? new NegativesNotAllowedException(negativeNumbers).ToString() : result.ToString();
        }
    }

    class NegativesNotAllowedException : Exception
    {
        public NegativesNotAllowedException(string negatives) : base(String.Format("negatives not allowed: {0}", negatives))
        {
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace e4Assessment
{
    class Program
    {
        static void Main(string[] args)
        {
            Add("-1,2,3");
            Add("//;" + Environment.NewLine + "4;3;6");
            Add("1" + Environment.NewLine + "4" + Environment.NewLine + "3");
            Add("8");
            Add("3$5$9");
            Add("1,5" + Environment.NewLine + "4" + Environment.NewLine + "3");
            Add("");
        }

        public static int Add(string numbers)
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
                else
                {
                    if (!int.TryParse(numbers, out _))
                    {
                        Console.WriteLine("Invalid string!");
                        return 0;
                    }
                }

                if (!string.IsNullOrWhiteSpace(delimiter))
                {
                    var stringArray = numbers.Split(char.Parse(delimiter));

                    var negatives = Array.FindAll(stringArray, s => s.StartsWith("-"));
                    if (negatives.Count() > 0)
                    {
                        Console.WriteLine(new NegativesNotAllowedException(string.Join(",", negatives)));
                    }
                    else
                    {
                        sum = stringArray.Sum(int.Parse);
                        Console.WriteLine(sum);
                    }
                }
                else
                {
                    sum = int.Parse(numbers);
                    Console.WriteLine(numbers);
                }

                return sum;
            }
        }
    }

    class NegativesNotAllowedException : Exception
    {
        public NegativesNotAllowedException(string negatives) : base(String.Format("negatives not allowed: {0}", negatives))
        {
        }
    }
}

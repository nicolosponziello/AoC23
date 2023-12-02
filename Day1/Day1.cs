using System.Text.RegularExpressions;

namespace AoC23;

class Day1
{
    private static string[] NumbersInWord = new string[] {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
    private static string[] NumbersInChars = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    
    public static void Main(String[] args)
    {
        var lines = File.ReadAllLines("input.txt");

        //part 1
        var sum = 0;

        foreach (var line in lines)
        {
            var calibrationValue = Int16.Parse(string.Concat(line.First(Char.IsNumber), line.Last(Char.IsNumber)));
            sum += calibrationValue;
        }

        Console.WriteLine(sum);
        
        //part 2
        var sum2 = 0;
        //lines = new string[] { "tdszrfzspthree2ttzseven5seven" };
        foreach (var line in lines)
        {
            var calibrationValue = ScanStringForNumberOrWord(line);
            sum2 += calibrationValue;
        }
        
        Console.WriteLine(sum2);
    }

    private static int ScanStringForNumberOrWord(string line)
    {
        var numbers = new List<(string, int)>();
        var numbersInWord = new List<(string, int)>();

        numbers = FindNumberWord(line, NumbersInChars).OrderBy(x => x.Item2).ToList();
        numbersInWord = FindNumberWord(line, NumbersInWord).OrderBy(x => x.Item2).ToList();

        var firstDigit = numbersInWord.Any() is false || numbers.First().Item2 < numbersInWord.First().Item2
            ? numbers.First().Item1
            : numbersInWord.First().Item1;

        var lastDigit = numbersInWord.Any() is false || numbers.Last().Item2 > numbersInWord.Last().Item2
            ? numbers.Last().Item1
            : numbersInWord.Last().Item1;
        
        var value = Int16.Parse(string.Concat(firstDigit, lastDigit));
        return value;
    }

    private static List<(string, int)> FindNumberWord(string line, string[] source)
    {
        var occurrencies = new List<(string, int)>();
        foreach (var word in source)
        {
            if (line.Contains(word))
            {
                var allIndexs = Regex.Matches(line, word).Cast<Match>().Select(m => m.Index).ToList();
                foreach (var index in allIndexs)
                {
                    occurrencies.Add((ConvertWordIntoNumber(word), index));
                }
            }
        }

        return occurrencies;
    }

    private static string ConvertWordIntoNumber(string word)
    {
        switch (word)
        {
            case "one": return "1";
            case "two": return "2";
            case "three" : return "3";
            case "four" : return "4";
            case "five" : return "5";
            case "six" : return "6";
            case "seven" : return "7";
            case "eight" : return "8";
            case "nine" : return "9";
            default: return word;
        }
    }
}
using System;
using System.IO;

using System.Threading.Tasks;

var filePath = "Day1/input.txt";
var input = await File.ReadAllTextAsync(filePath);

var lines = input.Split("\n");
var numbers = new List<int>();

var matchers = new Dictionary<string, string> {
    { "1", "1" },
    {"2", "2"},
    {"3", "3"},
    {"4", "4"},
    {"5", "5"},
    {"6", "6"},
    {"7", "7"},
    {"8", "8"},
    {"9", "9"},
    {"one", "1"},
    {"two", "2"},
    {"three", "3"},
    {"four", "4"},
    {"five", "5"},
    {"six", "6"},
    {"seven", "7"},
    {"eight", "8"},
    {"nine", "9"},
    {"ten", "0"},
    { "eleven", "11" },
    { "twelve", "12"},
    { "thirteen", "13"},
    { "fourteen", "14" },
    { "fifteen", "15" },
    { "sixteen", "16" },
    { "seventeen", "17" },
    { "eighteen", "18" },
    { "nineteen", "19" },
    { "twenty", "20" },
    { "thirty", "30" },
    { "forty", "40" },
    { "fifty", "50" },
    { "sixty", "60" },
    { "seventy", "70" },
    { "eighty", "80" },
    { "ninety", "90" },
    { "hundred", "100" },
    { "thousand", "1000" }
};

foreach (var line in lines)
{
    var firstIndex = int.MaxValue;
    var lastIndex = -1;

    string firstValue = null;
    string lastValue = null;
    foreach (var matcher in matchers)
    {
        var firstMatchIndex = line.IndexOf(matcher.Key);
        var lastMatchIndex = line.LastIndexOf(matcher.Key);

        var matchFirst = matcher.Value;
        var matchLast = matcher.Value;


        if (matcher.Value.Length > 1)
        {
            matchFirst = matcher.Value.Substring(0, 1);
            matchLast = matcher.Value[matcher.Value.Length - 1].ToString();
        }

        if (firstMatchIndex > -1 && firstMatchIndex < firstIndex)
        {
            firstIndex = firstMatchIndex;
            firstValue = matchFirst;
        }

        if (lastMatchIndex > -1 && lastMatchIndex > lastIndex)
        {
            lastIndex = lastMatchIndex;
            lastValue = matchLast;
        }
    }


    var number = int.Parse($"{firstValue}{lastValue}");
    numbers.Add(number);
}

Console.WriteLine(numbers.Sum());

var test = false;

var filePath = test ? "Day3/test.txt" : "Day3/input.txt";

var input = await File.ReadAllTextAsync(filePath);

var lines = input.Trim().Split("\n");

var matrix = lines.Select(line => line.ToCharArray()).ToArray();

var symbols = new List<char> { '@', '#', '$', '%', '&', '*', '/', '+', '-', '=' };

class Number
{
    public int Value { get; set; }
    public int Row { get; set; }
    public int ColumnStart { get; set; }
    public int ColumnEnd { get; set; }
}


var currentNumber = "";
var numbers = new List<Number>();


for (var i = 0; i < matrix.Length; i++)
{
    for (var j = 0; j < matrix[i].Length; j++)
    {
        if (char.IsDigit(matrix[i][j])) currentNumber += matrix[i][j];
        else
        {
            if (!string.IsNullOrEmpty(currentNumber))
            {
                numbers.Add(new Number
                {
                    Value = int.Parse(currentNumber),
                    Row = i,
                    ColumnStart = j - currentNumber.Length,
                    ColumnEnd = j - 1
                });
            };
            currentNumber = "";
        }
    }
    if (!string.IsNullOrEmpty(currentNumber))
    {
        numbers.Add(new Number
        {
            Value = int.Parse(currentNumber),
            Row = i,
            ColumnStart = matrix[i].Length - currentNumber.Length,
            ColumnEnd = matrix[i].Length - 1
        });
    }
}

var partNumbers = new List<int>();

foreach (var number in numbers)
{
    var isPartNumber = false;

    var surroundingChars = new List<char>();
    // Add top row
    var topRow = number.Row - 1;
    var topLine = "";
    if (topRow >= 0)
    {
        var start = Math.Max(0, number.ColumnStart - 1);
        var end = Math.Min(matrix[topRow].Length - 1, number.ColumnEnd + 1);

        var top = matrix[topRow][start..(end + 1)];
        topLine = new string(top);
        surroundingChars.AddRange(top);
    }

    // Check sides
    var left = number.ColumnStart - 1;
    var leftLine = "";
    if (left >= 0)
    {
        surroundingChars.Add(matrix[number.Row][left]);
        leftLine = matrix[number.Row][left].ToString();
    }

    var right = number.ColumnEnd + 1;
    var rightLine = "";
    if (right < matrix[number.Row].Length)
    {
        surroundingChars.Add(matrix[number.Row][right]);
        rightLine = matrix[number.Row][right].ToString();
    }

    // Check bottom
    var bottomRow = number.Row + 1;
    var bottomLine = "";
    if (bottomRow < matrix.Length)
    {
        var start = Math.Max(0, number.ColumnStart - 1);
        var end = Math.Min(matrix[bottomRow].Length - 1, number.ColumnEnd + 1);

        var bottom = matrix[bottomRow][start..(end + 1)];
        surroundingChars.AddRange(bottom);
        bottomLine = new string(bottom);
    }

    foreach (var surroundingChar in surroundingChars)
    {
        if (symbols.Contains(surroundingChar))
        {
            isPartNumber = true;
            break;
        }
    }

    if (isPartNumber)
    {
        partNumbers.Add(number.Value);
    }
}


Console.WriteLine($"Sum of part numbers: {partNumbers.Sum()}");

// Part 2

var gearPowers = new List<int>();

for(var i = 0; i < matrix.Length; i += 1){
    for(var j = 0; j < matrix.Length; j += 1){
        if(matrix[i][j] == '*') {
            var gearNumbers = numbers.Where(n => (i >= n.Row - 1 && i <= n.Row + 1) && (j >= n.ColumnStart - 1 && j <= n.ColumnEnd + 1)).Select(n => n.Value).ToList();
            if (gearNumbers.Count == 2) {
                gearPowers.Add(gearNumbers[0] * gearNumbers[1]);
            }
        }
    }
}
Console.WriteLine($"Gears count: {gearPowers.Count}");
Console.WriteLine($"Gear power: {gearPowers.Sum()}");
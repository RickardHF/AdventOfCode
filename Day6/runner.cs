var test = false;

var filePath = test ? "Day6/test.txt" : "Day6/input.txt";

var input = await File.ReadAllLinesAsync(filePath);

var times = input[0].Split(":")[1].Trim().Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();
var distances = input[1].Split(":")[1].Trim().Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();

Console.WriteLine($"Times: {string.Join(", ", times)}");
Console.WriteLine($"Distances: {string.Join(", ", distances)}");

var options = new List<int>();

for (var i = 0; i < times.Count; i += 1)
{
    var time = times[i];
    var distance = distances[i];

    var matches = 0;
    for (var j = 1; j < time - 1; j += 1) {
        var distanceTraveled = CalculateDistanceTraveled(time - j, j);
        if (distanceTraveled > distance) matches += 1;  
    }

    options.Add(matches);
}

Console.WriteLine($"Options: {string.Join(", ", options)}");

int CalculateDistanceTraveled(int time, int speed) {
    var distance = 0;

    foreach(var t in Enumerable.Range(0, time)) {
        distance += speed;
    }

    return distance;
}

// Multiply all the options together
var result = options.Aggregate(1, (acc, x) => acc * x);
Console.WriteLine($"Result: {result}");

// Part two

var realTime = long.Parse(input[0].Split(":")[1].Replace(" ", ""));
var realDistance = long.Parse(input[1].Split(":")[1].Replace(" ", ""));

Console.WriteLine($"Real time: {realTime}");
Console.WriteLine($"Real distance: {realDistance}");

var sqPart = Math.Sqrt((realTime * realTime) - (4 * realDistance));

var x1 = (realTime + sqPart) / 2;
var x2 = (realTime - sqPart) / 2;

Console.WriteLine($"x1: {x1}");
Console.WriteLine($"x2: {x2}");

var larger = x1 > x2 ? x1 : x2;
var smaller = x1 < x2 ? x1 : x2;

larger = Math.Floor(larger);
smaller = Math.Ceiling(smaller);

Console.WriteLine($"Larger: {larger}");
Console.WriteLine($"Smaller: {smaller}");

Console.WriteLine($"Options: {larger - smaller + 1}");
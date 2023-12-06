
using System;
using System.IO;

var filePath = "Day2/input.txt";

var input = await File.ReadAllTextAsync(filePath);

var lines = input.Split("\n");

var limits = new Dictionary<string, int>(){
    {"blue", 14},
    {"red", 12},
    {"green", 13},
};

var possibleIds = new List<int>();
var setPowers = new List<int>();

foreach(var line in lines) {
    var split = line.Split(":");
    var hands = split[1].Split("; ");

    var id = int.Parse(split[0].Split(" ")[1]);
    
    var possible = true;

    // Checking if possible game
    foreach(var hand in hands) {
        var sets = hand.Trim().Split(", ");
        foreach(var set in sets) {
            var parts = set.Trim().Split(" ");
            var color = parts[1];
            var amount = parts[0];
            if (int.Parse(amount) > limits[color]) {
                possible = false;
                break;
            }
        }
        if (!possible) {
            break;
        }
    }

    // Checking minimum number of cubes needed
    var red = 0;
    var green = 0;
    var blue = 0;

    foreach(var hand in hands) {
        var sets = hand.Trim().Split(", ");
        foreach(var set in sets) {
            var parts = set.Trim().Split(" ");
            var color = parts[1];
            var amount = int.Parse(parts[0]);
            if (color == "red" && amount > red) {
                red = amount;
            } else if (color == "green" && amount > green) {
                green = amount;
            } else if (color == "blue" && amount > blue) {
                blue = amount;
            }
        }
    
    }

    var setPower = red * green * blue;
    setPowers.Add(setPower);

    if (possible) {
        possibleIds.Add(id);
    }

}

Console.WriteLine("Sum of possible game IDs:");
Console.WriteLine(possibleIds.Sum());

Console.WriteLine("Sum of set powers:");
Console.WriteLine(setPowers.Sum());
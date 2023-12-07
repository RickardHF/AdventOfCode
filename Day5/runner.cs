var test = false;

var filePath = test ? "Day5/test.txt" : "Day5/input.txt";

var input = await File.ReadAllLinesAsync(filePath);

foreach (var line in input[0].Split(":")[1].Trim().Split(" "))
{
    Console.WriteLine($"Parsing '{line}'");
    var intForm = long.Parse(line);
}

var seeds = input[0].Split(":")[1].Trim().Split(" ").Select(long.Parse).ToList();

Console.WriteLine($"Seeds: {string.Join(", ", seeds)}");

class Map
{
    public long DestinationRangeStart { get; set; }
    public long SourceRangeStart { get; set; }
    public long Length { get; set; }
}

var maps = new Dictionary<string, List<Map>>();
var currentType = "";
var currentMaps = new List<Map>();

for (var i = 1; i < input.Length; i += 1)
{
    if (string.IsNullOrEmpty(input[i]))
    {
        if (!string.IsNullOrEmpty(currentType)) maps.Add(currentType, currentMaps);
        currentType = "";
        currentMaps = new List<Map>();
        continue;
    }

    if (input[i].Contains(":"))
    {
        currentType = input[i].Split("map:")[0].Trim();
        continue;
    }

    var parts = input[i].Split(" ").Select(long.Parse).ToList();
    currentMaps.Add(new Map
    {
        DestinationRangeStart = parts[0],
        SourceRangeStart = parts[1],
        Length = parts[2]
    });

}

if (!string.IsNullOrEmpty(currentType)) maps.Add(currentType, currentMaps);

foreach (var map in maps)
{
    Console.WriteLine($"{map.Key}: {string.Join(", ", map.Value.Select(m => $"({m.DestinationRangeStart}, {m.SourceRangeStart}, {m.Length})"))}");
}


long Convert(long value, string type, Dictionary<string, List<Map>> maps)
{
    var map = maps[type].Where(m => m.SourceRangeStart <= value && m.SourceRangeStart + m.Length > value).FirstOrDefault();
    return map != null ? map.DestinationRangeStart + (value - map.SourceRangeStart) : value;
}

long ConvertBack(long value, string type, Dictionary<string, List<Map>> maps)
{
    var map = maps[type].Where(m => m.DestinationRangeStart <= value && m.DestinationRangeStart + m.Length > value).FirstOrDefault();
    return map != null ? map.SourceRangeStart + (value - map.DestinationRangeStart) : value;
}

long GetLocation(long seed)
{
    var soil = Convert(seed, "seed-to-soil", maps);
    var fertilizer = Convert(soil, "soil-to-fertilizer", maps);
    var water = Convert(fertilizer, "fertilizer-to-water", maps);
    var light = Convert(water, "water-to-light", maps);
    var temperature = Convert(light, "light-to-temperature", maps);
    var humidity = Convert(temperature, "temperature-to-humidity", maps);
    var location = Convert(humidity, "humidity-to-location", maps);

    return location;
}

long GetLowestLocation(List<long> seeds)
{
    var locations = new List<long>();

    foreach (var seed in seeds)
    {
        locations.Add(GetLocation(seed));
    }

    return locations.Min();
}

Console.WriteLine($"Lowest location is {GetLowestLocation(seeds)}");

// Part 2

var seedRanges = new List<(long, long)>();

for(var i = 0; i < seeds.Count - 1; i += 2) {
    var seed = seeds[i];
    var nextSeed = seed + seeds[i + 1];
    seedRanges.Add((seed, nextSeed));
}

long GetSeedFromLocation(long location) {
    var humidity = ConvertBack(location, "humidity-to-location", maps);
    var temperature = ConvertBack(humidity, "temperature-to-humidity", maps);
    var light = ConvertBack(temperature, "light-to-temperature", maps);
    var water = ConvertBack(light, "water-to-light", maps);
    var fertilizer = ConvertBack(water, "fertilizer-to-water", maps);
    var soil = ConvertBack(fertilizer, "soil-to-fertilizer", maps);
    var seed = ConvertBack(soil, "seed-to-soil", maps);

    return seed;
}

var seedLocations = new List<(long, long)>();

foreach(var locationMaps in maps["humidity-to-location"]) {
    var locationRangeStart = locationMaps.DestinationRangeStart;
    var locationRangeEnd = locationMaps.DestinationRangeStart + locationMaps.Length;
    var seedRangeStart = GetSeedFromLocation(locationRangeStart);
    var seedRangeEnd = GetSeedFromLocation(locationRangeEnd);
    if (seedRangeStart < seedRangeEnd) seedLocations.Add((seedRangeStart, seedRangeEnd));
    else seedLocations.Add((seedRangeEnd, seedRangeStart));
}

foreach(var seedRange in seedRanges) {
    Console.WriteLine($"Seed range: {seedRange}");
    var matchingLocations = seedLocations.Where(x => seedRange.Item1 >= x.Item2 && seedRange.Item2 <= x.Item1).ToList();
    Console.WriteLine($"Matching locations: {string.Join(", ", matchingLocations)}");
    
}
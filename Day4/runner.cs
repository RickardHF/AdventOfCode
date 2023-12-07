
var test = false;

var filePath = test ? "Day4/test.txt" : "Day4/input.txt";

var cards = await File.ReadAllLinesAsync(filePath);

var points = new List<int>();

foreach(var card in cards) {
    Console.WriteLine(card);
    var parts = card.Split(":");
    var lastSpaceIndex = parts[0].LastIndexOf(" ");
    var id = int.Parse(parts[0].Substring(lastSpaceIndex + 1));

    var sets = parts[1].Split("|");

    var winningNumbers = sets[0].Trim().Replace("  ", " ").Split(" ").Select(int.Parse).ToList();
    var yourNumbers = sets[1].Trim().Replace("  ", " ").Split(" ").Select(int.Parse).ToList();

    var matches = yourNumbers.Intersect(winningNumbers).ToList();


    var cardPoints = 0;

    foreach(var match in matches) {
        if (cardPoints == 0) cardPoints = 1;
        else cardPoints *= 2;
    }

    points.Add(cardPoints);

    Console.WriteLine($"Game {id}: matches = {matches.Count} points = {cardPoints}");
}

Console.WriteLine($"Total points: {points.Sum()}");


// Part two

var newCards = new Dictionary<int, List<int>>();

foreach(var card in cards) {
    Console.WriteLine(card);
    var parts = card.Split(":");
    var lastSpaceIndex = parts[0].LastIndexOf(" ");
    var id = int.Parse(parts[0].Substring(lastSpaceIndex + 1));

    var sets = parts[1].Split("|");

    var winningNumbers = sets[0].Trim().Replace("  ", " ").Split(" ").Select(int.Parse).ToList();
    var yourNumbers = sets[1].Trim().Replace("  ", " ").Split(" ").Select(int.Parse).ToList();

    var matches = yourNumbers.Intersect(winningNumbers).ToList();

    var wins = new List<int>();

    var newCard = id;
    foreach(var match in matches) {
        newCard += 1;
        wins.Add(newCard);
    }

    newCards.Add(id, wins);
}


int GetAmountOfCards(int cardId, Dictionary<int, List<int>> winnings) {
    var amount = 1;

    var cardWins = winnings[cardId];
    foreach(var cardWin in cardWins) {
        amount += GetAmountOfCards(cardWin, winnings);
    }

    return amount;
}

var totalAmount = 0;

foreach(var card in newCards) {
    var amount = GetAmountOfCards(card.Key, newCards);
    Console.WriteLine($"Card {card.Key} has {amount} cards");
    totalAmount += amount;    
}

Console.WriteLine($"Total amount of cards: {totalAmount}");
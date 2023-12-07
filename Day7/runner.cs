var test = false;

var filePath = test ? "Day7/test.txt" : "Day7/input.txt";

var input = await File.ReadAllLinesAsync(filePath);

enum HandTypes
{
    FiveOfAKind = 0,
    FourOfAKind = 1,
    FullHouse = 2,
    ThreeOfAKind = 3,
    TwoPairs = 4,
    OnePair = 5,
    HighCard = 6
}

var suits = new Dictionary<char, int> {
    { 'A', 14 },
    { 'K', 13 },
    { 'Q', 12 },
    { 'J', 11 },
    { 'T', 10 }
};

HandTypes GetHandType (IEnumerable<char> cards) {
    var groupedHand = cards.GroupBy(x => x);
    if (groupedHand.Any(x => x.Count() == 5)) return HandTypes.FiveOfAKind;
    else if (groupedHand.Any(x => x.Count() == 4)) return HandTypes.FourOfAKind;
    else if (groupedHand.Any(x => x.Count() == 3) && groupedHand.Any(x => x.Count() == 2)) return HandTypes.FullHouse;
    else if (groupedHand.Any(x => x.Count() == 3)) return HandTypes.ThreeOfAKind;
    else if (groupedHand.Count(x => x.Count() == 2) == 2) return HandTypes.TwoPairs;
    else if (groupedHand.Any(x => x.Count() == 2)) return HandTypes.OnePair;
    else return HandTypes.HighCard;
}

class Hand
{
    public List<char> Cards { get; set; }
    public HandTypes Type { get; set; }
    public int Wager { get; set; }
}

int GetCardValue(char card)
{
    if (char.IsDigit(card)) return int.Parse(card.ToString());
    else return suits[card];
}

List<char> SortCards(List<char> cards)
{
    return cards.OrderByDescending(x => GetCardValue(x)).ToList();
}

var hands = new List<Hand>();

foreach (var line in input)
{
    var parts = line.Split(" ");
    var wager = int.Parse(parts[1]);
    var cards = new List<char>();
    foreach (var card in parts[0])
    {
        cards.Add(card);
    }
    var hand = new Hand {
        Wager = wager,
        Cards = cards,
        Type = GetHandType(cards)
    };
    var sortedCards = SortCards(cards);

    hands.Add(hand);
}

var sortedHands = hands
    .OrderByDescending(x => x.Type)
    .ThenBy(x => GetCardValue(x.Cards[0]))
    .ThenBy(x => GetCardValue(x.Cards[1]))
    .ThenBy(x => GetCardValue(x.Cards[2]))
    .ThenBy(x => GetCardValue(x.Cards[3]))
    .ThenBy(x => GetCardValue(x.Cards[4]))
    .ToList();
var winnings = 0;

for(var i = 0; i < sortedHands.Count; i += 1) {
    var hand = sortedHands[i];
    var winning = (i + 1) * hand.Wager;
    winnings += winning;
}

Console.WriteLine($"Winnings: {winnings}");

// Part two

suits = new Dictionary<char, int> {
    { 'A', 14 },
    { 'K', 13 },
    { 'Q', 12 },
    { 'J', 1 },
    { 'T', 10 }
};

HandTypes GetHandTypePart2 (IEnumerable<char> cards) {
    var groupedHand = cards.GroupBy(x => x);
    
    if (cards.Contains('J')) {
        var jCount = cards.Count(x => x == 'J');
        if (jCount == 5) return HandTypes.FiveOfAKind;
        var highestAmount = cards.Where(x => x != 'J').GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;
        var replacedCards = cards.Select(x => x == 'J' ? highestAmount : x);
        return GetHandType(replacedCards);
    }

    return GetHandType(cards);
}

hands = new List<Hand>();

foreach (var line in input)
{
    var parts = line.Split(" ");
    var wager = int.Parse(parts[1]);
    var cards = new List<char>();
    foreach (var card in parts[0])
    {
        cards.Add(card);
    }
    var hand = new Hand {
        Wager = wager,
        Cards = cards,
        Type = GetHandTypePart2(cards)
    };
    var sortedCards = SortCards(cards);

    hands.Add(hand);
}

sortedHands = hands
    .OrderByDescending(x => x.Type)
    .ThenBy(x => GetCardValue(x.Cards[0]))
    .ThenBy(x => GetCardValue(x.Cards[1]))
    .ThenBy(x => GetCardValue(x.Cards[2]))
    .ThenBy(x => GetCardValue(x.Cards[3]))
    .ThenBy(x => GetCardValue(x.Cards[4]))
    .ToList();
winnings = 0;

for(var i = 0; i < sortedHands.Count; i += 1) {
    var hand = sortedHands[i];
    var winning = (i + 1) * hand.Wager;
    winnings += winning;
}

Console.WriteLine($"Winnings: {winnings}");
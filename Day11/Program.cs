using System.Text.RegularExpressions;
using Day11;


const int monkeyInfoLength = 6;
const int partOneRounds = 20;
Main(partOneRounds);

void Main(int rounds)
{
    var file = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
    var input = File.ReadAllLines(file).Where(e => e != string.Empty).ToArray();
    var monkeys = MapInputToObjects(input).ToList();
    for (int roundIndex = 0; roundIndex < rounds; roundIndex++)
    {
        foreach (var monkey in monkeys)
        {
            var itemsToPass = monkey.InspectItems();
            foreach (var value in itemsToPass)
            {
                monkeys.FirstOrDefault(e => e.Id == value.Key)?.Items.AddRange(value.Value);
            }
        }
    }

    var monkeyBusinessElements = monkeys.OrderByDescending(e => e.InspectedItems).Take(2).Select(e => e.InspectedItems);
    var monkeyBusiness = monkeyBusinessElements.Aggregate((result, next) => result * next);
    Console.WriteLine(monkeyBusiness);
}

IEnumerable<Monkey> MapInputToObjects(IReadOnlyCollection<string> lines)
{
    var monkeys = new List<Monkey>();
    for (var i = 0; i < lines.Count / monkeyInfoLength; i++)
    {
        var monkeyLines = lines.Skip(i * monkeyInfoLength).Take(monkeyInfoLength).ToList();
        var id = int.Parse(Regex.Match(monkeyLines[(int)ObjectStructure.Id], "\\d").Value);
        var startingItems = Regex.Matches(monkeyLines[(int)ObjectStructure.Items], "\\d+").ToList();
        var items = startingItems.Select(e => int.Parse(e.Value)).ToList();
        var operationValue = Regex.Match(monkeyLines[(int)ObjectStructure.Operation], "\\d+").ToString();
        var operationType = Regex.Match(monkeyLines[(int)ObjectStructure.Operation], "[+-\\/*]").ToString();

        var test = monkeyLines[(int)ObjectStructure.Test].Split(" ");
        var testName = test[3];
        var testValue = int.Parse(test[5]);

        var trueValue = int.Parse(Regex.Match(monkeyLines[(int)ObjectStructure.True], "\\d").ToString());
        var falseValue = int.Parse(Regex.Match(monkeyLines[(int)ObjectStructure.False], "\\d").ToString());
        monkeys.Add(new Monkey(id, items, operationType, operationValue,
            new Test(testName, testValue, trueValue, falseValue)));
    }

    return monkeys;
}
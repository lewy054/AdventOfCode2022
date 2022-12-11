namespace Day11;

class Monkey
{
    public int Id { get; set; }
    public long InspectedItems { get; set; } = 0;
    public List<long> Items { get; set; }
    public string OperationType { get; set; }
    public string OperationValue { get; set; }
    public Test Test { get; set; }

    public Monkey(int id, List<long> items, string operationType, string operationValue, Test test)
    {
        Id = id;
        Items = items;
        OperationType = operationType;
        OperationValue = operationValue;
        Test = test;
    }

    public Dictionary<int, List<long>> InspectItems(int modulo, bool part2 = false)
    {
        var itemsToPass = new Dictionary<int, List<long>>();
        var itemsCount = Items.Count;
        for (var index = 0; index < itemsCount; index++)
        {
            if (part2)
            {
                var value = StarInspectionOfItem(Items[index]);
                Items[index] = LevelAfterMonkeyGetsBored(value, modulo);
            }
            else
            {
                var value = StarInspectionOfItem(Items[index]);
                Items[index] = LevelAfterMonkeyGetsBored(value);
            }

            var monkeyToPassTo = ChooseMonkeyToThrowToHer((int)Items[index]);
            if (itemsToPass.ContainsKey(monkeyToPassTo))
            {
                itemsToPass.TryGetValue(monkeyToPassTo, out var existingItems);
                existingItems?.Add(Items[index]);
            }
            else
            {
                itemsToPass.Add(monkeyToPassTo, new List<long> { Items[index] });
            }

            InspectedItems++;
        }

        Items.Clear();

        return itemsToPass;
    }
    
    private long StarInspectionOfItem(long item)
    {
        var value = OperationValue == "" ? item : long.Parse(OperationValue);
        return OperationType switch
        {
            "*" => item * value,
            "/" => item / value,
            "+" => item + value,
            "-" => item - value,
            _ => item
        };
    }

    private long LevelAfterMonkeyGetsBored(long item)
    {
        return item / 3;
    }
    
    private long LevelAfterMonkeyGetsBored(long item, int modulo)
    {
        return item % modulo;
    }

    private int ChooseMonkeyToThrowToHer(int item)
    {
        return item % Test.Value == 0 ? Test.TrueResult : Test.FalseResult;
    }
}
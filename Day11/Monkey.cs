namespace Day11;

class Monkey
{
    public int Id { get; set; }
    public int InspectedItems { get; set; } = 0;
    public List<int> Items { get; set; }
    public string OperationType { get; set; }
    public string OperationValue { get; set; }
    public Test Test { get; set; }

    public Monkey(int id, List<int> items, string operationType, string operationValue, Test test)
    {
        Id = id;
        Items = items;
        OperationType = operationType;
        OperationValue = operationValue;
        Test = test;
    }

    public Dictionary<int, List<int>> InspectItems()
    {
        var itemsToPass = new Dictionary<int, List<int>>();
        var itemsCount = Items.Count;
        for (var index = 0; index < itemsCount; index++)
        {
            Items[index] = StarInspectionOfItem(Items[index]);
            Items[index] = LevelAfterMonkeyGetsBored(Items[index]);

            var monkeyToPassTo = ChooseMonkeyToThrowToHer(Items[index]);
            if (itemsToPass.ContainsKey(monkeyToPassTo))
            {
                itemsToPass.TryGetValue(monkeyToPassTo, out var existingItems);
                existingItems?.Add(Items[index]);
            }
            else
            {
                itemsToPass.Add(monkeyToPassTo, new List<int> { Items[index] });
            }

            InspectedItems++;
        }

        Items.Clear();

        return itemsToPass;
    }

    private int StarInspectionOfItem(int item)
    {
        var value = OperationValue == "" ? item : int.Parse(OperationValue);
        return OperationType switch
        {
            "*" => item * value,
            "/" => item / value,
            "+" => item + value,
            "-" => item - value,
            _ => item
        };
    }

    private int LevelAfterMonkeyGetsBored(int item)
    {
        return item / 3;
    }

    private int ChooseMonkeyToThrowToHer(int item)
    {
        return item % Test.Value == 0 ? Test.TrueResult : Test.FalseResult;
    }
}
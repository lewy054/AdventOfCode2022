namespace Day11;

class Test
{
    public string Operation { get; set; }
    public int Value { get; set; }
    public int TrueResult { get; set; }
    public int FalseResult { get; set; }

    public Test(string operation, int value, int trueResult, int falseResult)
    {
        Operation = operation;
        Value = value;
        TrueResult = trueResult;
        FalseResult = falseResult;
    }
}
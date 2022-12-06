using System.Text.RegularExpressions;

var file = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
const int firstPackagePosition = 1;
const int spacingBetweenPacks = 4;
const int rows = 9;

PartOne();
PartTwo();

void PartOne()
{
    var stacks = PrepareStacks();
    var instructions = ReadInstruction();
    foreach (var instruction in instructions)
    {
        var (move, from, to) = GetOneInstructionDetails(instruction);
        stacks[to] = stacks[to].Insert(0, new string(stacks[from].Take(move).Reverse().ToArray()));
        stacks[from] = stacks[from].Remove(0, move);
    }

    var result = stacks.Aggregate("", (current, stack) => current + stack.First());
    Console.WriteLine(result);
}


void PartTwo()
{
    var stacks = PrepareStacks();
    var instructions = ReadInstruction();
    foreach (var instruction in instructions)
    {
        var (move, from, to) = GetOneInstructionDetails(instruction);
        stacks[to] = stacks[to].Insert(0, new string(stacks[from].Take(move).ToArray()));
        stacks[from] = stacks[from].Remove(0, move);
    }

    var result = stacks.Aggregate("", (current, stack) => current + stack.First());
    Console.WriteLine(result);
}

List<string> PrepareStacks()
{
    var stacksInput = File.ReadLines(file).TakeWhile(line => !string.IsNullOrEmpty(line)).SkipLast(1).ToList();
    var stacks = new List<string>(new string[rows]);

    foreach (var stackInput in stacksInput)
    {
        var line = new List<char>();
        for (var i = firstPackagePosition; i < stackInput.Length; i += spacingBetweenPacks)
        {
            line.Add(stackInput[i]);
        }

        for (var index = 0; index < line.Count; index++)
        {
            stacks[index] += line[index];
        }
    }

    return stacks.Select(e => e.Trim(' ')).ToList();
}

List<string> ReadInstruction()
{
    return File.ReadLines(file)
        .SkipWhile(line => !string.IsNullOrEmpty(line))
        .Skip(1)
        .ToList();
}

(int move, int from, int to) GetOneInstructionDetails(string instruction)
{
    var instructionNumbers = Regex.Matches(instruction, @"\d+").ToList();
    var move = int.Parse(instructionNumbers[0].Value);
    var from = int.Parse(instructionNumbers[1].Value) - 1;
    var to = int.Parse(instructionNumbers[2].Value) - 1;
    return (move, from, to);
}
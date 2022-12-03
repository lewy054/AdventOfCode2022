var file = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
PartOne(file);
PartTwo(file);

void PartOne(string filePath)
{
    var prioritiesSum = 0;
    foreach (var line in File.ReadLines(filePath))
    {
        var firstHalfLength = line.Length / 2;
        var secondHalfLength = line.Length - firstHalfLength;
        var compartments  = new[] 
        {
            line[..firstHalfLength],
            line[^secondHalfLength..]
        };
        var list = compartments[0].Intersect(compartments[1]).ToList();
        prioritiesSum += list.Sum(item => char.IsUpper(item) ? item - 38 : item - 96);
    }
    Console.WriteLine(prioritiesSum);
}

void PartTwo(string filePath)
{
    var prioritiesSum = 0;
    const int linesPerGroup = 3;
    var groupRucksacks  = new List<string>();
    foreach (var line in File.ReadLines(filePath))
    {
        groupRucksacks.Add(line);
        if (groupRucksacks.Count == linesPerGroup) {
            var list = groupRucksacks[0].Intersect(groupRucksacks[1]).Intersect(groupRucksacks[2]).ToList();
            prioritiesSum += list.Sum(item => char.IsUpper(item) ? item - 38 : item - 96);
            groupRucksacks.Clear();
        }
    }
    Console.WriteLine(prioritiesSum);
}
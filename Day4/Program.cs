var file = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
PartOne(file);
PartTwo(file);

void PartOne(string filePath)
{
    var sum = 0;
    foreach (var line in File.ReadLines(filePath))
    {
        var elvesPair = line.Split(",");
        var firstElf = GetElfSector(elvesPair[0]).ToList();
        var secondElf = GetElfSector(elvesPair[1]).ToList();
        var enumerable = firstElf.Except(secondElf);
        var enumerable1 = secondElf.Except(firstElf);
        if (!enumerable.Any() || !enumerable1.Any())
        {
            sum++;
        }
    }

    Console.WriteLine(sum);
}

void PartTwo(string filePath)
{
    var sum = 0;
    foreach (var line in File.ReadLines(filePath))
    {
        var elvesPair = line.Split(",");
        var firstElf = GetElfSector(elvesPair[0]).ToList();
        var secondElf = GetElfSector(elvesPair[1]).ToList();
        var enumerable = firstElf.Intersect(secondElf);
        var enumerable1 = secondElf.Intersect(firstElf);
        if (enumerable.Any() || enumerable1.Any())
        {
            sum++;
        }
    }

    Console.WriteLine(sum);
}

IEnumerable<int> GetElfSector(string section)
{
    var sectorStartRange = int.Parse(section.Split("-")[0]);
    var sectorEndRange = int.Parse(section.Split("-")[1]) - sectorStartRange + 1;
    return Enumerable.Range(sectorStartRange, sectorEndRange);
}
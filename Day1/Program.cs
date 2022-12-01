var file = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
PartOne(file);
PartTwo(file);

void PartOne(string filePath)
{
    var mostCalories = 0;
    var currentCalories = 0;
    foreach (var line in File.ReadLines(filePath))
    {
        if (!string.IsNullOrEmpty(line))
        {
            currentCalories += int.Parse(line);
        }
        else
        {
            if (currentCalories > mostCalories)
            {
                mostCalories = currentCalories;
            }

            currentCalories = 0;
        }
    }

    Console.WriteLine(mostCalories);
}

void PartTwo(string filePath)
{
    var mostCalories = new List<int> { 0, 0, 0 };
    var currentCalories = 0;
    foreach (var line in File.ReadLines(filePath))
    {
        if (!string.IsNullOrEmpty(line))
        {
            currentCalories += int.Parse(line);
        }
        else
        {
            foreach (var mostCalorie in mostCalories)
            {
                if (currentCalories > mostCalorie)
                {
                    var minValue = mostCalories.Min();
                    mostCalories[mostCalories.IndexOf(minValue)] = currentCalories;
                    break;
                }
            }

            currentCalories = 0;
        }
    }
    Console.WriteLine(mostCalories.Sum());
}
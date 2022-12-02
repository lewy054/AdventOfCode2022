var file = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
PartOne(file);
PartTwo(file);

const int pointsForRock = 1;
const int pointsForPaper = 2;
const int pointsForScissors = 3;
const int pointsForLost = 0;
const int pointsForDraw = 3;
const int pointsForWin = 6;

void PartOne(string filePath)
{
    // A B C - Rock, Paper, Scissors
    // X Y Z - Rock, Paper, Scissors
    var totalPoints = 0;
    foreach (var line in File.ReadLines(filePath))
    {
        switch (line[0], line[2])
        {
            // draw
            case ('A', 'X'):
                totalPoints += pointsForRock + pointsForDraw;
                break;
            case ('B', 'Y'):
                totalPoints += pointsForPaper + pointsForDraw;
                break;
            case ('C', 'Z'):
                totalPoints += pointsForScissors + pointsForDraw;
                break;

            // lost
            case ('A', 'Z'):
                totalPoints += pointsForScissors + pointsForLost;
                break;
            case ('B', 'X'):
                totalPoints += pointsForRock + pointsForLost;
                break;
            case ('C', 'Y'):
                totalPoints += pointsForPaper + pointsForLost;
                break;

            // win
            case ('A', 'Y'):
                totalPoints += pointsForPaper + pointsForWin;
                break;
            case ('B', 'Z'):
                totalPoints += pointsForScissors + pointsForWin;
                break;
            case ('C', 'X'):
                totalPoints += pointsForRock + pointsForWin;
                break;
        }
    }

    Console.WriteLine(totalPoints);
}

void PartTwo(string filePath)
{
    // A B C - Rock, Paper, Scissors
    // X Y Z - Lose, Draw, Win
    var totalPoints = 0;
    foreach (var line in File.ReadLines(filePath))
    {
        switch (line[2])
        {
            case 'X':
                switch (line[0])
                {
                    case ('A'):
                        totalPoints += pointsForScissors + pointsForLost;
                        break;
                    case ('B'):
                        totalPoints += pointsForRock + pointsForLost;
                        break;
                    case ('C'):
                        totalPoints += pointsForPaper + pointsForLost;
                        break;
                }

                break;
            case 'Y':
                switch (line[0])
                {
                    case ('A'):
                        totalPoints += pointsForRock + pointsForDraw;
                        break;
                    case ('B'):
                        totalPoints += pointsForPaper + pointsForDraw;
                        break;
                    case ('C'):
                        totalPoints += pointsForScissors + pointsForDraw;
                        break;
                }

                break;
            case 'Z':
                switch (line[0])
                {
                    case ('A'):
                        totalPoints += pointsForPaper + pointsForWin;
                        break;
                    case ('B'):
                        totalPoints += pointsForScissors + pointsForWin;
                        break;
                    case ('C'):
                        totalPoints += pointsForRock + pointsForWin;
                        break;
                }

                break;
        }
    }
    Console.WriteLine(totalPoints);
}
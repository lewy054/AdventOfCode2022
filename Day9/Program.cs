using Day9;

var file = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

PartOne();
PartTwo();

void PartOne()
{
    var visitedPositions = new List<Position>() { new(0, 0) };
    var head = new Position(0, 0);
    var tail = new Position(0, 0);
    foreach (var line in File.ReadLines(file))
    {
        var elements = line.Split(" ");
        var direction = elements[0];
        var moves = int.Parse(elements[1]);
        switch (direction)
        {
            case "U":
                for (int i = 0; i < moves; i++)
                {
                    head.Y++;
                    if (tail.Y == head.Y || tail.Y + 1 == head.Y)
                    {
                        continue;
                    }

                    if (tail.X == head.X)
                    {
                        tail.Y++;
                    }
                    else
                    {
                        tail.Y++;
                        tail.X = tail.X + 1 == head.X ? tail.X += 1 : tail.X -= 1;
                    }

                    var alreadyVisited = CheckIfPositionAlreadyVisited(visitedPositions, tail);
                    if (!alreadyVisited)
                    {
                        visitedPositions.Add(new Position(tail.X, tail.Y));
                    }
                }

                break;
            case "D":
                for (int i = 0; i < moves; i++)
                {
                    head.Y--;
                    if (tail.Y == head.Y || tail.Y - 1 == head.Y)
                    {
                        continue;
                    }

                    if (tail.X == head.X)
                    {
                        tail.Y--;
                    }
                    else
                    {
                        tail.Y--;
                        tail.X = tail.X - 1 == head.X ? tail.X -= 1 : tail.X += 1;
                    }

                    var alreadyVisited = CheckIfPositionAlreadyVisited(visitedPositions, tail);
                    if (!alreadyVisited)
                    {
                        visitedPositions.Add(new Position(tail.X, tail.Y));
                    }
                }

                break;
            case "R":
                for (int i = 0; i < moves; i++)
                {
                    head.X++;
                    if (tail.X == head.X || tail.X + 1 == head.X)
                    {
                        continue;
                    }

                    if (tail.Y == head.Y)
                    {
                        tail.X++;
                    }
                    else
                    {
                        tail.X++;
                        tail.Y = tail.Y + 1 == head.Y ? tail.Y += 1 : tail.Y -= 1;
                    }


                    var alreadyVisited = CheckIfPositionAlreadyVisited(visitedPositions, tail);
                    if (!alreadyVisited)
                    {
                        visitedPositions.Add(new Position(tail.X, tail.Y));
                    }
                }

                break;
            case "L":
                for (int i = 0; i < moves; i++)
                {
                    head.X--;
                    if (tail.X == head.X || tail.X - 1 == head.X)
                    {
                        continue;
                    }

                    if (tail.Y == head.Y)
                    {
                        tail.X--;
                    }
                    else
                    {
                        tail.X--;
                        tail.Y = tail.Y - 1 == head.Y ? tail.Y -= 1 : tail.Y += 1;
                    }

                    var alreadyVisited = CheckIfPositionAlreadyVisited(visitedPositions, tail);
                    if (!alreadyVisited)
                    {
                        visitedPositions.Add(new Position(tail.X, tail.Y));
                    }
                }

                break;
        }
    }

    Console.WriteLine(visitedPositions.Count);
}

void PartTwo()
{
    const int ropeLength = 10;
    var rope = new List<Position>(ropeLength);
    for (int i = 0; i < ropeLength; i++)
    {
        rope.Add(new Position(0, 0));
    }

    var visitedPositions = new List<Position>() { new(0, 0) };
    foreach (var line in File.ReadLines(file))
    {
        var elements = line.Split(" ");
        var direction = elements[0];
        var moves = int.Parse(elements[1]);
        for (int i = 0; i < moves; i++)
        {
            switch (direction)
            {
                case "U":
                    rope[0].Y++;
                    break;
                case "D":
                    rope[0].Y--;
                    break;
                case "L":
                    rope[0].X--;
                    break;
                case "R":
                    rope[0].X++;
                    break;
            }

            for (var index = 1; index < rope.Count; index++)
            {
                var tail = rope[index];
                var head = rope[index - 1];
                if (index == rope.Count - 1)
                {
                    var alreadyVisited = CheckIfPositionAlreadyVisited(visitedPositions, tail);
                    if (!alreadyVisited)
                    {
                        visitedPositions.Add(new Position(tail.X, tail.Y));
                    }
                }

                var continueLoop = MoveInSameRow(tail, head);
                if (continueLoop)
                {
                    continue;
                }

                continueLoop = MoveInSameColumn(tail, head);
                if (continueLoop)
                {
                    continue;
                }

                MoveDiagonally(tail, head);
            }
        }
    }

    Console.WriteLine(visitedPositions.Count + 1);
}

bool MoveInSameRow(Position tail, Position head)
{
    if (tail.X != head.X)
    {
        return false;
    }

    if (tail.Y == head.Y)
    {
        return true;
    }

    if (tail.Y > head.Y + 1)
    {
        tail.Y--;
        return true;
    }

    if (tail.Y < head.Y - 1)
    {
        tail.Y++;
        return true;
    }

    return true;
}

bool MoveInSameColumn(Position tail, Position head)
{
    if (tail.Y != head.Y)
    {
        return false;
    }

    if (tail.X + 1 == head.X || tail.X - 1 == head.X)
    {
        return true;
    }

    tail.X = tail.X > head.X ? tail.X - 1 : tail.X + 1;
    return true;
}

bool MoveDiagonally(Position tail, Position head)
{
    //don't judge me by that
    if (tail.Y + 1 == head.Y || tail.Y - 1 == head.Y)
    {
        if (tail.X + 1 == head.X || tail.X - 1 == head.X)
        {
            return true;
        }
    }

    if (tail.X < head.X)
    {
        //left
        if (tail.Y > head.Y)
        {
            //top left
            tail.Y--;
            tail.X++;
        }
        else
        {
            //bottom left
            tail.Y++;
            tail.X++;
        }
    }
    else
    {
        //right
        if (tail.Y > head.Y)
        {
            //top right
            tail.Y--;
            tail.X--;
        }
        else
        {
            //bottom right
            tail.Y++;
            tail.X--;
        }
    }

    return false;
}


bool CheckIfPositionAlreadyVisited(IEnumerable<Position> visitedPositions, Position position)
{
    return visitedPositions.Where(e => e.X == position.X).Any(e => e.Y == position.Y);
}
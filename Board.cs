using System;

namespace BattleshipGame
{
    class Board
    {
        private const int Size = 10;
        private Ship[] ships;
        private bool[,] shots;

        public Board()
        {
            ships = new Ship[10];
            ships[0] = new Ship(4);
            ships[1] = new Ship(3);
            ships[2] = new Ship(3);
            ships[3] = new Ship(2);
            ships[4] = new Ship(2);
            ships[5] = new Ship(2);
            ships[6] = new Ship(1);
            ships[7] = new Ship(1);
            ships[8] = new Ship(1);
            ships[9] = new Ship(1);
            shots = new bool[Size, Size];
        }

        public void SetShipsManually()
        {
            Console.WriteLine("Set your ships:");
            Draw();
            foreach (var ship in ships)
            {
                bool placed = false;
                while (!placed)
                {
                    Console.Write($"Enter starting position for {ship.Size}-length ship (e.g., A1): ");
                    string position = Console.ReadLine().ToUpper();

                    if (position.Length >= 2)
                    {
                        int x = position[1] - '1';
                        int y = position[0] - 'A';

                        if (x >= 0 && x < Size && y >= 0 && y < Size)
                        {
                            Console.Write("Enter direction (R for right, D for down): ");
                            char direction = Console.ReadLine().ToUpper()[0];
                            bool horizontal = direction == 'R';

                            if (CanPlaceShip(ship, x, y, horizontal))
                            {
                                PlaceShip(ship, x, y, horizontal);
                                placed = true;
                            }
                            else
                            {
                                Console.WriteLine("Cannot place the ship there. Try again.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid position. Please enter a position within the board.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid position. Please enter a position within the board.");
                    }
                }
            }
        }




        private bool CanPlaceShip(Ship ship, int x, int y, bool horizontal)
        {
            if (horizontal && y + ship.Size > Size)
                return false;
            if (!horizontal && x + ship.Size > Size)
                return false;

            for (int i = 0; i < ship.Size; i++)
            {
                int newX = horizontal ? x : x + i;
                int newY = horizontal ? y + i : y;

                if (newX < 0 || newX >= Size || newY < 0 || newY >= Size || shipsAnyOccupiesPosition(newX, newY))
                    return false;
            }

            return true;
        }

        private void PlaceShip(Ship ship, int x, int y, bool horizontal)
        {
            for (int i = 0; i < ship.Size; i++)
            {
                int newX = horizontal ? x : x + i;
                int newY = horizontal ? y + i : y;

                ship.Positions.Add((newX, newY));
            }
        }

        private bool shipsAnyOccupiesPosition(int x, int y)
        {
            foreach (var ship in ships)
            {
                if (ship.OccupiesPosition(x, y))
                    return true;
            }
            return false;
        }


        public void Draw()
        {
            Console.WriteLine("    A B C D E F G H I J");
            for (int i = 0; i < Size; i++)
            {
                Console.Write((i + 1).ToString().PadLeft(2) + " ");
                for (int j = 0; j < Size; j++)
                {
                    bool shipHit = false;
                    bool shipMiss = false;
                    foreach (var ship in ships)
                    {
                        if (ship.OccupiesPosition(i, j) && shots[i, j])
                        {
                            shipHit = true;
                            break;
                        }
                        else if (ship.OccupiesPosition(i, j) && !shots[i, j])
                        {
                            shipMiss = true;
                            break;
                        }
                    }
                    if (shots[i, j] && shipHit)
                    {
                        Console.Write(" X");
                    }
                    else if (shots[i, j] && shipMiss)
                    {
                        Console.Write(" O");
                    }
                    else
                    {
                        Console.Write(" .");
                    }
                }
                Console.WriteLine();
            }
        }

        public bool FireShot(int x, int y)
        {
            if (shots[x, y])
                return false;

            shots[x, y] = true;

            foreach (var ship in ships)
            {
                if (ship.OccupiesPosition(x, y))
                {
                    ship.RegisterHit(x, y);
                    if (ship.IsSunk())
                    {
                        Console.WriteLine("Hit and sunk a ship!");
                    }
                    return true;
                }
            }

            return false;
        }



        public bool ShipSunk(int x, int y)
        {
            foreach (var ship in ships)
            {
                if (ship.OccupiesPosition(x, y) && ship.IsSunk())
                    return true;
            }
            return false;
        }

        public bool AllShipsSunk()
        {
            foreach (var ship in ships)
            {
                if (!ship.IsSunk())
                    return false;
            }
            return true;
        }

        public bool AlreadyFired(int x, int y)
        {
            return shots[x, y];
        }

        public void MarkHit(int x, int y)
        {
            shots[x, y] = true;
            Console.WriteLine($"Hit at {((char)('A' + x))}{y + 1}");
        }

        public void MarkMiss(int x, int y)
        {
            shots[x, y] = true;
            Console.WriteLine($"Miss at {((char)('A' + x))}{y + 1}");
        }

    }
}

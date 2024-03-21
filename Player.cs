using System;

namespace BattleshipGame
{
    class Player
    {
        private Board shipBoard;
        private Board firingBoard;

        public Player()
        {
            shipBoard = new Board();
            firingBoard = new Board();
            SetShips();
        }

        private void SetShips()
        {
            shipBoard.SetShipsManually();
        }

        public void DrawBoards()
        {
            Console.WriteLine("Ship board:");
            shipBoard.Draw();
            Console.WriteLine("\nFiring board:");
            firingBoard.Draw();
        }



        public bool Fire()
        {
            Console.Write("Enter coordinates to fire (e.g., A5): ");
            string coordinates = Console.ReadLine().ToUpper();
            int x = coordinates[0] - 'A';
            int y = int.Parse(coordinates.Substring(1)) - 1;

            if (firingBoard.AlreadyFired(x, y))
            {
                Console.WriteLine("You've already fired at this location!");
                return false;
            }

            bool hit = shipBoard.FireShot(x, y);
            if (hit)
            {
                firingBoard.MarkHit(x, y);
                if (shipBoard.ShipSunk(x, y))
                {
                    Console.WriteLine("You've sunk a ship!");
                }
            }
            else
            {
                firingBoard.MarkMiss(x, y);
            }

            return hit;
        }


        public bool AllShipsSunk()
        {
            return shipBoard.AllShipsSunk();
        }
    }
}

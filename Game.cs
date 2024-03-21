using System;

namespace BattleshipGame
{
    class Game
    {
        private Player player1;
        private Player player2;

        public Game()
        {
            player1 = new Player();
            player2 = new Player();
        }

        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("Battleship Game\n");

            while (true)
            {
                Console.WriteLine($"Player 1 - Your Ships (Top) / Enemy's Board (Bottom)");
                player1.DrawBoards();

                Console.WriteLine($"Player 2 - Your Ships (Top) / Enemy's Board (Bottom)");
                player2.DrawBoards();

                Console.WriteLine("\nPlayer 1 - Fire!");
                bool hitPlayer1 = player2.Fire();
                if (hitPlayer1)
                {
                    Console.WriteLine("Hit!");
                }

                if (player1.AllShipsSunk())
                {
                    Console.WriteLine("Player 2 wins!");
                    break;
                }

                Console.WriteLine("\nPlayer 2 - Fire!");
                bool hitPlayer2 = player1.Fire();
                if (hitPlayer2)
                {
                    Console.WriteLine("Hit!");
                }

                if (player2.AllShipsSunk())
                {
                    Console.WriteLine("Player 1 wins!");
                    break;
                }
            }
        }
    }
}


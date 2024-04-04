using System;
using System.Diagnostics;

namespace BattleShipGame
{
    class Gra
    {
        private Gracz gracz1;
        private Gracz gracz2;

        public Gra()
        {
            gracz1 = new Gracz();
            gracz2 = new Gracz();
        }

        public void RozpocznijGre()
        {
            Console.Clear();
            Console.WriteLine("Gra w Statki\n");

            while (true)
            {
                Console.WriteLine($"Gracz 1 - Twoje Statki (Góra) / Plansza Przeciwnika (Dół)");
                gracz1.RysujPlansze();

                Console.WriteLine($"Gracz 2 - Twoje Statki (Góra) / Plansza Przeciwnika (Dół)");
                gracz2.RysujPlansze();

                Console.WriteLine("\nGracz 1 - Ogień!");
                bool trafienieGracz1 = gracz2.Ogien();
                if (trafienieGracz1)
                {
                    Console.WriteLine("Trafienie!");
                }

                if (gracz1.WszystkieStatkiZatopione())
                {
                    Console.WriteLine("Gracz 2 wygrywa!");
                    break;
                }

                Console.WriteLine("\nGracz 2 - Ogień!");
                bool trafienieGracz2 = gracz1.Ogien();
                if (trafienieGracz2)
                {
                    Console.WriteLine("Trafienie!");
                }

                if (gracz2.WszystkieStatkiZatopione())
                {
                    Console.WriteLine("Gracz 1 wygrywa!");
                    break;
                }
            }
        }
    }
}

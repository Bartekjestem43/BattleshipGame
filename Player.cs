using System;

namespace BattleShipGame
{
    class Gracz
    {
        private Plansza planszaStatkow;
        private Plansza planszaOgnia;

        public Gracz()
        {
            planszaStatkow = new Plansza();
            planszaOgnia = new Plansza();
            UstawStatki();
        }

        private void UstawStatki()
        {
            planszaStatkow.UstawStatkiRecznie();
        }

        public void RysujPlansze()
        {
            Console.WriteLine("Plansza statków:");
            planszaStatkow.Rysuj();
            Console.WriteLine("\nPlansza ognia:");
            planszaOgnia.Rysuj();
        }

        public bool Ogien()
        {
            Console.Write("Podaj współrzędne do ostrzału (np. A5): ");
            string wspolrzedne = Console.ReadLine().ToUpper();
            int x = wspolrzedne[0] - 'A';
            int y = int.Parse(wspolrzedne.Substring(1)) - 1;

            if (planszaOgnia.JuzStrzelono(x, y))
            {
                Console.WriteLine("Już strzelałeś w to miejsce!");
                return false;
            }

            bool trafienie = planszaStatkow.Ogien(x, y);
            if (trafienie)
            {
                planszaOgnia.OznaczTrafienie(x, y);
                if (planszaStatkow.StatekZatopiony(x, y))
                {
                    Console.WriteLine("Zatopiłeś statek!");
                }
            }
            else
            {
                planszaOgnia.OznaczPudlo(x, y);
            }

            return trafienie;
        }

        public bool WszystkieStatkiZatopione()
        {
            return planszaStatkow.WszystkieStatkiZatopione();
        }
    }
}

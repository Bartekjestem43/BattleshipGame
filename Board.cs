using System;

namespace BattleShipGame
{
    class Plansza
    {
        private const int Rozmiar = 10;
        private Statek[] statki;
        private bool[,] strzaly;

        public Plansza()
        {
            statki = new Statek[10];
            statki[0] = new Statek(4);
            statki[1] = new Statek(3);
            statki[2] = new Statek(3);
            statki[3] = new Statek(2);
            statki[4] = new Statek(2);
            statki[5] = new Statek(2);
            statki[6] = new Statek(1);
            statki[7] = new Statek(1);
            statki[8] = new Statek(1);
            statki[9] = new Statek(1);
            strzaly = new bool[Rozmiar, Rozmiar];
        }

        public void UstawStatkiRecznie()
        {
            Console.WriteLine("Ustaw swoje statki:");
            Rysuj();
            foreach (var statek in statki)
            {
                bool umieszczony = false;
                while (!umieszczony)
                {
                    Console.Write($"Podaj początkową pozycję dla statku długości {statek.Rozmiar} (np. A1): ");
                    string pozycja = Console.ReadLine().ToUpper();

                    if (pozycja.Length >= 2)
                    {
                        int x = pozycja[1] - '1';
                        int y = pozycja[0] - 'A';

                        if (x >= 0 && x < Rozmiar && y >= 0 && y < Rozmiar)
                        {
                            Console.Write("Podaj kierunek (R dla prawo, D dla dół): ");
                            char kierunek = Console.ReadLine().ToUpper()[0];
                            bool poziomy = kierunek == 'R';

                            if (MoznaUmiescicStatek(statek, x, y, poziomy))
                            {
                                UmiescStatek(statek, x, y, poziomy);
                                umieszczony = true;
                            }
                            else
                            {
                                Console.WriteLine("Nie można umieścić statku tam. Spróbuj ponownie.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowa pozycja. Podaj pozycję na planszy.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowa pozycja. Podaj pozycję na planszy.");
                    }
                }
            }
        }

        private bool MoznaUmiescicStatek(Statek statek, int x, int y, bool poziomy)
        {
            if (poziomy && y + statek.Rozmiar > Rozmiar)
                return false;
            if (!poziomy && x + statek.Rozmiar > Rozmiar)
                return false;

            for (int i = 0; i < statek.Rozmiar; i++)
            {
                int noweX = poziomy ? x : x + i;
                int noweY = poziomy ? y + i : y;

                if (noweX < 0 || noweX >= Rozmiar || noweY < 0 || noweY >= Rozmiar || CzyKtorysZajmujePozycje(noweX, noweY))
                    return false;
            }

            return true;
        }

        private void UmiescStatek(Statek statek, int x, int y, bool poziomy)
        {
            for (int i = 0; i < statek.Rozmiar; i++)
            {
                int noweX = poziomy ? x : x + i;
                int noweY = poziomy ? y + i : y;

                statek.Pozycje.Add((noweX, noweY));
            }
        }

        private bool CzyKtorysZajmujePozycje(int x, int y)
        {
            foreach (var statek in statki)
            {
                if (statek.ZajmujePozycje(x, y))
                    return true;
            }
            return false;
        }

        public void Rysuj()
        {
            Console.WriteLine("    A B C D E F G H I J");
            for (int i = 0; i < Rozmiar; i++)
            {
                Console.Write((i + 1).ToString().PadLeft(2) + " ");
                for (int j = 0; j < Rozmiar; j++)
                {
                    bool trafienieStatek = false;
                    bool pudloStatek = false;
                    foreach (var statek in statki)
                    {
                        if (statek.ZajmujePozycje(i, j) && strzaly[i, j])
                        {
                            trafienieStatek = true;
                            break;
                        }
                        else if (statek.ZajmujePozycje(i, j) && !strzaly[i, j])
                        {
                            pudloStatek = true;
                            break;
                        }
                    }
                    if (strzaly[i, j] && trafienieStatek)
                    {
                        Console.Write(" X");
                    }
                    else if (strzaly[i, j] && pudloStatek)
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

        public bool Ogien(int x, int y)
        {
            if (strzaly[x, y])
                return false;

            strzaly[x, y] = true;

            foreach (var statek in statki)
            {
                if (statek.ZajmujePozycje(x, y))
                {
                    statek.ZarejestrujTrafienie(x, y);
                    if (statek.Zatopiony())
                    {
                        Console.WriteLine("Trafienie i zatopienie statku!");
                    }
                    return true;
                }
            }

            return false;
        }

        public bool StatekZatopiony(int x, int y)
        {
            foreach (var statek in statki)
            {
                if (statek.ZajmujePozycje(x, y) && statek.Zatopiony())
                    return true;
            }
            return false;
        }

        public bool WszystkieStatkiZatopione()
        {
            foreach (var statek in statki)
            {
                if (!statek.Zatopiony())
                    return false;
            }
            return true;
        }

        public bool JuzStrzelono(int x, int y)
        {
            return strzaly[x, y];
        }

        public void OznaczTrafienie(int x, int y)
        {
            strzaly[x, y] = true;
            Console.WriteLine($"Trafienie w {((char)('A' + x))}{y + 1}");
        }

        public void OznaczPudlo(int x, int y)
        {
            strzaly[x, y] = true;
            Console.WriteLine($"Pudło w {((char)('A' + x))}{y + 1}");
        }

    }
}

using System;
using System.Collections.Generic;

namespace BattleShipGame
{
    class Statek
    {
        public int Rozmiar { get; }
        public List<(int, int)> Pozycje { get; private set; }
        private int trafienia;

        public Statek(int rozmiar)
        {
            Rozmiar = rozmiar;
            Pozycje = new List<(int, int)>();
            trafienia = 0;
        }

        public void DodajPozycje(List<(int, int)> pozycje)
        {
            Pozycje.AddRange(pozycje);
        }

        public bool ZajmujePozycje(int x, int y)
        {
            foreach (var (posX, posY) in Pozycje)
            {
                if (posX == x && posY == y)
                {
                    return true;
                }
            }
            return false;
        }

        public void ZarejestrujTrafienie(int x, int y)
        {
            trafienia++;
        }

        public bool Zatopiony()
        {
            return trafienia >= Rozmiar;
        }
    }
}

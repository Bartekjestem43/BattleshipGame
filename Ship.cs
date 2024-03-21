using System;
using System.Collections.Generic;

namespace BattleshipGame
{
    class Ship
    {
        public int Size { get; }
        public List<(int, int)> Positions { get; private set; }
        private int hits;

        public Ship(int size)
        {
            Size = size;
            Positions = new List<(int, int)>();
            hits = 0;
        }

        public void AddPositions(List<(int, int)> positions)
        {
            Positions.AddRange(positions);
        }

        public bool OccupiesPosition(int x, int y)
        {
            foreach (var (posX, posY) in Positions)
            {
                if (posX == x && posY == y)
                {
                    return true;
                }
            }
            return false;
        }

        public void RegisterHit(int x, int y)
        {
            hits++;
        }

        public bool IsSunk()
        {
            return hits >= Size;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDD49
{
    class Player
    {
        private int playerID; // 0, ..., 7.
        private List<Tile> hand; // Tile hand (max 3).
        private int boardPositionX; // 0, ..., 5. X = 0: Left
        private int boardPositionY; // 0, ..., 5. Y = 0: Up
        private int tilePosition; // 0, ..., 7.

        public Player(int startBoardPositionX, int startBoardPositionY, int startTilePosition)
        {
            boardPositionX = startBoardPositionX;
            boardPositionY = startBoardPositionY;
            tilePosition = startTilePosition;
        }
    }
}
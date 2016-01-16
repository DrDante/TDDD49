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
        private int boardPositionX; // 0, ..., 5. X = 0: Left.
        private int boardPositionY; // 0, ..., 5. Y = 0: Up.
        private int tilePosition; // 0, ..., 7.

        // Constructor.
        public Player(int pID, int startBoardPositionX, int startBoardPositionY, int startTilePosition)
        {
            playerID = pID;
            boardPositionX = startBoardPositionX;
            boardPositionY = startBoardPositionY;
            tilePosition = startTilePosition;
            hand = new List<Tile>();
        }

        #region Custom methods
        // Attempts to add a tile to the hand. Will only work if the hand is not already full.
        public void AddTileToHand(Tile tile)
        {
            if (hand.Count < 3)
            {
                hand.Add(tile);
            }
        }

        // Removes the tile specified by num from the players hand.
        public void RemoveFromHand(int num)
        {
            hand.RemoveAt(num - 1);
        }
        #endregion

        #region Getters
        // Gets the player ID.
        public int GetPlayerID()
        {
            return playerID;
        }

        // Gets the players hand.
        public List<Tile> GetHand()
        {
            return hand;
        }

        // Gets the players X position (horizontal).
        public int GetBoardPosX()
        {
            return boardPositionX;
        }

        // Gets the players Y position (vertical).
        public int GetBoardPosY()
        {
            return boardPositionY;
        }

        // Gets the players tile position.
        public int GetTilePos()
        {
            return tilePosition;
        }
        #endregion

        #region Setters
        // Sets the players X position (horizontal).
        public void SetBoardPosX(int x)
        {
            boardPositionX = x;
        }

        // Sets the players Y position (vertical).
        public void SetBoardPosY(int y)
        {
            boardPositionY = y;
        }

        // Sets the players tile position.
        public void SetTilePos(int n)
        {
            tilePosition = n;
        }
        #endregion
    }
}
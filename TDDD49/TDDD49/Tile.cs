using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDD49
{
    class Tile
    {
        private int[] nodeList = new int[8];
        private int orientation = 0; // Can be 0, 1, 2 or 3.
        // 0 is standard, increasing the number by 1 corresponds
        // to a rotation by 90 degrees in a mathematically
        // positive direction (CCW).
        private Image image;

        // Tiles are defined by the node connections.
        // There is a connection from node i to node n if:
        // nodeConnections[i] = n <=> nodeConnections[n] = i
        // Examples:
        // nodeConnections = "10325476"
        // nodeConnections = "52176043"

        // Tile node chart:
        //    7  6
        //    ____
        // 0 |    | 5
        // 1 |    | 4
        //    ¨¨¨¨
        //    2  3

        // Constructor.
        public Tile(string nodeConnections, Image img)
        {
            // Checks if the tile is valid.
            bool valid = true;
            if (nodeConnections.Length == 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    int a = (int)Char.GetNumericValue(nodeConnections[i]);
                    int n = (int)Char.GetNumericValue(nodeConnections[a]);
                    valid = valid && (i == n);
                }
            }
            else
            {
                valid = false;
            }

            // If the tile image file name was invalid, an exception is thrown.
            if (!valid)
            {
                throw new System.ArgumentException("An attempt was made to create an impossible tile. Check the tile image file names!");
            }

            for (int i = 0; i < 8; i++)
            {
                nodeList[i] = (int)Char.GetNumericValue(nodeConnections[i]);
            }

            image = img;
        }

        #region Custom methods
        // Rotates tile clockwise.
        public void RotateCW()
        {
            orientation--;
            if (orientation == -1)
            {
                orientation = 3;
            }
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        // Rotates tile counterclockwise.
        public void RotateCCW()
        {
            orientation++;
            orientation = orientation % 4;
            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
        }
        #endregion

        #region Getters
        // Gets which node is connected to node i.
        public int GetConnection(int i)
        {
            return nodeList[i];
        }

        // Gets current orientation.
        public int GetOrientation()
        {
            return orientation;
        }

        // Gets the tile image.
        public Image GetImage()
        {
            return image;
        }
        #endregion
    }
}
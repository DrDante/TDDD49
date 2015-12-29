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
        // 0 is standard, increasing the number by 1 equals
        // to a rotation by 90 degrees in a mathematically
        // positive direction (CCW).
        private Image image;

        // Tiles are defined by the node connections.
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
        public Tile(string nodeConnections, Image img)
        {
            // Check if tile is valid.
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

            // if not valid, throw exception/fit
            // else, keep going

            for (int i = 0; i < 8; i++)
            {
                nodeList[i] = (int)Char.GetNumericValue(nodeConnections[i]);
            }

            image = img;
        }

        // Returns what node i is connected to.
        public int GetConnection(int i)
        {
            return nodeList[i];
        }

        // Returns current orientation.
        public int GetOrientation()
        {
            return orientation;
        }

        // Returns image.
        public Image GetImage()
        {
            return image;
        }

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
    }
}
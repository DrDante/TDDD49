using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDD49
{
    class Tile
    {
        private int[] nodeList = new int[8];
        private int orientation = 0; // Can be 0, 1, 2 or 3.

        // Tiles are defined by the node connections.
        // nodeConnections[i] = n <=> nodeConnections[n] = i
        // Examples:
        // nodeConnections = "10325476"
        // nodeConnections = "52176043"
        public Tile(string nodeConnections)
        {
            // Check if tile is valid.
            bool valid = true;
            if (nodeConnections.Length == 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    int n = (int)nodeConnections[(int)nodeConnections[i]];
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
                nodeList[i] = (int)nodeConnections[i];
            }
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

        // Rotates tile clockwise.
        public void RotateCW()
        {
            orientation--;
            if (orientation == -1)
            {
                orientation = 3;
            }
        }

        // Rotates tile counterclockwise.
        public void RotateCCW()
        {
            orientation++;
            orientation = orientation % 4;
        }
    }
}
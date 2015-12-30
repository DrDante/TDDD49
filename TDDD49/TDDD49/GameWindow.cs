using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDDD49
{
    public partial class GameWindow : Form
    {
        private string tileFolder = "..\\..\\images\\tiles";
        private const int tileCount = 35;
        private string[] tilePaths;
        private string[] tileCodes;
        private List<Tile> deck;
        public int playerCount = 4;
        private List<Player> players;
        private Player currentPlayer;
        private int dragonMarkerHome = -1;
        private int selectedHandTile;

        // Constructor.
        public GameWindow()
        {
            InitializeComponent();
            // TODO: Check if tileFolder, tileCount, etc. is correct here.
            // ErrorCheck();
            MyInit();

            handTile1.Image = deck[0].GetImage();

        }

        #region Custom methods
        // Custom initialization.
        // TODO: Maybe split into several different functions?
        private void MyInit()
        {
            // Gets the paths to all tile images.
            tilePaths = Directory.GetFiles(tileFolder);

            // Extracts the tile connection codes from the paths.
            tileCodes = new string[tileCount];
            for (int n = 0; n < tileCount; n++)
            {
                tileCodes[n] = TileCodeFromPath(tilePaths[n]);
            }

            // Creates a deck full of the tiles specified by the connection codes.
            // TODO: Shuffle the deck.
            deck = new List<Tile>();
            for (int n = 0; n < tileCount; n++)
            {
                Image image = Image.FromFile(tilePaths[n]);
                Tile tile = new Tile(tileCodes[n], image);
                deck.Add(tile);
            }

            // ---Temp test code---
            int[] sPosX = {0, 1, 5, 3};
            int[] sPosY = {3, 0, 2, 5};
            int[] tPos = {1, 6, 4, 2};
            players = new List<Player>();
            for (int n = 0; n < playerCount; n++)
            {
                currentPlayer = new Player(n, sPosX[n], sPosY[n], tPos[n]);
                players.Add(currentPlayer);
            }
            currentPlayer = players[0];
            // --------------------
            // TODO: Instead of above, implement manual selection.
            // TODO: Player positioning should be done after cards are dealt,
            //       but DealTilesFromDeck() needs players first. Fix this!

            // Deals cards to all players.
            DealTilesFromDeck();

            // Display the current players hand on the screen.
            DisplayCurrentPlayerHand();
        }

        // Returns the tile code associated with the image referred to by tilePath.
        private string TileCodeFromPath(string tilePath)
        {
            return tilePath.Substring(tileFolder.Length + 1, 8);
        }

        // Deals tiles to each player up until either all players
        // have a hand of 3 tiles, or until the deck is empty.
        private void DealTilesFromDeck()
        {
            bool allHaveFullHands = true;

            // Gives each player tiles in order.
            foreach (Player player in players)
            {
                // Deals tiles until the hand is full as long as the deck is not empty.
                while (deck.Count > 0 && player.GetHand().Count < 3)
                {
                    // Adds a tile (top tile) to the hand.
                    player.AddTileToHand(deck[deck.Count - 1]);

                    // Removes the reference/copy of the tile from the deck.
                    deck.RemoveAt(deck.Count - 1);
                }

                // If the deck was emptied before the current player got their hand,
                // they get the dragon marker (next free tile goes to them).
                if (player.GetHand().Count < 3 && deck.Count == 0)
                {
                    dragonMarkerHome = player.GetPlayerID();
                    allHaveFullHands = false;
                    break;
                }
                // TODO: Logic that makes sure the dragon marker switches owner if needed.
            }

            // If all players have full hands, the dragon marker goes back to the deck.
            if (!allHaveFullHands)
            {
                dragonMarkerHome = -1;
            }
        }

        // Displays the current players hand.
        private void DisplayCurrentPlayerHand()
        {
            switch (currentPlayer.GetHand().Count)
            {
                case 0:
                    handTile1.Image = null;
                    handTile2.Image = null;
                    handTile3.Image = null;
                    break;
                case 1:
                    handTile1.Image = currentPlayer.GetHand()[0].GetImage();
                    handTile2.Image = null;
                    handTile3.Image = null;
                    break;
                case 2:
                    handTile1.Image = currentPlayer.GetHand()[0].GetImage();
                    handTile2.Image = currentPlayer.GetHand()[1].GetImage();
                    handTile3.Image = null;
                    break;
                case 3:
                    handTile1.Image = currentPlayer.GetHand()[0].GetImage();
                    handTile2.Image = currentPlayer.GetHand()[1].GetImage();
                    handTile3.Image = currentPlayer.GetHand()[2].GetImage();
                    break;
                default:
                    break;
            }
        }

        // Attempts to select the specified hand slot (only slots with tiles can be selected).
        public void SelectHandTile(int tileNum)
        {
            switch(tileNum)
            {
                case 1:
                    if(currentPlayer.GetHand().Count > 0)
                    {
                        // Modifies tile selection color, and marks the tile for selection.
                        handTile1.BackColor = Color.OrangeRed;
                        handTile2.BackColor = SystemColors.Control;
                        handTile3.BackColor = SystemColors.Control;
                        selectedHandTile = 1;
                    }
                    break;
                case 2:
                    if (currentPlayer.GetHand().Count > 1)
                    {
                        // Modifies tile selection color, and marks the tile for selection.
                        handTile1.BackColor = SystemColors.Control;
                        handTile2.BackColor = Color.OrangeRed;
                        handTile3.BackColor = SystemColors.Control;
                        selectedHandTile = 2;
                    }
                    break;
                case 3:
                    if (currentPlayer.GetHand().Count > 2)
                    {
                        // Modifies tile selection color, and marks the tile for selection.
                        handTile1.BackColor = SystemColors.Control;
                        handTile2.BackColor = SystemColors.Control;
                        handTile3.BackColor = Color.OrangeRed;
                        selectedHandTile = 3;
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion


        private void startBtn_Click(object sender, EventArgs e)
        {
            //deck[0].RotateCW();
            //handTile1.Image = deck[0].GetImage();
        }

        #region Hand Slots
        // Selects the slot (if possible) when clicked.
        private void handTile1_Click(object sender, EventArgs e)
        {
            SelectHandTile(1);
        }
        private void handTile2_Click(object sender, EventArgs e)
        {
            SelectHandTile(2);
        }

        private void handTile3_Click(object sender, EventArgs e)
        {
            SelectHandTile(3);
        }
        #endregion

        #region Board Slots
        private void board00_Click(object sender, EventArgs e)
        {

        }

        private void board10_Click(object sender, EventArgs e)
        {

        }

        private void board20_Click(object sender, EventArgs e)
        {

        }

        private void board30_Click(object sender, EventArgs e)
        {

        }

        private void board40_Click(object sender, EventArgs e)
        {

        }

        private void board50_Click(object sender, EventArgs e)
        {

        }

        private void board01_Click(object sender, EventArgs e)
        {

        }

        private void board11_Click(object sender, EventArgs e)
        {

        }

        private void board21_Click(object sender, EventArgs e)
        {

        }

        private void board31_Click(object sender, EventArgs e)
        {

        }

        private void board41_Click(object sender, EventArgs e)
        {

        }

        private void board51_Click(object sender, EventArgs e)
        {

        }

        private void board02_Click(object sender, EventArgs e)
        {

        }

        private void board12_Click(object sender, EventArgs e)
        {

        }

        private void board22_Click(object sender, EventArgs e)
        {

        }

        private void board32_Click(object sender, EventArgs e)
        {

        }

        private void board42_Click(object sender, EventArgs e)
        {

        }

        private void board52_Click(object sender, EventArgs e)
        {

        }

        private void board03_Click(object sender, EventArgs e)
        {

        }

        private void board13_Click(object sender, EventArgs e)
        {

        }

        private void board23_Click(object sender, EventArgs e)
        {

        }

        private void board33_Click(object sender, EventArgs e)
        {

        }

        private void board43_Click(object sender, EventArgs e)
        {

        }

        private void board53_Click(object sender, EventArgs e)
        {

        }

        private void board04_Click(object sender, EventArgs e)
        {

        }

        private void board14_Click(object sender, EventArgs e)
        {

        }

        private void board24_Click(object sender, EventArgs e)
        {

        }

        private void board34_Click(object sender, EventArgs e)
        {

        }

        private void board44_Click(object sender, EventArgs e)
        {

        }

        private void board54_Click(object sender, EventArgs e)
        {

        }

        private void board05_Click(object sender, EventArgs e)
        {

        }

        private void board15_Click(object sender, EventArgs e)
        {

        }

        private void board25_Click(object sender, EventArgs e)
        {

        }

        private void board35_Click(object sender, EventArgs e)
        {

        }

        private void board45_Click(object sender, EventArgs e)
        {

        }

        private void board55_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}

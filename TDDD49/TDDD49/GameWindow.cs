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
        public int playerCount;
        private List<Player> players;
        private Player currentPlayer;
        private int dragonMarkerHome = -1;
        private int selectedHandTile = -1;
        private int selectedBoardSlotX = -1;
        private int selectedBoardSlotY = -1;
        private List<Label> playerLabels;
        private List<PictureBox> boardSlots;

        //TestComment matilda
        // Constructor.
        public GameWindow()
        {
            InitializeComponent();
            // TODO: Check if tileFolder, tileCount, etc. is correct here.
            // ErrorCheck();
            MyInit();

            //handTile1.Image = deck[0].GetImage();

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

            // Loads components into ordered lists.
            LoadPlayerLabels();
            LoadBoardSlots();

            // ---Temp test code---
            int[] sPosX = {0, 1, 5, 3};
            int[] sPosY = {3, 0, 2, 5};
            int[] tPos = {1, 6, 4, 2};
            playerCount = 4;
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

            // Displays the current players hand on the screen.
            DisplayCurrentPlayerHand();

            // Displays the text data.
            DisplayTextData();

            // Highlights where the next tile will be placed.
            HighlightBoardSlot(currentPlayer.GetBoardPosX(), currentPlayer.GetBoardPosY());
        }

        // Returns the tile code associated with the image referred to by tilePath.
        private string TileCodeFromPath(string tilePath)
        {
            return tilePath.Substring(tileFolder.Length + 1, 8);
        }

        // Loads all player labels into playerLabels, in order.
        private void LoadPlayerLabels()
        {
            playerLabels = new List<Label>();
            playerLabels.Add(player1Lbl);
            playerLabels.Add(player2Lbl);
            playerLabels.Add(player3Lbl);
            playerLabels.Add(player4Lbl);
            playerLabels.Add(player5Lbl);
            playerLabels.Add(player6Lbl);
            playerLabels.Add(player7Lbl);
            playerLabels.Add(player8Lbl);
        }

        // Loads all board slots into boardSlots, in order.
        private void LoadBoardSlots()
        {
            boardSlots = new List<PictureBox>();
            boardSlots.Add(board00);
            boardSlots.Add(board10);
            boardSlots.Add(board20);
            boardSlots.Add(board30);
            boardSlots.Add(board40);
            boardSlots.Add(board50);
            boardSlots.Add(board01);
            boardSlots.Add(board11);
            boardSlots.Add(board21);
            boardSlots.Add(board31);
            boardSlots.Add(board41);
            boardSlots.Add(board51);
            boardSlots.Add(board02);
            boardSlots.Add(board12);
            boardSlots.Add(board22);
            boardSlots.Add(board32);
            boardSlots.Add(board42);
            boardSlots.Add(board52);
            boardSlots.Add(board03);
            boardSlots.Add(board13);
            boardSlots.Add(board23);
            boardSlots.Add(board33);
            boardSlots.Add(board43);
            boardSlots.Add(board53);
            boardSlots.Add(board04);
            boardSlots.Add(board14);
            boardSlots.Add(board24);
            boardSlots.Add(board34);
            boardSlots.Add(board44);
            boardSlots.Add(board54);
            boardSlots.Add(board05);
            boardSlots.Add(board15);
            boardSlots.Add(board25);
            boardSlots.Add(board35);
            boardSlots.Add(board45);
            boardSlots.Add(board55);
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
            if (allHaveFullHands)
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

        // Displays text data on the screen (deck and player hand sizes).
        private void DisplayTextData()
        {
            deckSizeLbl.Text = deck.Count.ToString();
            for (int n = 0; n < playerCount; n++)
            {
                playerLabels[n].Text = "Tiles" + players[n].GetHand().Count.ToString();
            }
            for (int n = playerCount; n < 8; n++)
            {
                playerLabels[n].Text = "-";
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

        // Highlights an unoccupied slot on the game board, and clears any previous highlights.
        private void HighlightBoardSlot(int x, int y)
        {
            int currentlySelectedSlotX = selectedBoardSlotX;
            int currentlySelectedSlotY = selectedBoardSlotY;
            selectedBoardSlotX = x;
            selectedBoardSlotY = y;

            // Clears any old highlights.
            if (currentlySelectedSlotX != -1 && currentlySelectedSlotY != -1)
            {
                boardSlots[currentlySelectedSlotX + 6 * currentlySelectedSlotY].BackColor = SystemColors.Control;
            }

            // Highlights the new slot.
            if (selectedBoardSlotX != -1 && selectedBoardSlotY != -1)
            {
                boardSlots[selectedBoardSlotX + 6 * selectedBoardSlotY].BackColor = Color.DarkOliveGreen;
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

        private void rotCWBtn_Click(object sender, EventArgs e)
        {
            // Rotates the selected tile 90 degrees clockwise.
            if(selectedHandTile != -1)
            {
                switch(selectedHandTile)
                {
                    case 1:
                        currentPlayer.GetHand()[0].RotateCW();
                        handTile1.Image = currentPlayer.GetHand()[0].GetImage();
                        break;
                    case 2:
                        currentPlayer.GetHand()[1].RotateCW();
                        handTile2.Image = currentPlayer.GetHand()[1].GetImage();
                        break;
                    case 3:
                        currentPlayer.GetHand()[2].RotateCW();
                        handTile3.Image = currentPlayer.GetHand()[2].GetImage();
                        break;
                    default:
                        break;
                }
            }
        }

        private void rotCCWBtn_Click(object sender, EventArgs e)
        {
            // Rotates the selected tile 90 degrees clockwise.
            if (selectedHandTile != -1)
            {
                switch (selectedHandTile)
                {
                    case 1:
                        currentPlayer.GetHand()[0].RotateCCW();
                        handTile1.Image = currentPlayer.GetHand()[0].GetImage();
                        break;
                    case 2:
                        currentPlayer.GetHand()[1].RotateCCW();
                        handTile2.Image = currentPlayer.GetHand()[1].GetImage();
                        break;
                    case 3:
                        currentPlayer.GetHand()[2].RotateCCW();
                        handTile3.Image = currentPlayer.GetHand()[2].GetImage();
                        break;
                    default:
                        break;
                }
            }
        }

        private void placeTileBtn_Click(object sender, EventArgs e)
        {
            Tile placedTile = currentPlayer.GetHand()[selectedHandTile - 1];

            // Places tile on the board, thus removing it from the hand.
            boardSlots[selectedBoardSlotX + 6 * selectedBoardSlotY].Image = placedTile.GetImage();
            currentPlayer.RemoveFromHand(selectedHandTile);

            // Deals new tiles to the players hand.
            DealTilesFromDeck();
            DisplayCurrentPlayerHand();

            // Temporary current tile position is calculated.
            int oldPlayerTilePos = currentPlayer.GetTilePos() + 8;
            oldPlayerTilePos -= placedTile.GetOrientation() * 2;
            oldPlayerTilePos = oldPlayerTilePos % 8;

            // New tile position is extracted, and recalculated to compensate for tile orientation.
            int newPlayerTilePos = placedTile.GetConnection(currentPlayer.GetTilePos());
            newPlayerTilePos += placedTile.GetOrientation() * 2;
            newPlayerTilePos = newPlayerTilePos % 8;

            // Sets the new board and tile positions.
            switch(newPlayerTilePos)
            {
                case 0:
                    currentPlayer.SetBoardPosX(selectedBoardSlotX - 1);
                    break;
                case 1:
                    currentPlayer.SetBoardPosX(selectedBoardSlotX - 1);
                    break;
                case 2:
                    currentPlayer.SetBoardPosY(selectedBoardSlotY + 1);
                    break;
                case 3:
                    currentPlayer.SetBoardPosY(selectedBoardSlotY + 1);
                    break;
                case 4:
                    currentPlayer.SetBoardPosX(selectedBoardSlotX + 1);
                    break;
                case 5:
                    currentPlayer.SetBoardPosX(selectedBoardSlotX + 1);
                    break;
                case 6:
                    currentPlayer.SetBoardPosY(selectedBoardSlotY - 1);
                    break;
                case 7:
                    currentPlayer.SetBoardPosY(selectedBoardSlotY - 1);
                    break;
                default:
                    break;
            }
            if(newPlayerTilePos % 2 == 0)
            {
                newPlayerTilePos += 5;
            }
            else
            {
                newPlayerTilePos += 3;
            }
            newPlayerTilePos = newPlayerTilePos % 8;
            currentPlayer.SetTilePos(newPlayerTilePos);
            HighlightBoardSlot(currentPlayer.GetBoardPosX(), currentPlayer.GetBoardPosY());

            // ---Debug---
            player6Lbl.Text = currentPlayer.GetBoardPosX().ToString();
            player7Lbl.Text = currentPlayer.GetBoardPosY().ToString();
            player8Lbl.Text = currentPlayer.GetTilePos().ToString();
            // -----------
            // TODO: Does not handle retreading through old tiles at all.
        }
    }
}
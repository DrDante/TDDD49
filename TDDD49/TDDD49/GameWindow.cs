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

        public GameWindow()
        {
            InitializeComponent();
            // TODO: Check if tileFolder, tileCount, etc. is correct here.
            // ErrorCheck();
            MyInit();

            handTile1.Image = deck[0].GetImage();

        }
        #region Custom functions
        // Custom initialization. Maybe split into different functions?
        private void MyInit()
        {
            tilePaths = Directory.GetFiles(tileFolder);
            tileCodes = new string[tileCount];
            for (int n = 0; n < tileCount; n++)
            {
                tileCodes[n] = TileCodeFromPath(tilePaths[n]);
            }
            deck = new List<Tile>();
            for (int n = 0; n < tileCount; n++)
            {
                Image image = Image.FromFile(tilePaths[n]);
                Tile tile = new Tile(tileCodes[n], image);
                deck.Add(tile);
            }
        }

        // Returns the tile code associated with the image referred to by tilePath.
        private string TileCodeFromPath(string tilePath)
        {
            return tilePath.Substring(tileFolder.Length + 1, 8);
        }
        #endregion


        private void startBtn_Click(object sender, EventArgs e)
        {
            deck[0].RotateCW();
            handTile1.Image = deck[0].GetImage();
        }

        #region Hand Slots
        private void handTile1_Click(object sender, EventArgs e)
        {
            // TODO: Shouldn't be able to select unused slots. Separate function?
            handTile1.BackColor = Color.OrangeRed;
            handTile2.BackColor = SystemColors.Control;
            handTile3.BackColor = SystemColors.Control;
        }
        private void handTile2_Click(object sender, EventArgs e)
        {
            handTile1.BackColor = SystemColors.Control;
            handTile2.BackColor = Color.OrangeRed;
            handTile3.BackColor = SystemColors.Control;
        }

        private void handTile3_Click(object sender, EventArgs e)
        {
            handTile1.BackColor = SystemColors.Control;
            handTile2.BackColor = SystemColors.Control;
            handTile3.BackColor = Color.OrangeRed;
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

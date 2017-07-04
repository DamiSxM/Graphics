using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MazeDll;

namespace GraphicsDLL
{
    public partial class Form1 : Form
    {
        LabyPictureBox labyPic;
        PlayerPictureBox player;

        int[,] _maze;
        public Form1()
        {
            InitializeComponent();

            int taille = 61;
            _maze = new int[taille, taille];

            Maze.InitialiseTableau(_maze, taille, taille);
            Maze.GenereCheminPrimaire(_maze, taille, taille);

            Initialize();
        }

        void Initialize()
        {
            labyPic = new LabyPictureBox(_maze, 50, 9, 9);
            labyPic.Location = new Point(0, 0);

            Size = labyPic.Size;
            BackColor = Color.Black;

            player = new PlayerPictureBox();
            player.setLocation(Size);

            Controls.Add(labyPic);
            Controls.Add(player);

            player.Parent = labyPic;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            player.Moving(e.KeyCode);
            labyPic.Moving(e.KeyCode);


            switch (e.KeyCode)
            {
                case Keys.A:
                    labyPic.addOverlay(3, 3, "test");
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            player.Stop();
        }
    }
}

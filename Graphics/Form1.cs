using System;
using System.Drawing;
using System.Windows.Forms;
using MazeDll;
using System.Diagnostics;

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

            int taille = 11;
            _maze = new int[taille, taille];

            Maze.InitialiseTableau(_maze, taille, taille);
            Maze.GenereCheminPrimaire(_maze, taille, taille);

            Initialize();
        }

        void Initialize()
        {
            labyPic = new LabyPictureBox(_maze, 50, 9, 9);
            labyPic.PositionChanged += LabyPic_PositionChanged;
            labyPic.Location = new Point(0, 0);

            player = new PlayerPictureBox();
            player.setLocation(Size);

            Controls.Add(labyPic);
            Controls.Add(player);

            player.Parent = labyPic;
        }

        private void LabyPic_PositionChanged(int x, int y)
        {
            Debug.WriteLine(string.Format("X {0}, Y {1}", x, y));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            player.Moving(e.KeyCode);

            switch (e.KeyCode)
            {
                case Keys.Left:
                    labyPic.GoLeft();
                    break;
                case Keys.Up:
                    labyPic.GoUp();
                    break;
                case Keys.Right:
                    labyPic.GoRight();
                    break;
                case Keys.Down:
                    labyPic.GoDown();
                    break;
                case Keys.F1:
                    labyPic.Warfog(0);
                    break;
                case Keys.F2:
                    labyPic.Warfog(1);
                    break;
                case Keys.F3:
                    labyPic.Warfog(2);
                    break;
                case Keys.F4:
                    labyPic.Warfog(3);
                    break;
                case Keys.F5:
                    labyPic.Warfog(4);
                    break;
                case Keys.A:
                    labyPic.addItem(3, 3, "porte1");
                    break;
                case Keys.Z:
                    labyPic.removeItem(3, 3);
                    break;
                case Keys.E:
                    labyPic.addItem(4, 4, "GrilleFermee");
                    break;
                case Keys.Q:
                    labyPic.addPlayer("bob", 5, 5);
                    break;
                case Keys.S:
                    labyPic.movePlayer("bob", 5, 4);
                    break;
                case Keys.D:
                    labyPic.removePlayer("bob");
                    break;
                case Keys.D1:
                    labyPic.moveToTile(0, 0);
                    break;
                case Keys.D2:
                    labyPic.moveToTile(1, 1);
                    break;
                case Keys.D3:
                    labyPic.moveToTile(2, 2);
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            player.Stop();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Debug.WriteLine("Width {0}, Height {1}", Width, Height);
        }
    }
}

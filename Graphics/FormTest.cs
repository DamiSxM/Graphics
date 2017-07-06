using MazeDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsDLL
{
    public partial class FormTest : Form
    {
        int[,] Labyrinthe;
        AffichageWinForm Affichage;
        // Reseau Liaison;
        // MongoDB Database;

        public FormTest()
        {
            InitializeComponent();

            int LabyrintheTaille = 51;
            Labyrinthe = new int[LabyrintheTaille, LabyrintheTaille];
            Maze.InitialiseTableau(Labyrinthe, LabyrintheTaille, LabyrintheTaille);
            Maze.GenereCheminPrimaire(Labyrinthe, LabyrintheTaille, LabyrintheTaille);

            Affichage = new AffichageWinForm(Labyrinthe);
            Affichage.Location = new Point(0, 0);
            Controls.Add(Affichage);
        }

        private void FormTest_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Affichage.PersoMove(0); break;
                case Keys.Up:
                    Affichage.PersoMove(1); break;
                case Keys.Right:
                    Affichage.PersoMove(2); break;
                case Keys.Down:
                    Affichage.PersoMove(3); break;
                case Keys.A:
                    Affichage.ItemAdd(3, 3, "porte1"); break;
                case Keys.Z:
                    Affichage.ItemRemove(3, 3); break;
                case Keys.E:
                    Affichage.ItemAdd(4, 4, "GrilleFermee"); break;
                case Keys.Q:
                    Affichage.PlayerAdd("bob", 5, 5); break;
                case Keys.S:
                    Affichage.PlayerMove("bob", 5, 4); break;
                case Keys.D:
                    Affichage.PlayerRemove("bob"); break;
                case Keys.NumPad0:
                    Affichage.Warfog(0); break;
                case Keys.NumPad1:
                    Affichage.Warfog(1); break;
                case Keys.NumPad2:
                    Affichage.Warfog(2); break;
                case Keys.NumPad3:
                    Affichage.Warfog(3); break;
                case Keys.NumPad4:
                    Affichage.Warfog(4); break;
                case Keys.D1:
                    Affichage.PersoTeleport(0, 0); break;
                case Keys.D2:
                    Affichage.PersoTeleport(1, 1); break;
                case Keys.D3:
                    Affichage.PersoTeleport(3, 3); break;
            }
        }

        private void FormTest_KeyUp(object sender, KeyEventArgs e)
        {
            Affichage.PersoStop();
        }
    }
}

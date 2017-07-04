using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeDll
{
    public class Maze
    {
        public static void FenPrincipale()
        {
            int Hauteur = 61;
            int Largeur = 61;
            /*int DirHorizont = 1;
            int DirVertival = 0;*/


            // Liste a deux dimentions

            int[,] Maze = new int[Hauteur, Largeur];


            InitialiseTableau(Maze, Hauteur, Largeur);

            //CentreMaze(Maze, Hauteur, Largeur);

            GenereCheminPrimaire(Maze, Hauteur, Largeur/*, DirHorizont, DirVertival*/);

            Console.Clear();

            AffichageTableau(Maze, Hauteur, Largeur);           //Affichage du Maze[,]          

            Console.ReadLine();

        }

        public static void AffichageTableau(int[,] Maze, int Hauteur, int Largeur)
        {

            for (int H = 0; H < Hauteur; H++)
            {
                for (int L = 0; L < Largeur; L++)
                {

                    var Valeur = Maze[H, L];

                    if (Valeur == 1)
                    {
                        Console.Write("██");
                    }
                    if (Valeur == 0)
                    {
                        Console.Write("  ");
                    }

                    if (Valeur == 3)
                    {
                        Console.Write("░░");
                    }

                }

                Console.WriteLine("");
            }


        }
        public static void InitialiseTableau(int[,] Maze, int Hauteur, int Largeur)
        {

            // Initialisation de la liste 
            int Pas = 10;

            for (int H = 0; H < Hauteur; H++)
            {
                for (int L = 0; L < Largeur; L++)

                {
                    if (H % 2 != 0 && L % 2 != 0)
                    {

                        Pas++;
                        Maze[H, L] = 0;

                    }
                    else
                    {

                        Maze[H, L] = 1;

                    }
                }
            }
        }

        public static void GenereCheminPrimaire(int[,] Maze, int Hauteur, int Largeur/*, int DirHorizont, int DirVertical*/)
        {



            Random DirectionPorte = new Random();


            int H = 1;
            int L = 1;

            int EtatNord = Maze[H + 1, L];
            int EtatEst = Maze[H, L + 1];
            int EtatSud = Maze[H - 1, L];
            int EtatOuest = Maze[H, L - 1];


            #region Generation étape 1


            for (int Y = 1; Y < Hauteur - 1; Y += 2)

            {

                for (int X = 1; X < Largeur - 1; X += 2)
                {

                    H = Y;
                    L = X;

                    EtatNord = Maze[H + 1, L];
                    EtatEst = Maze[H, L + 1];
                    EtatSud = Maze[H - 1, L];
                    EtatOuest = Maze[H, L - 1];


                    if (EtatNord == 1 && EtatEst == 1 && EtatSud == 1 && EtatOuest == 1)
                    {

                        // Chemin

                        for (int i = 0; i < Hauteur * 200; i++)
                        {

                            int direction = DirectionPorte.Next(1, 4);

                            EtatNord = Maze[H + 1, L];
                            EtatEst = Maze[H, L + 1];
                            EtatSud = Maze[H - 1, L];
                            EtatOuest = Maze[H, L - 1];

                            switch (direction)

                            {

                                case 1:

                                    if (EtatNord == 1 && H + 1 < Hauteur - 1)
                                    {

                                        Maze[H + 1, L] = 0;
                                        H += 2;
                                    }

                                    break;

                                case 2:


                                    if (EtatEst == 1 && L + 1 < Largeur - 1)
                                    {

                                        Maze[H, L + 1] = 0;
                                        L += 2;

                                    }
                                    break;

                                case 3:

                                    if (EtatSud == 1 && H - 1 > 1)
                                    {

                                        Maze[H - 1, L] = 0;
                                        H -= 2;

                                    }

                                    break;

                                case 4:

                                    if (EtatOuest == 1 && L - 1 > 1)
                                    {

                                        Maze[H, L - 1] = 0;
                                        L -= 2;

                                    }

                                    break;
                            }
                        }
                    }
                }
            }

            #endregion

            #region Generation étape 2

            for (int Y = 1; Y < Hauteur - 1; Y += 1)

            {

                for (int X = 1; X < Largeur - 1; X += 1)
                {

                    H = Y;
                    L = X;

                    EtatNord = Maze[H + 1, L];
                    EtatEst = Maze[H, L + 1];
                    EtatSud = Maze[H - 1, L];
                    EtatOuest = Maze[H, L - 1];


                    if (EtatNord == 0 && EtatEst == 0 && EtatSud == 0 && EtatOuest == 0)
                    {
                        int direction = DirectionPorte.Next(1, 4);

                        EtatNord = Maze[H + 1, L];
                        EtatEst = Maze[H, L + 1];
                        EtatSud = Maze[H - 1, L];
                        EtatOuest = Maze[H, L - 1];

                        switch (direction)

                        {

                            case 1:

                                if (EtatNord == 0 && H + 1 < Hauteur - 1)
                                {

                                    Maze[H + 1, L] = 1;
                                    H += 2;
                                }

                                break;

                            case 2:


                                if (EtatEst == 0 && L + 1 < Largeur - 1)
                                {

                                    Maze[H, L + 1] = 1;
                                    L += 2;

                                }
                                break;

                            case 3:

                                if (EtatSud == 0 && H - 1 > 1)
                                {

                                    Maze[H - 1, L] = 1;
                                    H -= 2;

                                }

                                break;

                            case 4:

                                if (EtatOuest == 0 && L - 1 > 1)
                                {

                                    Maze[H, L - 1] = 1;
                                    L -= 2;

                                }

                                break;

                        }




                    }
                }

            }

            #endregion

            #region Generation étape 3

            H = Hauteur / 2;
            L = Largeur / 2;

            for (int Y = H - 1; Y < H + 2; Y++)
            {

                for (int X = L - 1; X < L + 2; X++)
                {

                    Maze[Y, X] = 0;
                }

            }

            Maze[H, L] = 3;

            #endregion

        }
    }
}

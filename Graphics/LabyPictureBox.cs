using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicsDLL
{
    class LabyPictureBox : PictureBox
    {
        int _tailleTiles;
        int _tailleMaze;

        Bitmap _labyBase;
        Bitmap _labyItems;
        Bitmap _labyPlayers;

        Hashtable _items = new Hashtable();
        Hashtable _players = new Hashtable();

        Image warfog;

        int _width;
        int _height;
        int OffsetX = 0;
        int OffsetY = 0;

        int _velocity = 2;


        public LabyPictureBox(int[,] maze, int tailleTiles, int cmbTuillesWidth, int cmbTuillesHeight) : base()
        {
            DoubleBuffered = true;
            _tailleMaze = maze.GetUpperBound(0);
            _tailleTiles = tailleTiles;
            
            NiveauWarfog(0);

            _width = cmbTuillesWidth * _tailleTiles;
            _height = cmbTuillesHeight * _tailleTiles;

            Size = new Size(_width, _height);
            Image = new Bitmap(cmbTuillesWidth * _tailleTiles, cmbTuillesHeight * _tailleTiles);

            _labyBase = new Bitmap(_tailleMaze * _tailleTiles, _tailleMaze * _tailleTiles);
            _labyItems = new Bitmap(_tailleMaze * _tailleTiles, _tailleMaze * _tailleTiles);


            using (Graphics g = Graphics.FromImage(_labyBase))
            {
                for (int x = 0; x < _tailleMaze - 1; x++)
                {
                    for (int y = 0; y < _tailleMaze - 1; y++)
                    {
                        Bitmap tmp = new Bitmap(_tailleTiles, _tailleTiles);
                        if (maze[x, y] == 0) tmp = new Bitmap(Properties.Resources.solSable);
                        if (maze[x, y] == 1) tmp = new Bitmap(Properties.Resources.murs);
                        g.DrawImage(tmp, (x * _tailleTiles), (y * _tailleTiles), _tailleTiles, _tailleTiles);
                    }
                }
            }
            RePaint();

        }


        public void addItem(int x, int y, string nom)
        {
            _items.Add(new Point(x, y), nom);
        }
        public void removeItem(int x, int y)
        {
            _items.Remove(new Point(x, y));
        }
        public void addPlayer(string ip, int x, int y)
        {
            _players.Add(ip, new Point(x, y));
        }
        public void movePlayer(string ip, int x, int y)
        {
            //_players.Remove(new Point(x, y));
            _players.Remove(new Point(x, y));
        }

        void RePaint()
        {
            using (Graphics g = Graphics.FromImage(Image))
            {
                affichageFond(g);

                affichageBitmap(g, _labyBase);      // Laby
                //affichageBitmap(g, _labyItems);   // Items
                //affichageBitmap(g, _labyPlayers);   // Players
                
                affichageFog(g);
            }
        }


        void affichageFond(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, Size.Width, Size.Height));
        }
        void affichageBitmap(Graphics g, Bitmap b)
        {
            int droite, bas;
            if (OffsetY + _height < _height) bas = _height;
            else bas = OffsetY + _height;
            if (OffsetX + _width < _width) droite = _width;
            else droite = OffsetX + _width;

            Rectangle destRect = new Rectangle(new Point(0, 0), Size);
            Rectangle srcRect = new Rectangle(OffsetX, OffsetY, droite, bas);

            g.DrawImage(b, 0, 0, srcRect, GraphicsUnit.Pixel);
        }
        void affichageFog(Graphics g)
        {
            g.DrawImage(warfog, 0, 0, Size.Width, Size.Height);
        }

        public void NiveauWarfog(int lvl)
        {
            switch (lvl)
            {
                case 0:
                    warfog = Properties.Resources.warfog_vision_1;
                    break;
                case 1:
                    warfog = Properties.Resources.warfog_vision_2;
                    break;
                case 2:
                    warfog = Properties.Resources.warfog_vision_3;
                    break;
                case 3:
                    warfog = Properties.Resources.warfog_vision_4;
                    break;
                case 4:
                    warfog = Properties.Resources.warfog_vision_5;
                    break;
                default:
                    break;
            }
        }

        public void Moving(Keys k)
        {
            switch (k)
            {
                case Keys.Left:
                    OffsetX -= _velocity;
                    break;
                case Keys.Up:
                    OffsetY -= _velocity;
                    break;
                case Keys.Right:
                    OffsetX += _velocity;
                    break;
                case Keys.Down:
                    OffsetY += _velocity;
                    break;
                case Keys.F1:
                    NiveauWarfog(0);
                    break;
                case Keys.F2:
                    NiveauWarfog(1);
                    break;
                case Keys.F3:
                    NiveauWarfog(2);
                    break;
                case Keys.F4:
                    NiveauWarfog(3);
                    break;
                case Keys.F5:
                    NiveauWarfog(4);
                    break;
            }
            RePaint();
            this.Invalidate();
        }
    }
}

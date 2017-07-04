using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace GraphicsDLL
{
    class PlayerPictureBox : PictureBox
    {
        //string _perso;
        string _direction = "Bas";
        Thread th;
        bool stopThread = false;

        public PlayerPictureBox(/*string perso*/)
        {
            //_perso = perso;
            Size = new Size(50, 50);
            BackColor = Color.Transparent;

            Image = Properties.Resources.ResourceManager.GetObject("Bas0") as Image;

            _direction = "Bas";

            Invalidate();

            th = new Thread(Loop);
        }

        private void Loop()
        {
            stopThread = false;
            int loop = 0;
            do
            {
                string name;
                lock (_direction)
                {
                    name = _direction + loop.ToString();
                }

                try
                {

                    Image img = Properties.Resources.ResourceManager.GetObject(name) as Image;
                    Image = img;
                }
                catch (Exception) { }

                Thread.Sleep(100);
                if (loop == 8) loop = 0;
                else loop++;
                Invalidate();
            } while (!stopThread);
        }

        public void Start()
        {
            if (th != null)
            {
                switch (th.ThreadState)
                {
                    case ThreadState.Running:
                        break;
                    case ThreadState.StopRequested:
                        break;
                    case ThreadState.SuspendRequested:
                        break;
                    case ThreadState.Background:
                        break;
                    case ThreadState.Unstarted:
                        th.Start();
                        break;
                    case ThreadState.Stopped:
                        th = new Thread(Loop);
                        th.Start();
                        break;
                    case ThreadState.WaitSleepJoin:
                        break;
                    case ThreadState.Suspended:
                        break;
                    case ThreadState.AbortRequested:
                        break;
                    case ThreadState.Aborted:
                        break;
                    default:
                        break;
                }
            }
        }
        public void Stop()
        {
            stopThread = true;
            th.Join();
            /*Image = Properties.Resources.ResourceManager.GetObject("Bas0") as Image;
            Invalidate();*/
        }

        void RePaint()
        {

        }

        public void setLocation(Size s)
        {
            int centerX = (s.Width / 2) - (Size.Width / 2);
            int centerY = (s.Height / 2) - (Size.Height / 2);
            Location = new Point(centerX, centerY);
        }

        public void Moving(Keys k)
        {
            lock (_direction)
            {
                switch (k)
                {
                    case Keys.Left:
                        _direction = "Gauche";
                        break;
                    case Keys.Up:
                        _direction = "Haut";
                        break;
                    case Keys.Right:
                        _direction = "Droite";
                        break;
                    case Keys.Down:
                        _direction = "Bas";
                        break;
                }
            }
            Start();
        }
    }
}

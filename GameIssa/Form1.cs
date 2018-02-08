using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameIssa
{
    public partial class Form1 : Form
    {
        Bitmap bmpMain;
        Bitmap bmpBall = new Bitmap(15, 15);
        Bitmap bmpTarget = new Bitmap(Properties.Resources.Target, new Size(25, 25));
        Bitmap bmpExplosion = new Bitmap(Properties.Resources.Explosion, new Size(60, 60));

        Graphics grpMain;
        Graphics grpBall;

        bool shouldMoveGun = false;
        bool isBorderWall = false;
        bool isGameOver = false;

        Pen penGun;
        Point pntGunHead;
        Point pntGunBottom;
        const float gunLength = 70;
        float alpha;

        float initialBallSpeedFactor = 0.10f;
        float ballSpeedFactor;
        RectangleF recfBall = new RectangleF(0, 0, 5, 5);
        Rectangle rectTarget;

        int score = 0;
        int bestScore = 0;

        Point mousePoint;
        float xDiff;
        float yDiff;

        Ball ball;
        List<Ball> balls = new List<Ball>();

        Font fntText = new Font("Arial", 15);

        CheckBox chkWall;
        CheckBox chkMovingGun;
        Button btnStart;
        public Form1()
        {
            InitializeComponent();

            bmpMain = new Bitmap(picMain.Width, picMain.Height);
            grpMain = Graphics.FromImage(bmpMain);
            grpBall = Graphics.FromImage(bmpBall);
            grpBall.FillEllipse(Brushes.Black, 0, 0, bmpBall.Width, bmpBall.Height);

            penGun = new Pen(Color.Black, 20);
            pntGunBottom = new Point(0, picMain.Bottom);
            ballSpeedFactor = initialBallSpeedFactor;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            ShowStartMenut();
        }

        private void ShowStartMenut()
        {
            chkWall = new CheckBox();
            chkWall.Text = "Wall";
            chkWall.Location = new Point(200, 200);
            picMain.Controls.Add(chkWall);
            chkWall.CheckedChanged += new System.EventHandler(this.chkWall_CheckedChanged);

            chkMovingGun = new CheckBox();
            chkMovingGun.Text = "Moving Gun";
            chkMovingGun.Location = new Point(200, 250);
            picMain.Controls.Add(chkMovingGun);
            chkMovingGun.CheckedChanged += new System.EventHandler(this.chkMovingGun_CheckedChanged);

            btnStart = new Button();
            btnStart.Text = "Start";
            btnStart.Location = new Point(200, 300);
            picMain.Controls.Add(btnStart);
            btnStart.Click += new System.EventHandler(this.btnStart_Click);
        }

        private void picMain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bmpMain, 0, 0);
        }

        private void picMain_MouseMove(object sender, MouseEventArgs e)
        {
            mousePoint = e.Location;

            rectTarget = new Rectangle(mousePoint.X - bmpTarget.Width / 2, mousePoint.Y - bmpTarget.Height / 2, bmpTarget.Width, bmpTarget.Height);
        }

        private void tmrMoveBall_Tick(object sender, EventArgs e)
        {
            grpMain.Clear(picMain.BackColor);

            // show scores:
            grpMain.DrawString(string.Format("Score: {0}\nBest Score: {1}", score, bestScore), fntText, Brushes.Gray, new PointF(300, 50));

            // show target image:
            //grpMain.DrawImage(bmpTarget, rectTarget);

            // show gun:
            //grpMain.DrawLine(penGun, new PointF(0, 0), new PointF((float)x, (float)y));
            grpMain.DrawLine(penGun, pntGunBottom, pntGunHead);

            foreach (Ball currentBall in balls)
            {
                if (!currentBall.IsOut(picMain))
                {
                    // move and show balls
                    currentBall.Move(bmpBall, grpMain, picMain);

                    // if lose:
                    if (currentBall.IsTouched(rectTarget))
                    {
                        tmrMoveBall.Stop();
                        tmrShoot.Stop();
                        tmrMoveGun.Stop();

                        if (score > bestScore)
                        {
                            bestScore = score;
                        }

                        isGameOver = true;
                    }
                }
            }

            if (isGameOver)
            {
                // draw explosion image at the mouse point
                grpMain.DrawImage(bmpExplosion, new Point(mousePoint.X - bmpExplosion.Width / 2, mousePoint.Y - bmpExplosion.Height / 2));
                grpMain.DrawString("Game Over\nClick to start again...", fntText, Brushes.Red, new PointF(275, 200));
            }

            picMain.Refresh();
        }

        private void tmrShoot_Tick(object sender, EventArgs e)
        {
            ball = new Ball(10, new RectangleF(pntGunHead.X - bmpBall.Width / 2, pntGunHead.Y - bmpBall.Height / 2, bmpBall.Width, bmpBall.Height), new PointF(pntGunHead.X - pntGunBottom.X, pntGunHead.Y - pntGunBottom.Y), isBorderWall, ballSpeedFactor);
            balls.Add(ball);
            score++;

            ballSpeedFactor += 0.001f; // increase speed of balls
        }

        private void tmrMoveGun_Tick(object sender, EventArgs e)
        {
            xDiff = (mousePoint.X - pntGunBottom.X);
            yDiff = (mousePoint.Y - pntGunBottom.Y);
            alpha = (float)Math.Atan(Math.Abs(yDiff / xDiff));
            float gunXLength = (float)Math.Cos(alpha) * gunLength;
            float gunYLength = (float)Math.Sin(alpha) * gunLength;

            int gunHeadNewX = pntGunHead.X;
            int gunHeadNewY = pntGunHead.Y;

            int gunBottomNewX = pntGunBottom.X;
            int gunBottomNewY = pntGunBottom.Y;

            if (pntGunBottom.Y == picMain.Bottom && pntGunBottom.X < picMain.Right) // bottom side
            {
                if (shouldMoveGun)
                    gunBottomNewX++; // move toward right

                // set gun head Points:
                if (mousePoint.X > pntGunBottom.X) // mouse cursor is at right side of gun
                {
                    gunHeadNewX = (int)(pntGunBottom.X + gunXLength);
                }
                else // mouse cursor is at left side of gun
                {
                    gunHeadNewX = (int)(pntGunBottom.X - gunXLength);
                }
                gunHeadNewY = (int)(picMain.Height - gunYLength);
            }
            else if (pntGunBottom.X == picMain.Right && pntGunBottom.Y > picMain.Top) // right side
            {
                if (shouldMoveGun)
                    gunBottomNewY--; // move toward up

                // set gun head Points:
                gunHeadNewX = (int)(picMain.Right - gunXLength);
                if (mousePoint.Y < pntGunBottom.Y) // mouse cursor is at up side of gun
                {
                    gunHeadNewY = (int)(pntGunBottom.Y - gunYLength);
                }
                else // mouse cursor is at bottom side of gun
                {
                    gunHeadNewY = (int)(pntGunBottom.Y + gunYLength);
                }
            }
            else if (pntGunBottom.Y == picMain.Top && pntGunBottom.X > picMain.Left) // top side
            {
                if (shouldMoveGun)
                    gunBottomNewX--; // move toward left

                // set gun head Points:
                if (mousePoint.X > pntGunBottom.X) // mouse cursor is at right side of gun
                {
                    gunHeadNewX = (int)(pntGunBottom.X + gunXLength);
                }
                else // mouse cursor is at left side of gun
                {
                    gunHeadNewX = (int)(pntGunBottom.X - gunXLength);
                }
                gunHeadNewY = (int)gunYLength;
            }
            else if (pntGunBottom.X == picMain.Left && pntGunBottom.X < picMain.Bottom) // left side
            {
                if (shouldMoveGun)
                    gunBottomNewY++; // move toward bottom

                // set gun head Point:
                gunHeadNewX = (int)gunXLength;
                if (mousePoint.Y < pntGunBottom.Y) // mouse cursor is at up side of gun
                {
                    gunHeadNewY = (int)(pntGunBottom.Y - gunYLength);
                }
                else // mouse cursor is at bottom side of gun
                {
                    gunHeadNewY = (int)(pntGunBottom.Y + gunYLength);
                }
            }

            pntGunHead = new Point(gunHeadNewX, gunHeadNewY);
            pntGunBottom = new Point(gunBottomNewX, gunBottomNewY);
        }

        private void chkWall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWall.Checked)
            {
                isBorderWall = true;
            }
            else
            {
                isBorderWall = false;
            }
        }
        private void chkMovingGun_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMovingGun.Checked)
            {
                shouldMoveGun = true;
            }
            else
            {
                shouldMoveGun = false;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (tmrMoveBall.Enabled == false && tmrShoot.Enabled == false && tmrMoveGun.Enabled == false)
            {
                HideStartMenu();
                picMain.Cursor = new Cursor(Properties.Resources.icoTarget1.Handle);

                // reset to start again:
                balls.Clear();
                score = 0;
                isGameOver = false;

                tmrShoot.Start();
                tmrMoveBall.Start();
                tmrMoveGun.Start();
            }
        }

        private void HideStartMenu()
        {
            // remove controls:
            picMain.Controls.Remove(chkWall);
            picMain.Controls.Remove(chkMovingGun);
            picMain.Controls.Remove(btnStart);
        }

        private void picMain_Click(object sender, EventArgs e)
        {
            if (isGameOver)
            {
                // reset to start again:
                balls.Clear();
                score = 0;
                isGameOver = false;
                ballSpeedFactor = initialBallSpeedFactor;

                tmrShoot.Start();
                tmrMoveBall.Start();
                tmrMoveGun.Start();
            }
        }
    }
}

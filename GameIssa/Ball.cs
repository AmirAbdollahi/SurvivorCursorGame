using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GameIssa
{
    class Ball
    {
        float size;
        RectangleF recfBall;
        PointF pntDirection;
        bool wallBorder;
        float speed;

        public Ball(float theSize, RectangleF theRecf, PointF theDirection, bool isBorderWall, float ballSpeed)
        {
            size = theSize;
            recfBall = theRecf;
            pntDirection = theDirection;
            wallBorder = isBorderWall;
            speed = ballSpeed;
        }

        public void Move(Bitmap bmpBall, Graphics g, PictureBox pic)
        {
            recfBall = new RectangleF(recfBall.X + pntDirection.X * speed, recfBall.Y + pntDirection.Y * speed, recfBall.Width, recfBall.Height);

            if (wallBorder)
            {
                if (recfBall.Top <= pic.Top) // ball hits top side
                {
                    pntDirection = new PointF(pntDirection.X, pntDirection.Y * -1);
                }
                else if (recfBall.Right >= pic.Right) // ball hits right side
                {
                    pntDirection = new PointF(pntDirection.X * -1, pntDirection.Y);
                }
                else if (recfBall.Bottom >= pic.Bottom) // ball hits bottom side
                {
                    pntDirection = new PointF(pntDirection.X, pntDirection.Y * -1);
                }
                else if (recfBall.Left <= pic.Left) // ball hits left side
                {
                    pntDirection = new PointF(pntDirection.X * -1, pntDirection.Y);
                }
            }

            g.DrawImage(bmpBall, recfBall);
        }

        public bool IsOut(PictureBox pictureBox)
        {
            if (recfBall.Left > pictureBox.Right || recfBall.Top > pictureBox.Bottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTouched(Rectangle rectTarget)
        {
            int margin = rectTarget.Width / 2;

            if(this.recfBall.Bottom > rectTarget.Top + margin && this.recfBall.Top < rectTarget.Bottom - margin &&
               this.recfBall.Right > rectTarget.Left + margin && this.recfBall.Left < rectTarget.Right - margin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

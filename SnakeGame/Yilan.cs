using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Yilan
    {
        YilanParcalari[] yilanParca;
        int yilanBuyuklugu;
        Yon yonumuz;
        public Yilan()
        {
            yilanParca = new YilanParcalari[3];
            yilanBuyuklugu = 3;
            yilanParca[0] = new YilanParcalari(150, 150);
            yilanParca[1] = new YilanParcalari(160, 150);
            yilanParca[2] = new YilanParcalari(170, 150);
        }
        public void Ilerle(Yon yon)
        {
            yonumuz = yon;
            if (yon._x==0&&yon._y==0)
            {

            }
            else
            {
                for (int i = yilanParca.Length - 1; i > 0; i--)
                {
                    yilanParca[i] = new YilanParcalari(yilanParca[i - 1].x_, yilanParca[i - 1].y_);
                }
                yilanParca[0] = new YilanParcalari(yilanParca[0].x_ + yon._x, yilanParca[0].y_ + yon._y);
            }
        }
        public void Buyu()
        {
            Array.Resize(ref yilanParca, yilanParca.Length + 1);
            yilanParca[yilanParca.Length - 1] = new YilanParcalari(yilanParca[yilanParca.Length - 2].x_ - yonumuz._x, yilanParca[yilanParca.Length - 2].y_ - yonumuz._y);
            yilanBuyuklugu++;
        }
        public Point GetPos (int number)
        {
            return new Point(yilanParca[number].x_,yilanParca[number].y_);
        }
        public int YilanBuyuklugu
        {
            get
            {
                return yilanBuyuklugu;
            }
        }
    }

    class YilanParcalari
    {
        public int x_;
        public int y_;
        public readonly int size_x;
        public readonly int size_y;
        public YilanParcalari(int x,int y)
        {
            x_ = x;
            y_ = y;
            size_x = 10;
            size_y = 10;

        }
    }

    class Yon
    {
        public readonly int _x;
        public readonly int _y;
        public Yon(int x,int y)
        {
            _x = x;
            _y = y;
        }
    }
}

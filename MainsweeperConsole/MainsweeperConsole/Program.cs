using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainsweeperConsole
{
    public class Point
    {
        public double GetDistanceTo(Point point)
        {
            return Math.Sqrt(Math.Pow(X - point.X, 2) + Math.Pow(Y - point.Y, 2));
        }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y, p1.IsMine && p2.IsMine);
        }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }

        public override bool Equals(object obj)
        {
            Point input = obj as Point;
            return input.X == X && input.Y == Y;
        }
        public int MineAround;
        public bool IsMine;
        public int X, Y;
        public Point(int x, int y, bool mine)
        {
            X = x;
            Y = y;
            IsMine = mine;
        }
    }

    public class Mainsweeper
    {


        public List<Point> GameField;
        public Mainsweeper(int xLenght, int yLenght, int minaCount, Point notGenerate)
        {
            double k = 10 / xLenght * yLenght;
            GameField = new List<Point>();
            Random random = new Random();
            int x, y = 0;
            Point point;
            for (int i = 0; i < minaCount; i++)
            {
                point = new Point(random.Next(0, xLenght), random.Next(0, xLenght), true);
                while(GameField.Contains(point) || point.Equals(notGenerate))
                    point = new Point(random.Next(0, xLenght), random.Next(0, xLenght), true);
                GameField.Add(point);
            }
        }

        public Point OpenPoint(Point point)
        {
            Point final = new Point(point.X, point.Y, false);
            if (GameField.Any(k => k.Equals(point) && k.IsMine))
            {
                return null;
            }
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {

                    if (GameField.Any(k => k.Equals(point + new Point(i, j, false)) && k.IsMine))
                    {
                        final.MineAround++;
                    }
                }
            }
            return final;
        }

        public void OpenPoints(Point start)
        {

        }

    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}

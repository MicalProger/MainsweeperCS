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

        public override bool Equals(object obj)
        {
            Point input = obj as Point;
            return input.X == X && input.Y == Y;
        }

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
        public Mainsweeper(int xLenght, int yLenght, int minaCount)
        {
            double k = 10 / xLenght * yLenght;
            GameField = new List<Point>();
            Random random = new Random();
            int x, y = 0;
            Point point;
            for (int i = 0; i < minaCount; i++)
            {
                point = new Point(random.Next(0, xLenght), random.Next(0, xLenght), true);
                while(GameField.Contains(point))
                    point = new Point(random.Next(0, xLenght), random.Next(0, xLenght), true);
                GameField.Add(point);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}

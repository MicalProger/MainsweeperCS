using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainsweeperGame
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

        Stopwatch time;
        public readonly int width, height, mines, attempts;
        public List<Point> GameField;
        public Point GetPointByPosition(Point point)
        {
            return GameField.FirstOrDefault(i => i.X == point.X && i.Y == point.Y);
        }
        public Mainsweeper(int xLenght, int yLenght, int mines, Point notGenerate)
        {
            width = xLenght;
            height = yLenght;
            this.mines = mines;
            double k = 10 / xLenght * yLenght;
            GameField = new List<Point>();
            Random random = new Random();
            Point point;
            for (int i = 0; i < mines; i++)
            {
                point = new Point(random.Next(0, xLenght), random.Next(0, yLenght), true);
                while (GameField.Contains(point) || point.Equals(notGenerate))
                    point = new Point(random.Next(0, xLenght), random.Next(0, yLenght), true);
                GameField.Add(point);
            }
            OpenPoints(notGenerate);
        }

        public void Restart(Point notGenerate)
        {
            double k = 10 / width * height;
            GameField = new List<Point>();
            Random random = new Random();
            Point point;
            for (int i = 0; i < mines; i++)
            {
                point = new Point(random.Next(0, width), random.Next(0, height), true);
                while (GameField.Contains(point) || point.Equals(notGenerate))
                    point = new Point(random.Next(0, width), random.Next(0, height), true);
                GameField.Add(point);
            }
            OpenPoints(notGenerate);
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
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    Point tmpPoint = start + new Point(i, j, false);
                    if (OpenPoint(tmpPoint) == null)
                        continue;
                    if (OpenPoint(tmpPoint).MineAround == 0)
                        GameField.Add(OpenPoint(tmpPoint));
                    else
                        OpenPoint(tmpPoint);
                }
            }
        }

    }
}

namespace SnakeGame
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate()
        {
            X = Console.WindowWidth / 2;
            Y = Console.WindowHeight / 2;
        }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
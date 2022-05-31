namespace SnakeGame
{
    public abstract class Meal
    {
        public Coordinate Position { get; set; }

        public Meal()
        {
            Random rand = new Random();
            var x = rand.Next(2, Console.WindowWidth - 2);
            var y = rand.Next(2, Console.WindowHeight - 2);
            Position = new Coordinate(x, y);
            Draw();
        }

        public abstract void Draw();
    }
}
namespace SnakeGame
{
    public class Nutrient : Meal
    {
        public override void Draw()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("$");
        }
    }
}
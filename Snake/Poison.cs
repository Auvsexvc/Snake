namespace SnakeGame
{
    public class Poison : Meal
    {
        public override void Draw()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("#");
        }
    }
}
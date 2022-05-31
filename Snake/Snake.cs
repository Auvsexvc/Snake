namespace SnakeGame
{
    public class Snake : ISnake
    {
        public int Length { get; set; } = 1;
        public Direction Direction { get; set; } = Direction.Right;
        public Coordinate HeadPosition { get; set; } = new Coordinate();
        public List<Coordinate> Tail { get; set; } = new List<Coordinate>();
        public bool OutOfRange { get; set; } = false;

        public void Eat(Meal meal)
        {
            if (meal.GetType() == typeof(Nutrient))
                Length++;
            else
            {
                Console.SetCursorPosition(Tail.FirstOrDefault().X, Tail.FirstOrDefault().Y);
                Console.Write(" ");
                Tail.RemoveAt(0);
                Length--;
            }
        }

        public void Move()
        {
            switch (Direction)
            {
                case Direction.Left:
                    HeadPosition.X--;
                    break;

                case Direction.Right:
                    HeadPosition.X++;
                    break;

                case Direction.Up:
                    HeadPosition.Y--;
                    break;

                case Direction.Down:
                    HeadPosition.Y++;
                    break;
            }
            if ((HeadPosition.X >= 1 && HeadPosition.X <= Console.BufferWidth - 2) && (HeadPosition.Y >= 1 && HeadPosition.Y <= Console.BufferHeight - 2))
            {
                Console.SetCursorPosition(HeadPosition.X, HeadPosition.Y);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("@");
                Tail.Add(new Coordinate(HeadPosition.X, HeadPosition.Y));
                if (Tail.Count > this.Length)
                {
                    var endTail = Tail.FirstOrDefault();
                    Console.SetCursorPosition(endTail.X, endTail.Y);
                    Console.Write(" ");
                    Tail.Remove(endTail);
                }
            }
            else
            {
                OutOfRange = true;
            }
        }
    }
}
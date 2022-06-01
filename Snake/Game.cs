namespace SnakeGame
{
    internal class Game
    {
        private ISnake snake;
        private int origRow;
        private int origCol;

        public bool Exit { get; set; } = false;
        public bool QuitOnDemand { get; set; } = false;
        public bool GameOver => snake.Tail.Where(t => t.X == snake.HeadPosition.X && t.Y == snake.HeadPosition.Y).ToList().Count > 1;
        public DateTime Time { get; set; } = DateTime.Now;
        public double Speed { get; set; } = 1000 / 5.0;
        public int MealsAtStart { get; set; } = 10;
        public List<Meal> meals { get; set; } = new List<Meal>();

        public Game()
        {
            DrawBox();
            snake = new Snake();
            for (int i = 0; i < MealsAtStart; i++)
                meals.Add(new Nutrient());
        }

        public void InitControls()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.Escape:
                        QuitOnDemand = true;
                        break;

                    case ConsoleKey.LeftArrow:
                        if (snake.Direction != Direction.Right) snake.Direction = Direction.Left;
                        break;

                    case ConsoleKey.RightArrow:
                        if (snake.Direction != Direction.Left) snake.Direction = Direction.Right;
                        break;

                    case ConsoleKey.UpArrow:
                        if (snake.Direction != Direction.Down) snake.Direction = Direction.Up;
                        break;

                    case ConsoleKey.DownArrow:
                        if (snake.Direction != Direction.Up) snake.Direction = Direction.Down;
                        break;
                }
            }
        }

        public void Play()
        {
            if (TimeLapse() >= Speed)
            {
                snake.Move();

                for (int m = 0; m < meals.Count; m++)
                {
                    if (meals[m].Position.X == snake.HeadPosition.X && meals[m].Position.Y == snake.HeadPosition.Y)
                    {
                        snake.Eat(meals[m]);
                        meals.Add(new Nutrient());
                        meals.Add(new Poison());
                        SpeedUp();
                    }
                }

                if (GameOver || snake.OutOfRange || QuitOnDemand || snake.Length < 1)
                {
                    Console.Clear();
                    int score = snake.Length <= 1 ? 0 : snake.Length;
                    Console.WriteLine($"Game Over. Your score is {score}.\nHit Enter to exit.");
                    Console.ReadLine();
                    Exit = true;
                }

                Update();
            }
        }

        private void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        private void DrawBox()
        {
            Console.CursorVisible = false;
            WriteAt("┌", 0, 0);
            WriteAt("└", 0, Console.WindowHeight - 1);
            WriteAt("┐", Console.WindowWidth - 1, 0);
            WriteAt("┘", Console.WindowWidth - 1, Console.WindowHeight - 1);
            for (int i = 1; i < Console.WindowHeight - 1; i++)
            {
                WriteAt("│", 0, i);
                WriteAt("│", Console.WindowWidth - 1, i);
            }
            for (int i = 1; i < Console.WindowWidth - 1; i++)
            {
                WriteAt("─", i, 0);
                WriteAt("─", i, Console.WindowHeight - 1);
            }
        }

        private void SpeedUp()
        {
            Speed /= 1.1;
        }

        private double TimeLapse()
        {
            return (DateTime.Now - Time).TotalMilliseconds;
        }

        private void Update()
        {
            Time = DateTime.Now;
        }
    }
}
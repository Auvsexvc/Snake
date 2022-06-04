using SnakeGame.Interfaces;

namespace SnakeGame
{
    internal class Game
    {
        private ISnake _snake;

        private int origRow;
        private int origCol;

        public bool Exit { get; set; } = false;
        public bool QuitOnDemand { get; set; } = false;
        public bool GameOver => _snake.Tail.Where(t => t.X == _snake.HeadPosition.X && t.Y == _snake.HeadPosition.Y).ToList().Count > 1;
        public DateTime Time { get; set; } = DateTime.Now;
        public double Speed { get; set; } = 1000 / 5.0;
        public int MealsAtStart { get; set; } = 10;
        public List<Meal> meals { get; set; } = new List<Meal>();

        public Game()
        {
            DrawBox();
            _snake = new Snake();
            for (int i = 0; i < MealsAtStart; i++)
            {
                meals.Add(new Nutrient());
            }
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
                        if (_snake.Direction != Direction.Right) _snake.Direction = Direction.Left;
                        break;

                    case ConsoleKey.RightArrow:
                        if (_snake.Direction != Direction.Left) _snake.Direction = Direction.Right;
                        break;

                    case ConsoleKey.UpArrow:
                        if (_snake.Direction != Direction.Down) _snake.Direction = Direction.Up;
                        break;

                    case ConsoleKey.DownArrow:
                        if (_snake.Direction != Direction.Up) _snake.Direction = Direction.Down;
                        break;
                }
            }
        }

        public void Play()
        {
            if (TimeLapse() >= Speed)
            {
                _snake.Move();

                for (int m = 0; m < meals.Count; m++)
                {
                    if (meals[m].Position.X == _snake.HeadPosition.X && meals[m].Position.Y == _snake.HeadPosition.Y)
                    {
                        _snake.Eat(meals[m]);
                        meals.Add(new Nutrient());
                        meals.Add(new Poison());
                        SpeedUp();
                    }
                }

                if (GameOver || _snake.OutOfRange || QuitOnDemand || _snake.Length < 1)
                {
                    Console.Clear();
                    int score = _snake.Length <= 1 ? 0 : _snake.Length;
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

        private void SpeedUp() => Speed /= 1.1;

        private double TimeLapse() => (DateTime.Now - Time).TotalMilliseconds;

        private void Update() => Time = DateTime.Now;
    }
}
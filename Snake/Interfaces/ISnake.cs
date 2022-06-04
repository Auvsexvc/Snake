namespace SnakeGame.Interfaces
{
    public interface ISnake
    {
        public Coordinate HeadPosition { get; set; }
        public int Length { get; set; }
        public bool OutOfRange { get; set; }
        public Direction Direction { get; set; }
        public List<Coordinate> Tail { get; set; }

        void Move();

        void Eat(Meal meat);
    }
}
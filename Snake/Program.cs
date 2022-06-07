// See https://aka.ms/new-console-template for more information

using SnakeGame;

Game game = new();

while (!game.Exit)
{
    game.InitControls();
    game.Play();
}
using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;
using RoguelikeGame.Patterns.Builder;

namespace RoguelikeGame.Patterns.Interpreter;

/// <summary>
/// WALL COMMAND: команда для добавления стены
/// </summary>
public class WallCommand : ICommand
{
    // координаты стены
    private int _x, _y;
    public WallCommand(int x, int y) { _x = x; _y = y; }
    // выполнить: добавить стену
    public void Execute(GameManager gm, Level level) => level.Walls.Add(new Wall(_x, _y));
}
using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;
using RoguelikeGame.Patterns.Builder;

namespace RoguelikeGame.Patterns.Interpreter;

/// <summary>
/// DOOR COMMAND: команда для создания двери
/// </summary>
public class DoorCommand : ICommand
{
    // координаты выхода
    private int _x, _y;
    public DoorCommand(int x, int y) { _x = x; _y = y; }
    // выполнить: создать дверь
    public void Execute(GameManager gm, Level level) => level.Door = new Door(_x, _y);
}

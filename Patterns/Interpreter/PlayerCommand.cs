using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;
using RoguelikeGame.Patterns.Builder;

namespace RoguelikeGame.Patterns.Interpreter;

/// <summary>
/// PLAYER COMMAND: команда для создания игрока
/// </summary>
public class PlayerCommand : ICommand
{
    // координаты способа
    private int _x, _y;
    public PlayerCommand(int x, int y) { _x = x; _y = y; }
    // выполнить: создать нового игрока
    public void Execute(GameManager gm, Level level) => level.Player = new Player(_x, _y, gm);
}
using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;
using RoguelikeGame.Patterns.Builder;

namespace RoguelikeGame.Patterns.Interpreter;

/// <summary>
/// KEY COMMAND: команда для создания ключа
/// </summary>
public class KeyCommand : ICommand
{
    // координаты ключа
    private int _x, _y;
    public KeyCommand(int x, int y) { _x = x; _y = y; }
    // выполнить: создать ключ
    public void Execute(GameManager gm, Level level) => level.Key = new Key(_x, _y);
}
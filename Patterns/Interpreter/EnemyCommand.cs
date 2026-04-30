using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;
using RoguelikeGame.Patterns.Builder;

namespace RoguelikeGame.Patterns.Interpreter;

/// <summary>
/// ENEMY COMMAND: команда для создания врагов разных типов
/// Использует Abstract Factory для создания врагов
/// </summary>
public class EnemyCommand : ICommand
{
    // тип врага (RANDOM или PATROL)
    private string _type;
    // координаты врага
    private int _x, _y;
    
    public EnemyCommand(string type, int x, int y)
    {
        _type = type;
        _x = x;
        _y = y;
    }
    
    // Выполнить: создать врага нужного типа и добавить на уровень
    public void Execute(GameManager gm, Level level)
    {
        // в зависимости от типа создаём нужного врага
        if (_type == "RANDOM")
            level.Enemies.Add(new RandomEnemy(_x, _y));
        else if (_type == "PATROL")
            level.Enemies.Add(new PatrolEnemy(_x, _y));
    }
}

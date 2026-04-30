using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;
using RoguelikeGame.Patterns.AbstractFactory;
namespace RoguelikeGame.Patterns.Builder;

/// <summary>
/// SIMPLE LEVEL BUILDER: конкретная реализация простого строителя уровней
/// </summary>
public class SimpleLevelBuilder : ILevelBuilder
{
    // готовый уровень
    private Level _level = new();
    
    public void Reset() => _level = new Level();
    
    // Построим стены (стены края + внутренние по дефолту)
    public void BuildWalls()
    {
        for (int i = 0; i < 20; i++)
        {
            _level.Walls.Add(new Wall(i, 0));
            _level.Walls.Add(new Wall(i, 19));
            _level.Walls.Add(new Wall(0, i));
            _level.Walls.Add(new Wall(19, i));
        }
        
        for (int i = 5; i < 10; i++)
        {
            _level.Walls.Add(new Wall(10, i));
        }
        for (int i = 10; i < 15; i++)
        {
            _level.Walls.Add(new Wall(5, i));
        }
    }
    
    public void BuildPlayer()
    {
        // создаем игрока на начальных координатах
        _level.Player = new Player(2, 2, GameManager.Instance);
    }
    
    public void BuildKey()
    {
        // создаем ключ в далеком конце уровня
        _level.Key = new Key(17, 17);
    }
    
    public void BuildDoor()
    {
        // создаем дверь на координатах 1,1
        _level.Door = new Door(1, 1);
    }
    
    public void BuildEnemies()
    {
        // создаем врагов с помощью фабрик (Абстракт Фабрика)
        var randomFactory = new RandomEnemyFactory();
        var patrolFactory = new PatrolEnemyFactory();
        
        _level.Enemies.Add(randomFactory.CreateEnemy(5, 5));
        _level.Enemies.Add(randomFactory.CreateEnemy(14, 5));
        _level.Enemies.Add(patrolFactory.CreateEnemy(3, 12));
        _level.Enemies.Add(patrolFactory.CreateEnemy(15, 15));
    }
    
    public Level GetResult() => _level;
}
using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;

namespace RoguelikeGame.Patterns.Builder;

public class SimpleLevelBuilder : ILevelBuilder
{
    private Level _level = new();
    
    public void Reset() => _level = new Level();
    
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
        _level.Player = new Player(2, 2, GameManager.Instance);
    }
    
    public void BuildKey()
    {
        _level.Key = new Key(17, 17);
    }
    
    public void BuildDoor()
    {
        _level.Door = new Door(1, 1);
    }
    
    public void BuildEnemies()
    {
        _level.Enemies.Add(new RandomEnemy(5, 5));
        _level.Enemies.Add(new RandomEnemy(14, 5));
        _level.Enemies.Add(new PatrolEnemy(3, 12));
        _level.Enemies.Add(new PatrolEnemy(15, 15));
    }
    
    public Level GetResult() => _level;
}
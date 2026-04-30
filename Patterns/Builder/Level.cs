using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;

namespace RoguelikeGame.Patterns.Builder;

/// <summary>
/// LEVEL: результат построения Builder'a
/// хранит все элементы уровня
/// </summary>
public class Level
{
    // компоненты уровня
    public List<Wall> Walls { get; set; } = new();
    public Player? Player { get; set; }
    public Key? Key { get; set; }
    public List<Enemy> Enemies { get; set; } = new();
    public Door? Door { get; set; }
    
    public void Setup(GameManager gm)
    {
        gm.Root.Clear();
        
        foreach (var wall in Walls)
            gm.Root.Add(wall);
        
        if (Door != null)
            gm.Root.Add(Door);
        
        if (Key != null)
            gm.Root.Add(Key);
        
        foreach (var enemy in Enemies)
            gm.Root.Add(enemy);
        
        gm.Player = Player;
        if (Player != null)
            gm.Root.Add(Player);
    }
}
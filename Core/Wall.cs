namespace RoguelikeGame.Core;

/// <summary>
/// WALL: стена, препятствие
/// Нельзя пройти ни а
/// </summary>
public class Wall : IGameObject
{
    public int X { get; set; }
    public int Y { get; set; }
    // стена твёрдая - блокирует проход
    public bool IsSolid => true;
    
    public Wall(int x, int y)
    {
        X = x;
        Y = y;
    }
}
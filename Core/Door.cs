namespace RoguelikeGame.Core;

/// <summary>
/// DOOR: выход из подземелья
/// Ходить докуда можно чим с ключом
/// </summary>
public class Door : IGameObject
{
    public int X { get; set; }
    public int Y { get; set; }
    // дверь не является препятствием
    public bool IsSolid => false;
    
    public Door(int x, int y)
    {
        X = x;
        Y = y;
    }
}
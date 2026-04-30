namespace RoguelikeGame.Core;

/// <summary>
/// KEY: ключ к выходу
/// Необходим для выигрыша у игры
/// </summary>
public class Key : IGameObject
{
    public int X { get; set; }
    public int Y { get; set; }
    // ключ не является препятствием
    public bool IsSolid => false;
    
    public Key(int x, int y)
    {
        X = x;
        Y = y;
    }
}
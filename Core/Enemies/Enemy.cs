namespace RoguelikeGame.Core.Enemies;

/// <summary>
/// ENEMY: Абстрактный базовый класс для всех врагов
/// Каждый враг должен реализовать Update() по своим правилам
/// </summary>
public abstract class Enemy : IGameObject
{
    public int X { get; set; }
    public int Y { get; set; }
    // Враги твёрдые - нельзя пройти через них
    public bool IsSolid => true;
    
    // Генератор случайных чисел
    protected Random _rand = new();
    
    public Enemy(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    // Абстрактный метод для выставления врага (приступ к Strategy pattern)
    public abstract void Update(GameManager gm);
    
    // Приватный метод для проверки столкновения с игроком
    protected void CheckPlayerCollision(GameManager gm)
    {
        if (gm.Player != null && gm.Player.X == X && gm.Player.Y == Y)
        {
            gm.GameLose();
        }
    }
}
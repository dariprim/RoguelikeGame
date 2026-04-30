namespace RoguelikeGame.Core.Enemies;

/// <summary>
/// PATROL ENEMY: Враг, который патрулирует вперёд-назад
/// Оружие: реализует территориальное поведение
/// </summary>
public class PatrolEnemy : Enemy
{
    // степпер для движения (двигаемся не каждый так
    private int _step;
    // направление движения (1 = вправо, -1 = влево)
    private int _direction = 1;
    
    public PatrolEnemy(int x, int y) : base(x, y) { }
    
    public override void Update(GameManager gm)
    {
        _step++;
        if (_step % 5 == 0)
        {
            int newX = X + _direction;
            if (newX >= 0 && newX < 20)
            {
                var solid = gm.Root.GetAt(newX, Y);
                if (solid == null && !gm.Root.IsEnemyAt(newX, Y))
                    X = newX;
                else
                    _direction *= -1;
            }
            else
                _direction *= -1;
        }
        
        CheckPlayerCollision(gm);
    }
}
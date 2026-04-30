namespace RoguelikeGame.Core.Enemies;

/// <summary>
/// RANDOM ENEMY: Враг, который двигается случайно в числюв одно из четырёх направлений
/// </summary>
public class RandomEnemy : Enemy
{
    public RandomEnemy(int x, int y) : base(x, y) { }
    
    // сразу двигаемся в случайном направлении
    public override void Update(GameManager gm)
    {
        var dirs = new[] { (0, 1), (0, -1), (1, 0), (-1, 0) };
        int idx = _rand.Next(dirs.Length);
        int dx = dirs[idx].Item1;
        int dy = dirs[idx].Item2;
        
        int newX = X + dx;
        int newY = Y + dy;
        
        if (newX >= 0 && newX < 20 && newY >= 0 && newY < 20)
        {
            var solid = gm.Root.GetAt(newX, newY);
            if (solid == null && !gm.Root.IsEnemyAt(newX, newY))
            {
                X = newX;
                Y = newY;
            }
        }
        
        CheckPlayerCollision(gm);
    }
}
namespace RoguelikeGame.Core;

/// <summary>
/// PLAYER: Главный персонаж игрока
/// Может двигаться, поднимать ключ и выходить
/// </summary>
public class Player : IGameObject
{
    public int X { get; set; }
    public int Y { get; set; }
    // Мы u043dе твёрдые - можно поставить вещи
    public bool IsSolid => false;
    
    // Сослаться на эксемпляр менеджера игры
    private GameManager _gm;
    
    public Player(int startX, int startY, GameManager gm)
    {
        X = startX;
        Y = startY;
        _gm = gm;
    }
    
    public void Move(int dx, int dy)
    {
        // Отражаем выход за границы карты
        int newX = X + dx;
        int newY = Y + dy;
        
        if (newX < 0 || newX > 19 || newY < 0 || newY > 19)
            return;
        
        // Проверяем, не враг ли на этой среди
        if (_gm.Root.IsEnemyAt(newX, newY))
        {
            _gm.GameLose();
            return;
        }
        
        // Проверяем, не препятствие ли (стена, враг)
        var solidObj = _gm.Root.GetAt(newX, newY);
        if (solidObj != null && solidObj.IsSolid)
            return;
        
        // Проверяем ключ и выход
        var objAt = _gm.Root.GetAnyAt(newX, newY);
        if (objAt is Door)
        {
            if (_gm.HasKey)
                _gm.GameWin();
            return;
        }
        
        if (objAt is Key)
        {
            _gm.PickKey();
            _gm.Root.Remove(objAt);
        }
        
        X = newX;
        Y = newY;
    }
}
namespace RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;

/// <summary>
/// COMPOSITE: Управление коллекцией всех игровых объектов
/// Хранит стены, врагов, ключ, дверь, игрока в одном списке
/// Предоставляет методы для поиска объектов по координатам
/// </summary>
public class GameObjectComposite
{
    // Список всех игровых объектов
    private List<IGameObject> _children = new();
    
    // Добавить объект на уровень
    public void Add(IGameObject obj) => _children.Add(obj);
    // Удалить объект со уровня
    public void Remove(IGameObject obj) => _children.Remove(obj);
    // Получить все объекты
    public List<IGameObject> GetChildren() => _children;
    
    // Получить твёрдый объект на позиции (препятствие, враг)
    public IGameObject? GetAt(int x, int y) 
    {
        return _children.FirstOrDefault(obj => obj.X == x && obj.Y == y && obj.IsSolid);
    }
    
    // Получить любой объект на позиции (включая ключ, дверь)
    public IGameObject? GetAnyAt(int x, int y) 
    {
        return _children.FirstOrDefault(obj => obj.X == x && obj.Y == y);
    }
    
    // Проверить есть ли враг на позиции
    public bool IsEnemyAt(int x, int y) 
    {
        return _children.Any(obj => obj.X == x && obj.Y == y && obj is Enemy);
    }
    
    public void Clear() => _children.Clear();
}
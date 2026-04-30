namespace RoguelikeGame.Core;

/// <summary>
/// GAME OBJECT INTERFACE: интерфейс для всех игровых объектов
/// Определяет базовые свойства: позиция (X, Y) и твёрдость объекта
/// </summary>
public interface IGameObject
{
    // координата X на карте
    int X { get; set; }
    // координата Y на карте
    int Y { get; set; }
    // является ли объект препятствием (нельзя пройти)
    bool IsSolid { get; }
}
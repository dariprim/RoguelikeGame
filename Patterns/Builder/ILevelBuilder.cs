using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;

namespace RoguelikeGame.Patterns.Builder;

/// <summary>
/// BUILDER INTERFACE: интерфейс для всех строителей уровней
/// Определяет этапы конструирования
/// </summary>
public interface ILevelBuilder
{
    // Очистить конструктор
    void Reset();
    // Построить стены
    void BuildWalls();
    // Построить игрока
    void BuildPlayer();
    // Построить ключ
    void BuildKey();
    // Построить врагов
    void BuildEnemies();
    // Построить выход
    void BuildDoor();
    // Получить готовый уровень
    Level GetResult();
}



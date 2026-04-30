using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;

namespace RoguelikeGame.Patterns.Builder;

/// <summary>
/// BUILDER DIRECTOR: Организирует порядок построения уровня
/// Контролирует этапы конструирования
/// </summary>
public class LevelDirector
{
    // Конструируем уровень в одределённом порядке
    public void Construct(ILevelBuilder builder)
    {
        // 1. Очистить ранее построенные уровни
        builder.Reset();
        // 2. Построить стены
        builder.BuildWalls();
        // 3. Построить выход
        builder.BuildDoor();
        // 4. Построить ключ
        builder.BuildKey();
        // 5. Построить врагов
        builder.BuildEnemies();
        // 6. Построить игрока
        builder.BuildPlayer();
    }
}
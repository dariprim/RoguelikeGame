using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;

namespace RoguelikeGame.Patterns.AbstractFactory;

/// <summary>
/// ABSTRACT FACTORY: Фабрика для создания врагов-патрульных
/// Отделяет создание врагов от их использования
/// </summary>
public class PatrolEnemyFactory : IEnemyFactory
{
    public Enemy CreateEnemy(int x, int y) => new PatrolEnemy(x, y);
}

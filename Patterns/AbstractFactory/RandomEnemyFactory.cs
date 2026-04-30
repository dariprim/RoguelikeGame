using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;
namespace RoguelikeGame.Patterns.AbstractFactory;

/// <summary>
/// ABSTRACT FACTORY: Фабрика для создания врагов со случайным движением
/// Отделяет создание врагов от их использования
/// </summary>
public class RandomEnemyFactory : IEnemyFactory
{
    public Enemy CreateEnemy(int x, int y) => new RandomEnemy(x, y);
}
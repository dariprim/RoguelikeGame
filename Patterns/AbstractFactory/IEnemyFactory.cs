using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;

namespace RoguelikeGame.Patterns.AbstractFactory;

/// <summary>
/// ABSTRACT FACTORY: интерфейс для доставки новых типов врагов
/// Любая фабрика создает свой тип врага
/// </summary>
public interface IEnemyFactory
{
    // создать врага на позиции
    Enemy CreateEnemy(int x, int y);
}


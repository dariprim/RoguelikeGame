using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;
using RoguelikeGame.Patterns.Builder;

namespace RoguelikeGame.Patterns.Interpreter;

/// <summary>
/// COMMAND: интерфейс команд для парсера
/// Каждая команда соответствует одному типу события в файле уровня
/// </summary>
public interface ICommand
{
    // выполнить команду через билдер
    void Execute(GameManager gm, Level level);
}











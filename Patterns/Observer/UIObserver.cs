namespace RoguelikeGame.Patterns.Observer;

/// <summary>
/// OBSERVER IMPLEMENTATION: Конкретный наблюдатель для ОБНОВЛЕНИЯ UI
/// Показывает сообщения на экране по событиям
/// </summary>
public class UIObserver : IGameObserver
{
    // Получить событие и обновить ОИ
    public void OnNotify(GameEvent gameEvent)
    {
        // место вывода сообщения
        Console.SetCursorPosition(0, 8);
        // цвет текста в зависимости от типа события
        Console.ForegroundColor = gameEvent.Type switch
        {
            EventType.KeyPicked => ConsoleColor.Green,  // зелёный для ключа
            _ => ConsoleColor.White  // белый для другого
        };
        Console.WriteLine(new string(' ', 60));
        Console.SetCursorPosition(0, 8);
        Console.WriteLine($"  {gameEvent.Message}");
        Console.ResetColor();
        
        Thread.Sleep(800);
    }
}
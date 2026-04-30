namespace RoguelikeGame.Patterns.Observer;

/// <summary>
/// GAME EVENT: событие для обуведомления наблюдателей
/// </summary>
public class GameEvent
{
    // тип события
    public EventType Type { get; }
    // текст сообщения
    public string Message { get; }
    
    public GameEvent(EventType type, string message)
    {
        Type = type;
        Message = message;
    }
}
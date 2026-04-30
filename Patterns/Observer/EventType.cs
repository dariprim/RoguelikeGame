namespace RoguelikeGame.Patterns.Observer;

/// <summary>
/// EVENT TYPES: типы событий в игре
/// </summary>
public enum EventType
{
    KeyPicked,  // Игрок поднял ключ
    GameWin,    // Конец игры (победа)
    GameLose    // Конец игры (поражение)
}

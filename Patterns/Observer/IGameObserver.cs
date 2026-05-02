namespace RoguelikeGame.Patterns.Observer;

/// <summary>
/// OBSERVER: Интерфейс для наблюдателей
/// Наблюдатели получают уведомления об событиях в игре
/// </summary>
public interface IGameObserver
{
    // Получить уведомление о гаме-событии
    void OnNotify(GameEvent gameEvent);
}


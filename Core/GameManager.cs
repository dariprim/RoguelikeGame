using RoguelikeGame.Patterns.Observer;

namespace RoguelikeGame.Core;

/// <summary>
/// SINGLETON: Единственный экземпляр менеджера игры
/// Управляет состоянием игры (игрок, враги, ключ, конец игры)
/// Уведомляет наблюдателей об изменениях через Observer pattern
/// </summary>
public class GameManager
{
    // Статическое поле для хранения единственного экземпляра
    private static GameManager? _instance;
    // Свойство для получения/создания экземпляра (ленивая инициализация)
    public static GameManager Instance => _instance ??= new GameManager();
    
    public Player? Player { get; set; }
    public GameObjectComposite Root { get; private set; }
    public bool HasKey { get; set; }
    public bool IsGameOver { get; private set; }
    
    // Список наблюдателей для Observer pattern
    private List<IGameObserver> _observers = new();
    
    // Приватный конструктор (Singleton)
    private GameManager()
    {
        Root = new GameObjectComposite();
        HasKey = false;
        IsGameOver = false;
    }
    
    // Добавить наблюдателя (подписка на события)
    public void AddObserver(IGameObserver observer) => _observers.Add(observer);
    
    // Уведомить всех наблюдателей об событии
    public void Notify(GameEvent gameEvent)
    {
        foreach (var observer in _observers)
            observer.OnNotify(gameEvent);
    }
    
    public void GameLose()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n");

        Console.WriteLine("               GAME OVER                ");
        Console.WriteLine("      You were killed by an enemy!      ");

        Console.ResetColor();
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey(true);
        Environment.Exit(0);
    }
    
    public void GameWin()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n");

        Console.WriteLine("                VICTORY!                ");
        Console.WriteLine("      You escaped with the key!         ");

        Console.ResetColor();
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey(true);
        Environment.Exit(0);
    }
    
    public void PickKey()
    {
        HasKey = true;
        Notify(new GameEvent(EventType.KeyPicked, "You found the key!"));
    }
}
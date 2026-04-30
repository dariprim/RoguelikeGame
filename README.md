# 🎮 Roguelike Dungeon Game

Консольная roguelike-игра на C# .NET 9.0 "Волшебный ключик"

## 📖 Описание Игры

**Roguelike Dungeon** — это пошаговая консольная игра в подземелье, где игрок должен:

1. **Исследовать подземелье** — перемещаться по лабиринту (20×20 клеток)
2. **Найти ключ** — необходимо подобрать ключ, лежащий где-то в подземелье
3. **Добраться до двери** — найти выход из подземелья
4. **Избежать врагов** — уничтожить противника означает смерть игрока

### Управление
- **W / ↑** — движение вверх
- **S / ↓** — движение вниз
- **A / ←** — движение влево
- **D / →** — движение вправо

### Условия Победы/Поражения
- **Победа**: Подобрать ключ и дойти до двери
- **Поражение**: Столкнуться с врагом

## Архитектура и Паттерны Проектирования

Проект использует **семь классических паттернов проектирования**. Каждый решает конкретную задачу в архитектуре игры.

---

### 1. **Singleton** 
**Где используется**: [`Core/GameManager.cs`](Core/GameManager.cs)

**Зачем нужен**: Гарантирует единственный экземпляр менеджера игры на протяжении всей программы.

**Как работает**:
```csharp
public class GameManager
{
    private static GameManager? _instance;
    public static GameManager Instance => _instance ??= new GameManager();
}
```

**Объяснение**:
- `GameManager` управляет состоянием игры (позиция игрока, наличие ключа, враги)
- Нужно, чтобы от всех мест программы был доступ к **одному и тому же** состоянию игры
- Использование Singleton предотвращает случайное создание нескольких экземпляров
- Инициализация **ленивая** (создаётся только при первом обращении к `Instance`)

**Преимущества**:
- Глобальная точка доступа к состоянию игры
- Гарантия единственного экземпляра
- Безопасен в многопоточной среде (благодаря null-coalescing оператору)

---

### 2. **Composite** 🧩
**Где используется**: [`Core/GameObjectComposite.cs`](Core/GameObjectComposite.cs)

**Зачем нужен**: Управление коллекциями игровых объектов как единым целым.

**Как работает**:
```csharp
public class GameObjectComposite
{
    private List<IGameObject> _children = new();
    
    public void Add(IGameObject obj) => _children.Add(obj);
    public IGameObject? GetAt(int x, int y) => _children.FirstOrDefault(...);
    public bool IsEnemyAt(int x, int y) => _children.Any(obj => ...);
}
```

**Объяснение**:
- Все игровые объекты (стены, враги, ключ, дверь) реализуют интерфейс `IGameObject`
- `GameObjectComposite` хранит их в одном списке и предоставляет методы для работы
- Позволяет работать с группой объектов как с единым целым
- Просто добавить новый тип объекта (например, ловушку или сокровище)

**Использование**:
- Проверка столкновения: `GetAt(x, y)` — есть ли препятствие?
- Поиск врага: `IsEnemyAt(x, y)` — враг на этой клетке?
- Очистка уровня: `Clear()` — удалить все объекты

**Преимущества**:
- Универсальный способ работать с любыми игровыми объектами
- Легко расширяется новыми типами объектов
- Централизованное управление всеми объектами

---

### 3. **Abstract Factory** 🏭
**Где используется**: [`Patterns/AbstractFactory/`](Patterns/AbstractFactory/)

**Зачем нужен**: Создание различных типов врагов без привязки к конкретным классам.

**Как работает**:
```csharp
public interface IEnemyFactory
{
    Enemy CreateEnemy(int x, int y);
}

public class RandomEnemyFactory : IEnemyFactory
{
    public Enemy CreateEnemy(int x, int y) => new RandomEnemy(x, y);
}

public class PatrolEnemyFactory : IEnemyFactory
{
    public Enemy CreateEnemy(int x, int y) => new PatrolEnemy(x, y);
}
```

**Объяснение**:
- В игре два типа врагов: `RandomEnemy` (движется случайно) и `PatrolEnemy` (патрулирует)
- Вместо `new RandomEnemy()` или `new PatrolEnemy()` используем фабрики
- `LevelParser` не знает точный тип врага — просто вызывает `factory.CreateEnemy(x, y)`
- При добавлении нового типа врага создаём новую фабрику, остальной код не меняется

**Использование в LevelParser**:
```csharp
if (parts[1] == "RANDOM")
    factory = new RandomEnemyFactory();
else if (parts[1] == "PATROL")
    factory = new PatrolEnemyFactory();
```

**Преимущества**:
- Слабая связанность кода (LevelParser не зависит от конкретных типов врагов)
- Легко добавить новый тип врага
- Логика создания врагов в одном месте

---

### 4. **Builder** 👷
**Где используется**: [`Patterns/Builder/`](Patterns/Builder/)

**Зачем нужен**: Пошаговое конструирование уровня в определённом порядке.

**Компоненты**:
- **ILevelBuilder** — интерфейс с методами для построения частей уровня
- **SimpleLevelBuilder** — реализует пошаговое построение
- **LevelDirector** — управляет порядком построения
- **Level** — итоговый объект (результат построения)

**Как работает**:
```csharp
public class LevelDirector
{
    public void Construct(ILevelBuilder builder)
    {
        builder.Reset();
        builder.BuildWalls();      // Сначала стены
        builder.BuildDoor();       // Потом дверь
        builder.BuildKey();        // Потом ключ
        builder.BuildEnemies();    // Потом враги
        builder.BuildPlayer();     // И наконец игрок
    }
}
```

**Объяснение**:
- Уровень состоит из нескольких компонентов (стены, враги, ключ и т.д.)
- Они должны быть добавлены в **определённом порядке**
- Builder инкапсулирует логику построения каждого компонента
- Director определяет порядок построения
- Разделение ответственности: Builder знает ЧТО строить, Director знает КОГДА

**Преимущества**:
- Чёткий порядок построения уровня
- Легко изменить алгоритм построения (добавить/удалить этап)
- Разделение логики построения разных элементов
- Можно создавать разные типы уровней с разными Builder'ами

---

### 5. **Interpreter** 📝
**Где используется**: [`Patterns/Interpreter/LevelParser.cs`](Patterns/Interpreter/LevelParser.cs)

**Зачем нужен**: Интерпретация текстового формата уровня в игровые объекты.

**Формат уровня** (`Levels/level1.txt`):
```
PLAYER 2,2
DOOR 1,1
KEY 17,17
ENEMY RANDOM,10,4
ENEMY PATROL,3,12
WALL 10,10
WALL 10,11
```

**Как работает**:
```csharp
public class LevelParser
{
    public Level Parse(string filename)
    {
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            var parts = line.Trim().Split(' ');
            
            ICommand? command = parts[0] switch
            {
                "PLAYER" => new PlayerCommand(...),
                "DOOR" => new DoorCommand(...),
                "ENEMY" => new EnemyCommand(...),
                "WALL" => new WallCommand(...),
                "KEY" => new KeyCommand(...),
                _ => null
            };
            
            command?.Execute(builder);
        }
    }
}
```

**Объяснение**:
- Интерпретер переводит текстовый синтаксис в команды (`ICommand`)
- Каждая строка файла = одна команда, которая создаёт объект
- Команды выполняются через Builder
- Добавление нового типа объекта = добавление новой команды

**Компоненты**:
- **ICommand** — интерфейс для всех команд
- **PlayerCommand, DoorCommand, EnemyCommand** и т.д. — конкретные команды
- **LevelParser** — интерпретер синтаксиса

**Преимущества**:
- Уровни хранятся в простом текстовом формате
- Не нужна перекомпиляция при изменении уровня
- Легко создавать новые уровни
- Возможность добавить новый синтаксис (например, для ловушек)

---

### 6. **Observer** 👁️
**Где используется**: [`Patterns/Observer/`](Patterns/Observer/)

**Зачем нужен**: Уведомление UI об изменении состояния игры.

**Компоненты**:
- **IGameObserver** — интерфейс наблюдателя
- **UIObserver** — реализация (обновляет пользовательский интерфейс)
- **GameEvent** — событие игры
- **EventType** — тип события (взял ключ, встретил врага и т.д.)

**Как работает**:
```csharp
public class GameManager
{
    private List<IGameObserver> _observers = new();
    
    public void AddObserver(IGameObserver observer) 
        => _observers.Add(observer);
    
    public void Notify(GameEvent gameEvent)
    {
        foreach (var observer in _observers)
            observer.OnNotify(gameEvent);  // Уведомляем всех наблюдателей
    }
}

// В Program.cs:
var gm = GameManager.Instance;
gm.AddObserver(new UIObserver());  // Добавляем наблюдателя

// Где-то в коде игры:
gm.Notify(new GameEvent(EventType.KeyTaken, "Вы подобрали ключ!"));
```

**Объяснение**:
- GameManager уведомляет об изменениях (например, "игрок взял ключ")
- UIObserver слушает события и обновляет экран
- GameManager не знает деталей UI, просто отправляет уведомления
- Можно добавить нескольких наблюдателей (логирование, звук и т.д.)

**Преимущества**:
- Слабая связанность между GameManager и UI
- Легко добавить новых наблюдателей (логирование, сохранение и т.д.)
- Автоматическое обновление UI при изменении состояния
- Один GameManager может уведомлять множество объектов

---

### 7. **Proxy** 🔒
**Где используется**: [`Patterns/Proxy/LevelLoaderProxy.cs`](Patterns/Proxy/LevelLoaderProxy.cs)

**Зачем нужен**: Отложенная загрузка и кэширование уровня.

**Как работает**:
```csharp
public class LevelLoaderProxy
{
    private Level? _cachedLevel;
    private bool _isLoaded = false;
    
    public Level Load()
    {
        if (!_isLoaded)
        {
            Console.WriteLine("[Proxy] Loading level...");
            var parser = new LevelParser();
            _cachedLevel = parser.Parse(_levelFile);
            _isLoaded = true;
        }
        return _cachedLevel!;
    }
}

// В Program.cs:
var levelProxy = new LevelLoaderProxy("Levels/level1.txt");
var level = levelProxy.Load();  // Загрузка происходит ленивым образом
```

**Объяснение**:
- Proxy — это **посредник** между клиентом и реальным объектом (LevelParser)
- Уровень загружается только когда его впервые запросили (`lazy loading`)
- Уровень кэшируется, повторные обращения не вызывают повторную загрузку
- Proxy выполняет дополнительные действия (логирование загрузки)

**Преимущества**:
- Отложенная инициализация (экономия памяти и времени)
- Кэширование уровня
- Контроль доступа к ресурсам
- Логирование и мониторинг загрузок
- Подготовка к сетевой загрузке (например, с сервера)

### Команды для запуска

```bash
# Сборка проекта
dotnet build

# Запуск игры
dotnet run

# Или просто
dotnet run --project RoguelikeGame.csproj
```

using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;
using RoguelikeGame.Patterns.Proxy;
using RoguelikeGame.Patterns.Observer;
using System.Text;

namespace RoguelikeGame;

/// <summary>
/// POINT OF ENTRY: главный класс приложения
/// Устанавливает параметры консоли, инициализирует игру и запускает основной цикл
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        // настройка кодировки консоли (для русского текста)
        Console.OutputEncoding = Encoding.UTF8;
        Console.Title = "Roguelike Dungeon";
        Console.CursorVisible = false;
        
        // установка размера консольного окна
        try
        {
            Console.SetWindowSize(60, 35);
            Console.SetBufferSize(60, 35);
        }
        catch { }
        
        // SINGLETON: получаем единственный экземпляр менеджера игры
        var gm = GameManager.Instance;
        // OBSERVER: добавляем наблюдателя для обновления UI
        gm.AddObserver(new UIObserver());
        
        // PROXY: загружаем уровень с отложенной инициализацией
        var levelProxy = new LevelLoaderProxy("Levels/level1.txt");
        var level = levelProxy.Load();
        // добавляем все объекты уровня в менеджер игры
        level.Setup(gm);
        
        // MAIN GAME LOOP: основной цикл игры
        while (!gm.IsGameOver)
        {
            Console.Clear();
            
            // рисуем заголовок, статус, игровое поле и управление
            DrawHeader();
            DrawStatus(gm);
            DrawGameField(gm);
            DrawControls();
            
            // ждём нажатия клавиши и обрабатываем движение
            var key = Console.ReadKey(true).Key;
            int dx = 0, dy = 0;
            
            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    dy = -1;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    dy = 1;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    dx = -1;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    dx = 1;
                    break;
                case ConsoleKey.Escape:
                    return;
            }
            
            if (dx != 0 || dy != 0)
            {
                gm.Player?.Move(dx, dy);
            }
            
            foreach (var obj in gm.Root.GetChildren())
            {
                if (obj is Enemy enemy)
                    enemy.Update(gm);
            }
        }
    }
    
    static void DrawHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine("                    ROGUELIKE DUNGEON                   ");

        Console.ResetColor();
        Console.WriteLine();
    }
    
    static void DrawStatus(GameManager gm)
    {
        Console.ForegroundColor = gm.HasKey ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine($"  KEY: {(gm.HasKey ? " FOUND" : " MISSING")}");
        Console.ResetColor();
        
        if (gm.Player != null)
        {
            Console.WriteLine($"  POS: ({gm.Player.X}, {gm.Player.Y})");
        }
        Console.WriteLine();
    }
    
    static void DrawGameField(GameManager gm)
    {
        int startY = Console.CursorTop;
        
        for (int y = 0; y < 20; y++)
        {
            Console.Write("  "); 
            for (int x = 0; x < 20; x++)
            {
                var obj = gm.Root.GetAnyAt(x, y);
                if (obj is Player && gm.Player != null && gm.Player.X == x && gm.Player.Y == y)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write('P');
                }
                else if (obj is Wall)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write('#');
                }
                else if (obj is Enemy)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('E');
                }
                else if (obj is Key)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write('K');
                }
                else if (obj is Door)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write('D');
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write('.');
                }
                Console.ResetColor();
                Console.Write(' ');
            }
            Console.WriteLine();
        }
    }
    
    static void DrawControls()
    {
        Console.WriteLine();
        Console.WriteLine("    CONTROLS:  W/A/S/D  or  ↑/←/↓/→      ESC = exit    ");

    }
}
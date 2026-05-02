using RoguelikeGame.Core;
using RoguelikeGame.Core.Enemies;
using RoguelikeGame.Patterns.Builder;
using RoguelikeGame.Patterns.Interpreter;

namespace RoguelikeGame.Patterns.Proxy;

/// <summary>
/// PROXY: Посредник для загрузки уровня
/// Кэширование загруженных уровней
/// </summary>
public class LevelLoaderProxy
{
    // Кэш загруженного уровня
    private Level? _cachedLevel;
    // Путь к файлу уровня
    private string _levelFile;
    // Флаг, противых репетитивных загружек
    private bool _isLoaded = false;
    
    public LevelLoaderProxy(string levelFile)
    {
        _levelFile = levelFile;  // Охраняем путь, но не гружим еще
    }
    
    // Ленивая загрузка: гружим только когда действительно требуется
    public Level Load()
    {
        if (!_isLoaded)
        {
            Console.WriteLine("[Proxy] Loading level...");
            
            if (File.Exists(_levelFile))
            {
                Console.WriteLine($"[Proxy] Found file: {_levelFile}, parsing...");
                var parser = new LevelParser();
                _cachedLevel = parser.Parse(_levelFile);
            }
            
            if (_cachedLevel == null || (_cachedLevel.Walls.Count == 0 && _cachedLevel.Enemies.Count == 0))
            {
                Console.WriteLine("[Proxy] No valid level file, using builder...");
                var builder = new SimpleLevelBuilder();
                var director = new LevelDirector();
                director.Construct(builder);
                _cachedLevel = builder.GetResult();
            }
            
            _isLoaded = true;
        }
        else
        {
            Console.WriteLine("[Proxy] Returning cached level...");
        }
        
        return _cachedLevel!;
    }
}
using UnityEngine;

public class PlayerProvider
{
    private MainHeroFactory _mainHeroFactory;
    private KeyboardInput _keyboardInput;

    public SimpleCharacter MainHero { get; private set; }

    public PlayerProvider(MainHeroFactory mainHeroFactory, KeyboardInput keyboardInput)
    {
        _mainHeroFactory = mainHeroFactory;
        _keyboardInput = keyboardInput;
    }

    public void Create(MainHeroConfig config, Vector3 spawnPoint)
    {
        MainHero = _mainHeroFactory.Create(config, spawnPoint, _keyboardInput);
    }

    public void DestroyHero()
    {
        MainHero?.Destroy();
        MainHero = null;
    }
}

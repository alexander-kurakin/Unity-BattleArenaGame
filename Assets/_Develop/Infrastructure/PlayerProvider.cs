public class PlayerProvider
{
    public SimpleCharacter MainHero { get; private set; }

    public PlayerProvider(MainHeroFactory mainHeroFactory)
    {
        mainHeroFactory.Created += OnHeroCreated;
    }

    public void DestroyHero()
    {
        MainHero?.Destroy();
        MainHero = null;
    }

    private void OnHeroCreated(SimpleCharacter hero)
    {
        MainHero = hero;
    }
}

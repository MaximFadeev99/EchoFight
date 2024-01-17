public class HumanoidEnemyBot : Humanoid, IEnemy
{
    private BotInputHandler _botInputHandler;
    private BotRotationHandler _botRotationHandler;

    public Player Player { get; private set; }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (Player != null )
            Player.HealthHandler.HealthIsZero -= PauseRotation;
    }

    public void SetPlayer(Player player)
    {
        Player = player;
        Player.HealthHandler.HealthIsZero += PauseRotation;
        _botInputHandler.SetPlayer(Player);
        _botRotationHandler.SetPlayer(Player.Transform);
    }

    protected override IInputHandler SetInputHandler()
    {
        _botInputHandler = new BotInputHandler(this);
        return _botInputHandler;
    }

    protected override IRotationHandler SetRotationHandler()
    {
        _botRotationHandler = new(Transform);
        return _botRotationHandler;
    }
}
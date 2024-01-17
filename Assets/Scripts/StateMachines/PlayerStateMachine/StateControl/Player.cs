public class Player : Humanoid
{
    protected override IInputHandler SetInputHandler()
    {
        return new PlayerInputHandler();
    }

    protected override IRotationHandler SetRotationHandler()
    {
        return new PlayerRotationHandler(Transform);
    }
}
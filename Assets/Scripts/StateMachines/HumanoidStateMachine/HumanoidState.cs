public abstract class HumanoidState : State
{
    protected Humanoid Humanoid {get; private set;} 

    public HumanoidState (Humanoid humanoid) => Humanoid = humanoid;
}
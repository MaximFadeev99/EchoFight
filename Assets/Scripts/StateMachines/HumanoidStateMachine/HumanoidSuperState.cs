public abstract class HumanoidSuperState : SuperState
{
    protected Humanoid Humanoid { get; private set; }
    
    public HumanoidSuperState(StateMachine stateMachine, Humanoid humanoid) : base(stateMachine)
    {
        Humanoid = humanoid;
    }
}
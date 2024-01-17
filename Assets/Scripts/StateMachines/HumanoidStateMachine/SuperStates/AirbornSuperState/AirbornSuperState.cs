public class AirbornSuperState : HumanoidSuperState
{
    private readonly FallingState _fallingState;
    
    public AirbornSuperState(StateMachine stateMachine, Humanoid humanoid) : base(stateMachine, humanoid)
    {
        _fallingState = new(Humanoid);
    }

    protected override State TryChangeState()
    {
        return _fallingState;
    }
}
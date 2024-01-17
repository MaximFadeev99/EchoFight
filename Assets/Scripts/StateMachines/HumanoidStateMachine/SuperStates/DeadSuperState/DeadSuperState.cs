public class DeadSuperState : HumanoidSuperState
{
    private readonly DeadState _deadState;
    
    public DeadSuperState(StateMachine stateMachine, Humanoid humanoid) : base(stateMachine, humanoid)
    {
        _deadState = new(Humanoid);
    }

    protected override State TryChangeState()
    {
        return _deadState;
    }
}
public abstract class SuperState
{
    public StateMachine StateMachine { get; protected set; }
    public State CurrentState { get; protected set; } 
    public State PreviousState { get; protected set; }

    public SuperState(StateMachine stateMachine) => StateMachine = stateMachine;

    public State SetState() 
    {
        State newState;

        newState = TryChangeState();

        if (newState != StateMachine.CurrentState)
        {
            PreviousState = StateMachine.CurrentState;
            StateMachine.CurrentState.Exit();
            newState.Enter();
            CurrentState = newState;
        }

        return newState;
    }

    protected abstract State TryChangeState();
}
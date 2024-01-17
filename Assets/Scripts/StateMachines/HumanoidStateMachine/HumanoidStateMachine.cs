public class HumanoidStateMachine : StateMachine
{
    private Humanoid _humanoid;

    private GroundedSuperState _groundedSuperState;
    private AirbornSuperState _airbornSuperState;
    private DeadSuperState _deadSuperState;

    private void Awake()
    {
        _humanoid = GetComponent<Humanoid>();
        _groundedSuperState = new(this, _humanoid);
        _airbornSuperState = new(this, _humanoid);
        _deadSuperState = new(this, _humanoid);
        CurrentSuperState = _groundedSuperState;
        CurrentState = _groundedSuperState.CurrentState;
    }

    protected override SuperState SetSuperState()
    {
        if (_humanoid.IsAlive == false) 
        {
            return _deadSuperState;
        }
        else if (_humanoid.IsGrounded)
        {
            return _groundedSuperState;
        }
        else if (_humanoid.IsGrounded == false)
        {
            return _airbornSuperState;
        }

        return _groundedSuperState;
    }
}
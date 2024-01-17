using UnityEngine;

public class GroundedSuperState : HumanoidSuperState
{
    private readonly IdleState _idleState;
    private readonly MovementState _movementState;
    private readonly DodgingState _dodgingState;
    private readonly AttackState _attackState;
    private readonly FightTransitionState _fightTransitionState;
    private readonly HurtState _hurtState;
    
    public GroundedSuperState(StateMachine stateMachine, Humanoid humanoid) : base(stateMachine, humanoid)
    {
        _idleState = new(Humanoid);
        _movementState = new(Humanoid);
        _dodgingState = new(Humanoid);
        _attackState = new(Humanoid);
        _fightTransitionState = new(Humanoid);
        _hurtState = new(Humanoid);
        CurrentState = _idleState;
    }

    protected override State TryChangeState()
    {
        if ((Humanoid.IsHurt && _fightTransitionState.IsReadyToFight) || _hurtState.IsHurt)
        {
            return _hurtState;
        }
        else if (Humanoid.IsDodging)
        {
            return _dodgingState;
        }
        else if (Humanoid.MovementDirection != Vector3.zero)
        {
            _fightTransitionState.ResetFightReadiness();
            return _movementState;
        }
        else if (Humanoid.IsAttacking || _attackState.IsAttacking)
        {
            if (_fightTransitionState.IsReadyToFight == false)
                return _fightTransitionState;
            else
                return _attackState;
        }
        else if (Humanoid.IsHurt || _fightTransitionState.IsReadyToFight)
        {
            _fightTransitionState.TransitToFight();
            return _fightTransitionState;
        }
        else if (Humanoid.MovementDirection == Vector3.zero && 
            _fightTransitionState.IsReadyToFight == false)
        {
            return _idleState;
        }
        else
        {
            return _idleState;
        }
    }
}
using UnityEngine;

public class HurtState : HumanoidState
{
    private const float TransitionStep = 0.1f;
    
    private readonly Animator _animator;
    private readonly int _hurtBlendingHash = Animator.StringToHash("HurtBlending");
    private readonly int _hurtState = Animator.StringToHash("Hurt");
    private readonly int _fightIdleState = Animator.StringToHash("Fight Idle");

    private float _currentTransition = 0f;   
    
    public HurtState(Humanoid humanoid) : base(humanoid)
    {
        _animator = Humanoid.Animator;
    }

    public bool IsHurt { get; private set; }

    public override void Enter()
    {
        IsHurt = true;
        Humanoid.EndDodge();
        _animator.Play(_hurtState);
    }

    public override void Exit()
    {
        _currentTransition = 0f;
        _animator.SetFloat(_hurtBlendingHash, _currentTransition);
        _animator.Play(_fightIdleState);
    }

    public override void Update()
    {
        if (Humanoid.IsHurt)
        {
            if (_currentTransition < 1f) 
            {
                _currentTransition += TransitionStep;
                _animator.SetFloat(_hurtBlendingHash, _currentTransition);
            }

            return;
        }
        
        if(IsHurt)
        {
            if (_currentTransition > 0f)
            {
                _currentTransition -= TransitionStep;
                _animator.SetFloat(_hurtBlendingHash, _currentTransition);
            }
            else 
            {
                IsHurt = false;
            }
        }       
    }
}
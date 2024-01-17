using UnityEngine;

public class FightTransitionState : HumanoidState
{
    private const float TransitionStep = 0.2f;
    
    private readonly Animator _animator;
    private readonly int FightIdleHash = Animator.StringToHash("Fight Idle");
    private readonly int FightTransitionHash = Animator.StringToHash("FightTransition");

    private float _currentTransitionValue = 0f;

    public bool IsReadyToFight { get; private set; } = false;

    public FightTransitionState(Humanoid humanoid) : base(humanoid)
    {
        _animator = Humanoid.Animator;
    }

    public override void Enter()
    {
        _animator.Play(FightIdleHash);
    }

    public override void Exit()
    {
        _currentTransitionValue = 0f;
    }

    public override void Update()
    {
        if (_currentTransitionValue < 1f)
        {
            _currentTransitionValue += TransitionStep;
            _animator.SetFloat(FightTransitionHash, _currentTransitionValue);
        }
        else
        {
            _currentTransitionValue = 1f;
            _animator.SetFloat(FightTransitionHash, _currentTransitionValue);
            IsReadyToFight = true;
        }
    }

    public void ResetFightReadiness() 
    {
        IsReadyToFight = false;
        _currentTransitionValue = 0f;
        _animator.SetFloat(FightTransitionHash, _currentTransitionValue);
    }

    public void TransitToFight() 
    {
        _currentTransitionValue = 1f;
        _animator.SetFloat(FightTransitionHash, _currentTransitionValue);
        IsReadyToFight = true;
    }
}
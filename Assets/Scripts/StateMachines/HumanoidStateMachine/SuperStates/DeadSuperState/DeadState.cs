using UnityEngine;

public class DeadState : HumanoidState
{
    private readonly Animator _animator;
    private readonly Rigidbody _rigidbody;
    private readonly CapsuleCollider _capsuleCollider;
    private readonly Timer _fallTimer = new();
    private readonly LayerMask _excludedLayerMask;
    private readonly int _deadStateHash = Animator.StringToHash("Dead");
    private readonly float _fallTime = 0.8f;
    
    public DeadState(Humanoid humanoid) : base(humanoid)
    {
        _animator = Humanoid.Animator;
        _capsuleCollider = Humanoid.CapsuleCollider;
        _rigidbody = Humanoid.Rigidbody;
        _excludedLayerMask = Humanoid.HumanoidData.DealDamageMask;
    }

    public override void Enter()
    {
        _animator.Play(_deadStateHash);
        _fallTimer.TimeIsUp += ChangeColliderDirection;
        _fallTimer.Start(_fallTime);
        Humanoid.PauseRotation();
        _capsuleCollider.excludeLayers = _excludedLayerMask;
    }

    public override void Exit(){}

    public override void Update()
    {
        if (_fallTimer.IsActive)
            _fallTimer.Tick();

        _rigidbody.velocity = Vector3.zero;
    }

    private void ChangeColliderDirection() 
    {
        _capsuleCollider.direction = 2;
        _fallTimer.TimeIsUp -= ChangeColliderDirection;
    }
}
using UnityEngine;

public class IdleState : HumanoidState
{   
    private readonly Animator _animator;
    private readonly Rigidbody _rigidbody;
    private readonly Transform _transform;
    private readonly int _zVelocty = Animator.StringToHash("ZVelocity");
    private readonly int _xVelocity = Animator.StringToHash("XVelocity");
    private readonly int MovementHash = Animator.StringToHash("Movement");
    
    public IdleState(Humanoid humanoid) : base(humanoid)
    {
        _animator = humanoid.Animator;
        _rigidbody = humanoid.Rigidbody;
        _transform = humanoid.Transform;
    }

    public override void Enter()
    {
        _animator.Play(MovementHash);
        _animator.SetFloat(_zVelocty, 0);
        _animator.SetFloat(_xVelocity, 0);
        _rigidbody.velocity = Vector3.zero;
        _transform.rotation = Quaternion.LookRotation(_transform.forward);
    }

    public override void Exit() {}

    public override void Update() {}
}
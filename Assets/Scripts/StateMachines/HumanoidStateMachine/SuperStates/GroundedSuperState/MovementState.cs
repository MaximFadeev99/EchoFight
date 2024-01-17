using UnityEngine;

public class MovementState : HumanoidState
{
    private readonly Animator _animator;
    private readonly Rigidbody _rigidbody;
    private readonly Transform _transform;
    private readonly int _zVelocty = Animator.StringToHash("ZVelocity");
    private readonly int _xVelocity = Animator.StringToHash("XVelocity");
    private readonly int _movementHash = Animator.StringToHash("Movement");
    private readonly float _forwardRunModifier = 2f;
    
    private Vector3 _movementDirection = Vector3.zero;
    private float _movementSpeed;

    public MovementState(Humanoid humanoid) : base(humanoid)
    {
        _animator = humanoid.Animator;
        _rigidbody = humanoid.Rigidbody;
        _transform = humanoid.transform;
        _movementSpeed = Humanoid.HumanoidData.MovementSpeed;
    }

    public override void Enter()
    {
        _animator.Play(_movementHash);
        UpdateAnimation();
    }

    public override void Exit()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    public override void Update()
    {
        UpdateAnimation();
        MovePlayer();
    }

    private void UpdateAnimation() 
    {
        _animator.SetFloat(_zVelocty, Humanoid.MovementDirection.z);
        _animator.SetFloat(_xVelocity, Humanoid.MovementDirection.x);
    }

    private void MovePlayer() 
    {
        if (Humanoid.MovementDirection.z != 0)
        {
            if (Humanoid.MovementDirection.z == 1f && Humanoid.MovementDirection.x == 0f)
                _movementSpeed *= _forwardRunModifier;

            _movementDirection = _transform.forward * Humanoid.MovementDirection.z;
        }

        if (Humanoid.MovementDirection.x != 0)
        {
            if (Humanoid.MovementDirection.z != 0)
            {
                _movementDirection = Vector3.Slerp
                    (_movementDirection, _transform.right * Humanoid.MovementDirection.x, 0.5f);
            }
            else 
            {
                _movementDirection = _transform.right * Humanoid.MovementDirection.x;         
            }
        }

        _rigidbody.velocity = Time.fixedDeltaTime * _movementSpeed * _movementDirection;
        _movementSpeed = Humanoid.HumanoidData.MovementSpeed;
        _movementDirection = Vector3.zero;
    }
}
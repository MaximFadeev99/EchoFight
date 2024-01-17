using UnityEngine;

public class DodgingState : HumanoidState
{
    private readonly Animator _animator;
    private readonly Rigidbody _rigidbody;
    private readonly Transform _transform;
    private readonly CapsuleCollider _capsuleCollider;
    private readonly Vector3 _initialColliderCenter;
    private readonly int DodgingHash = Animator.StringToHash("Dodging");
    private readonly float _dodgeSpeed;

    private Vector3 _newLookDirection = Vector3.zero;
    private Quaternion _newLookRotation = Quaternion.identity;
    private bool _isAnimationPlaying = false;
    
    public DodgingState(Humanoid humanoid) : base(humanoid)
    {
        _animator = Humanoid.Animator;
        _rigidbody = Humanoid.Rigidbody;
        _transform = Humanoid.Transform;
        _capsuleCollider = Humanoid.CapsuleCollider;
        _initialColliderCenter = _capsuleCollider.center;
        _dodgeSpeed = Humanoid.HumanoidData.DodgeSpeed;
    }

    public override void Enter()
    {
        Humanoid.PauseRotation();
        _newLookRotation = CalculateNewRotaion();                        
        _capsuleCollider.center = new Vector3(_initialColliderCenter.x, _initialColliderCenter.y, 1f);
    }

    public override void Exit()
    {
        _isAnimationPlaying = false;
        _rigidbody.velocity = Vector3.zero;
        _capsuleCollider.center = _initialColliderCenter;       
        Humanoid.UnpauseRotation();
    }

    public override void Update()
    {
        if (_transform.rotation != _newLookRotation) 
            _transform.rotation = Quaternion.Slerp(_transform.rotation, _newLookRotation, 0.7f);

        if (_isAnimationPlaying == false) 
        {
            _animator.Play(DodgingHash);
            _isAnimationPlaying = true;
        }
        
        _rigidbody.velocity = _transform.forward * _dodgeSpeed;       
    }

    private Quaternion CalculateNewRotaion() 
    {
        if (Humanoid.MovementDirection != Vector3.zero)
        {
            _newLookDirection = Humanoid.MovementDirection;

            if (_newLookDirection.x != 0f)
                _newLookDirection.x = _newLookDirection.x < 0f ? -1f : 1f;

            if (_newLookDirection.z != 0f)
                _newLookDirection.z = _newLookDirection.z < 0f ? -1f : 1f;

            if (_newLookDirection.x != 0f && _newLookDirection.z != 0f)
            {
                _newLookDirection = Vector3.Slerp
                    (_transform.right * _newLookDirection.x, _transform.forward * _newLookDirection.z, 0.5f);
            }
            else
            {
                _newLookDirection = 
                    _transform.right * _newLookDirection.x + _transform.forward * _newLookDirection.z;
            }

            return Quaternion.LookRotation(_newLookDirection);
        }
        else
        {
            return _transform.rotation;
        }
    }
}
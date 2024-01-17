using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider))]
public abstract class Humanoid : MonoBehaviour, IDamagable
{
    protected bool IsRotationPaused = false;

    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private HumanoidData _humanoidData;
    [SerializeField] private HealthHandler _healthHandler;

    private IInputHandler _inputHandler;   
    private IRotationHandler _rotationHandler;
    private bool _canAttack = true;

    public Transform Transform { get; private set; }
    public CapsuleCollider CapsuleCollider { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public DamageZone DamageZone { get; private set; }
    public Renderer Renderer { get; private set; }
    public HumanoidData HumanoidData => _humanoidData;
    public HealthHandler HealthHandler => _healthHandler;

    public Vector3 MovementDirection { get; private set; }

    public bool IsGrounded { get; private set; }
    public bool IsDodging { get; private set; } 
    public bool IsAttacking { get; private set; }
    public bool IsHurt { get; private set; }
    public bool IsAlive { get; private set; } = true;

    public void EndDodge() =>
        IsDodging = false;

    public void EndAttack() =>
        IsAttacking = false;

    public void ResetAttack() =>
        _canAttack = true;

    public void PauseRotation() =>
        IsRotationPaused = true;

    public void UnpauseRotation()
    {
        IsRotationPaused = false;
        _rotationHandler.ResetTurn();
    }

    public void TakeDamage()
    {
        if (IsHurt == false)
        {
            IsHurt = true;
            _healthHandler.ReduceHealth();
        }
    }

    public void EndHurt() =>
        IsHurt = false;

    protected  virtual void OnEnable()
    {
        _healthHandler.HealthIsZero += Die;
    }

    protected virtual void OnDisable()
    {
        _healthHandler.HealthIsZero -= Die;
    }

    protected abstract IInputHandler SetInputHandler();
    protected abstract IRotationHandler SetRotationHandler();

    private void Awake()
    {
        Transform = transform;
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
        DamageZone = GetComponentInChildren<DamageZone>();
        Renderer = GetComponentInChildren<Renderer>();
        _inputHandler = SetInputHandler();
        _rotationHandler = SetRotationHandler();
        _healthHandler.Intialize(HumanoidData.StartHealth, Renderer, _humanoidData.WireframeMaterialName);
    }

    private void Update()
    {
        IsGrounded = _raycaster.IsGrounded();

        _inputHandler.Update();

        if (IsDodging == false && IsHurt == false)
            IsDodging = _inputHandler.GetDodgeInput();

        MovementDirection = _inputHandler.GetMovementDirection();

        if (IsAttacking == false && _canAttack && IsHurt == false) 
        {
            IsAttacking = _inputHandler.GetAttackInput();
            
            if (IsAttacking)
                _canAttack = false;
        }

        if (IsRotationPaused == false)
            _rotationHandler.UpdateRotation();
    }

    private void Die() =>
        IsAlive = false; 
}
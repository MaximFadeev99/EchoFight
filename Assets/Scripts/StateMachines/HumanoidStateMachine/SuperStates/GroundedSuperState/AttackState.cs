using UnityEngine;

public class AttackState : HumanoidState
{
    private const int MaxAttackCount = 4;

    private readonly Animator _animator;
    private readonly DamageZone _damageZone;
    private readonly LayerMask _dealDamageLayerMask;
    private readonly Vector3 _damageZoneSize = new (0.25f, 0.85f, 0.5f);
    private readonly float _transitionStep = 0.33f;
    private readonly int AttackTransitionHash = Animator.StringToHash("AttackTransition");
    private readonly int AttackNumberHash = Animator.StringToHash("AttackNumber");

    private int _currentAttackCount = 1;
    private float _currentTransitionValue;       

    public AttackState(Humanoid humanoid) : base(humanoid)
    {
        _animator = Humanoid.Animator;
        _damageZone = Humanoid.DamageZone;
        _dealDamageLayerMask = Humanoid.HumanoidData.DealDamageMask;
    }

    public bool IsAttacking { get; private set; }

    public override void Enter()
    {
        _currentTransitionValue = 0f;
        _animator.SetInteger(AttackNumberHash, _currentAttackCount);
        _animator.SetFloat(AttackTransitionHash, _currentTransitionValue);
        IsAttacking = true;  
    }

    public override void Exit()
    {       
        _currentTransitionValue = 0f;
        _animator.SetInteger(AttackNumberHash, 0);
        _animator.SetFloat(AttackTransitionHash, _currentTransitionValue);
        Humanoid.EndAttack();
        Humanoid.ResetAttack();
    }

    public override void Update()
    {
        if (Humanoid.IsAttacking) 
        {
            if (_currentTransitionValue < 1f)
            {
                _animator.SetFloat(AttackTransitionHash, _currentTransitionValue);
                _currentTransitionValue += _transitionStep;
            }

            return;
        }

        if (IsAttacking)
        {
            if (_currentTransitionValue > 0f)
            {
                _animator.SetFloat(AttackTransitionHash, _currentTransitionValue);
                _currentTransitionValue -= _transitionStep;
            }
            else 
            {
                IsAttacking = false;
                DealDamage();
                _currentAttackCount++;

                if (_currentAttackCount > MaxAttackCount)
                    _currentAttackCount = 1;

            }
        }      
    }

    private void DealDamage() 
    {
        Collider[] overlappingColliders = Physics.OverlapBox
            (_damageZone.transform.position, _damageZoneSize, Quaternion.identity, _dealDamageLayerMask);

        foreach (Collider collider in overlappingColliders)
        {
            if (collider.gameObject.TryGetComponent(out IDamagable damagable))
                damagable.TakeDamage();    
        }
    }
}
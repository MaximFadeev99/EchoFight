using UnityEngine;

public class BotInputHandler : IInputHandler
{
    private const float DodgeDirections = 3f;
    private const float DodgeResetTime = 3f;
    
    private readonly HumanoidEnemyBot _bot;
    private readonly HumanoidData _botData;
    private readonly Transform _botTransform;
    private readonly Timer _attackTimer = new();
    private readonly Timer _dodgeTimer = new();
    private readonly float _dodgeProbability;   
    private readonly float _dodgeDirectionThreshold;
    private readonly float _attackDistance = 0.9f;

    private Player _player;
    private Transform _playerTransform;
    private float _randomDodgeChance;
    private float _currentDistance;
    private bool _canAttack = true;
    private bool _isDodging;
    private bool _canDodge = true;

    public BotInputHandler(HumanoidEnemyBot bot) 
    {
        _bot = bot;
        _botTransform = _bot.Transform;
        _botData = _bot.HumanoidData;
        _dodgeProbability = _botData.DodgeProbability;
        _dodgeDirectionThreshold = _dodgeProbability / DodgeDirections;
    }
    
    public bool GetAttackInput()
    {        
        if (_player != null && _player.IsAlive && _player.IsAttacking == false &&
            _currentDistance <= _attackDistance && _canAttack && _bot.IsHurt == false)
        {
            _canAttack = false;
            _attackTimer.TimeIsUp += ResetAttack;
            _attackTimer.Start(_botData.AttackResetTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetDodgeInput()
    {       
        if (_player != null && _currentDistance < _attackDistance && 
            _player.IsAttacking && _canDodge)
        {
            _randomDodgeChance = Random.Range(0f, 1f);
            _canDodge = false;
            _dodgeTimer.TimeIsUp += ResetDodge;
            _dodgeTimer.Start(DodgeResetTime);

            if (_randomDodgeChance <= _dodgeProbability)
            {
                _isDodging = true;
                return true;
            }
            
            return false;
        }

        return false;
    }

    public Vector3 GetMovementDirection()
    {
        if (_isDodging) 
        {
            _isDodging = false;

            if (0f <= _randomDodgeChance && _randomDodgeChance < _dodgeDirectionThreshold)
            {
                return -Vector3.forward;
            }
            else if (_dodgeDirectionThreshold <= _randomDodgeChance &&
                _randomDodgeChance < _dodgeDirectionThreshold * 2f)
            {
                return Vector3.right;
            }
            else if (_dodgeDirectionThreshold * 2f <= _randomDodgeChance &&
                _randomDodgeChance < _dodgeDirectionThreshold * 3f)
            {
                return -Vector3.right;
            }
            else
            {
                return -Vector3.forward;           
            }
        }
        
        if (_playerTransform == null || _player.IsAlive == false)
            return Vector3.zero;

        if (_currentDistance > _attackDistance)
            return Vector3.forward;
        else         
            return Vector3.zero;
    }

    public void SetPlayer(Player player) 
    {
        _player = player;
        _playerTransform = player.Transform;
    }

    public void Update()
    {
        if (_attackTimer.IsActive)
            _attackTimer.Tick();

        if (_dodgeTimer.IsActive)
            _dodgeTimer.Tick();

        CalculateDistanceToPlayer();

        if (_currentDistance > _attackDistance) 
        {
            _bot.EndAttack();
            _bot.ResetAttack();
        }
    }

    private void CalculateDistanceToPlayer() 
    {
        if (_playerTransform != null)
            _currentDistance = Vector3.Distance(_playerTransform.position, _botTransform.position);
    }

    private void ResetAttack() 
    {
        _canAttack = true;
        _attackTimer.TimeIsUp -= ResetAttack;
    }

    private void ResetDodge() 
    {
        _canDodge = true;
        _dodgeTimer.TimeIsUp -= ResetDodge;
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "HumanoidData",menuName = "Data/Humanoid Data", order = 51)]
public class HumanoidData : ScriptableObject
{
    [SerializeField] private float _movementSpeed = 100f;
    [SerializeField] private float _attackResetTime = 0f;
    [SerializeField] private int _startHealth = 5;
    [SerializeField] private LayerMask _dealDamageMask;
    [SerializeField] private float _dodgeProbability = 1f;
    [SerializeField] private float _dodgeSpeed = 3f;
    [SerializeField] private string _wireframeMaterialName;

    public float MovementSpeed => _movementSpeed;
    public float AttackResetTime => _attackResetTime;
    public int StartHealth => _startHealth;
    public LayerMask DealDamageMask => _dealDamageMask;
    public float DodgeProbability => _dodgeProbability;
    public string WireframeMaterialName => _wireframeMaterialName;
    public float DodgeSpeed => _dodgeSpeed;
}
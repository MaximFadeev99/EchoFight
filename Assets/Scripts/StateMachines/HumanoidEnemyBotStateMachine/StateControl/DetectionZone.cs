using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    private IEnemy _bot;

    private void Awake()
    {
        _bot = GetComponentInParent<IEnemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player)) 
            _bot.SetPlayer(player);            
    }
}
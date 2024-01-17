using System.Collections.Generic;
using UnityEngine;

public class SoundableTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 _collisionForce = Vector3.forward;

    private Transform _transform;
    private CollisionLogger _collisionLogger;

    private void Awake()
    {
        _transform = transform;
        _collisionLogger = FindFirstObjectByType<CollisionLogger>();
    }

    private void OnTriggerExit(Collider other)
    {
        KeyValuePair<Vector3, Vector3> collisionBundle = new(_transform.position, _collisionForce);
        _collisionLogger.RegisterCollision(collisionBundle);    
    }
}
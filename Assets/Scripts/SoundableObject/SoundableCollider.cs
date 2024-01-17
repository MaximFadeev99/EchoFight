using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SoundableCollider: MonoBehaviour
{
    private readonly Dictionary<Vector3, Vector3> _currentCollisionBundles = new ();

    [SerializeField] private float _velocityThreshold = 5f;

    private CollisionLogger _collisionLogger;
    private Rigidbody _rigidbody;
    private Vector3 _previousContactPoint = Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collisionLogger = FindFirstObjectByType<CollisionLogger>();
    }

    private void OnCollisionEnter(Collision collision) 
    {
        ContactPoint collisionPoint = collision.GetContact(0);
        KeyValuePair<Vector3, Vector3> collisionBundle = new(collisionPoint.point, collisionPoint.impulse);

        _collisionLogger.RegisterCollision(collisionBundle);
        _previousContactPoint = collisionPoint.point;
    }

    private void OnCollisionStay (Collision collision) 
    {
        if (Mathf.Abs(_rigidbody.velocity.x) <= _velocityThreshold &&
            Mathf.Abs(_rigidbody.velocity.y) <= _velocityThreshold &&
            Mathf.Abs(_rigidbody.velocity.z) <= _velocityThreshold)
            return;

        List<ContactPoint> contactPoints = new();
        collision.GetContacts(contactPoints);    

        for (int i = 0; i < contactPoints.Count - 1; i++)
        {
            if (Vector3.Distance(contactPoints[i].point, _previousContactPoint) > 0.5f)
            {
                _currentCollisionBundles.Add(contactPoints[i].point, contactPoints[i].impulse);
                _previousContactPoint = contactPoints[i].point;
            }
        }

        _collisionLogger.RegisterCollision(_currentCollisionBundles.ToArray());
        _currentCollisionBundles.Clear();
    }
}
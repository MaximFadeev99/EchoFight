using System.Collections.Generic;
using UnityEngine;

public class CollisionLogger : MonoBehaviour
{
    private readonly Dictionary<Vector3, Vector3> _collisionPoints = new ();
  
    public void RegisterCollision(params KeyValuePair<Vector3,Vector3>[] newContactPoints)
    {
        foreach (KeyValuePair<Vector3, Vector3> keyValuePair in newContactPoints) 
        {
            if (_collisionPoints.ContainsValue(keyValuePair.Value) == false)
                _collisionPoints.Add(keyValuePair.Key, keyValuePair.Value);       
        }
    }

    public IReadOnlyDictionary<Vector3, Vector3> GetCollisionBundles() 
    {
        return _collisionPoints;
    }

    private void LateUpdate() =>
        _collisionPoints.Clear();
}
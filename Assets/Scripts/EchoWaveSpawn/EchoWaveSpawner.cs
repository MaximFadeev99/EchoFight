using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EchoWaveSpawner : MonoBehaviour
{
    private const int PoolStartCount = 20;
    
    private readonly List<EchoWave> _echoWavePool = new ();

    [SerializeField] private EchoWave _echoWavePrefab;
    [SerializeField] private float _echoWaveDefaultEndScale = 6;
    [SerializeField] private float _echoWaveDefaultLifeTime = 1.5f;
    [SerializeField] private float _forceLimitingRate = 0.8f;
    [SerializeField] private float _maxScaleModifier = 2f;

    private CollisionLogger _collisionLogger;
    private Transform _transform;
    private float _previousForce = 1f;

    private void Awake()
    {
        _collisionLogger = FindFirstObjectByType<CollisionLogger>();
        _transform = transform;

        PopulatePool();
    }

    private void PopulatePool()
    {
        for (int i = 0; i < PoolStartCount; i++) 
            _echoWavePool.Add(Instantiate(_echoWavePrefab, _transform));
    }

    private void Update()
    {
        foreach (KeyValuePair<Vector3, Vector3> collisionBundle in _collisionLogger.GetCollisionBundles())
            SpawnEchoWave(collisionBundle);
    }

    private void SpawnEchoWave(KeyValuePair<Vector3,Vector3> collisionBundle) 
    {
        EchoWave spawnedEchoWave = _echoWavePool.FirstOrDefault(echoWave => echoWave.gameObject.activeSelf == false);

        if (spawnedEchoWave == null) 
        {
            spawnedEchoWave = Instantiate(_echoWavePrefab, _transform);
            _echoWavePool.Add(spawnedEchoWave);
        }

        float endScale = CalculateEndScale(collisionBundle.Value);
        float lifeTime = CalculateLifeTime(endScale);

        spawnedEchoWave.Shoot(collisionBundle.Key, lifeTime, endScale);
    }

    private float CalculateEndScale(Vector3 impulse)
    {
        float force = Mathf.Abs(impulse.x) + Mathf.Abs(impulse.y) + Mathf.Abs(impulse.z);
        float endScale = force == 0 ? _previousForce * _forceLimitingRate : force;

        endScale = endScale > _echoWaveDefaultEndScale * _maxScaleModifier ?
            _echoWaveDefaultEndScale * _maxScaleModifier : endScale;
        _previousForce = force;
        return endScale;       
    }

    private float CalculateLifeTime(float endScale) 
    {
        float lifeTime = endScale / _echoWaveDefaultEndScale * _echoWaveDefaultLifeTime;

        lifeTime = lifeTime > _echoWaveDefaultLifeTime * _maxScaleModifier ? 
            _echoWaveDefaultLifeTime * _maxScaleModifier : lifeTime;
        return lifeTime;
    }
}
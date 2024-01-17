using UnityEngine;
using DG.Tweening;

public class EchoWave : MonoBehaviour
{   
    private Transform _transform;
    private Tween _tween;
    private Vector3 _startScale;

    public void Shoot(Vector3 worldPosition, float lifeTime, float endScale)
    {
        _transform.position = worldPosition;
        gameObject.SetActive(true);
        _tween = _transform.DOScale(endScale, lifeTime);
        _tween.onComplete = () =>
        {
            _transform.localScale = _startScale;
            gameObject.SetActive(false);
        };
    }

    private void Awake()
    {
        _transform = transform;
        _startScale = _transform.localScale;
        gameObject.SetActive(false);
    }   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HighlightableObject highlightableObject)) 
            highlightableObject.Highlight();
    }
}
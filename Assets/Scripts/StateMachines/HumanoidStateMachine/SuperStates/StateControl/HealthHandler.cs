using System;
using UnityEngine;
using DG.Tweening;
using System.Linq;

[Serializable]
public class HealthHandler
{
    [SerializeField][ColorUsage(true, true)] private Color _deadColor;

    private int _maxHealth;
    private Material _wireframeMaterial;
    private Color _startColor;
    private Color _currentColor;
    private float _lerpStep;
    private float _currentLerpStep;

    public Action HealthIsZero;

    public int CurrentHealth { get; private set; }  

    public void Intialize(int maxHealth, Renderer renderer, string materialName) 
    {
        _maxHealth = maxHealth;
        CurrentHealth = _maxHealth;
        _wireframeMaterial = renderer.materials.FirstOrDefault(material => material.name == materialName);

        if (_wireframeMaterial == null)
            throw new ArgumentNullException("WireframeMaterial is not found");

        _startColor = _wireframeMaterial.color;
        _currentColor = _startColor;
        _lerpStep = 1f / _maxHealth;
        _currentLerpStep = _lerpStep;
    }

    public void ReduceHealth() 
    {
        CurrentHealth -= 1;
        ChangeColor();

        if (CurrentHealth <= 0)
            HealthIsZero?.Invoke();  
    }

    private void ChangeColor() 
    {
        if (_wireframeMaterial == null)
            throw new ArgumentNullException("The wireframe material is not set. Intialize HealthHandler first");
                
        LerpCurrentColor();
        _wireframeMaterial.DOColor(_currentColor, 1f);
    }

    private void LerpCurrentColor() 
    {
        _currentColor = Color.Lerp(_currentColor, _deadColor, _currentLerpStep);
        _currentLerpStep += _lerpStep;
    }
}
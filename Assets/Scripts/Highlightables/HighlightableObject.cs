using System.Collections;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class HighlightableObject : MonoBehaviour
{
    private const string MaterialName = "WireframeMaterial (Instance)";
    private const string Thickness = "_Thickness";
    private const float TransparentThickness = -0.5f;
    private const float OpaqueThickness = 1f;

    [SerializeField][ColorUsage(true,true)] private Color _wireframeColor;
    [SerializeField] private float _highlightedTime = 3f;
    [SerializeField] private Renderer _renderer;
    
    private Material _wireframeMaterial;
    private Coroutine _highlightingCoroutine;
    private float _timer = 0f;

    public void Highlight()
    {
        if (_highlightingCoroutine == null)
            _highlightingCoroutine = StartCoroutine(ChangeThickness());
        else
            _timer = 0f;
    }

    private void OnValidate()
    {
        if (_renderer == null)
            _renderer = GetComponent<Renderer>();
    }

    private void Awake()
    {
        _wireframeMaterial = _renderer.materials.FirstOrDefault
            (material => material.name == "WireframeMaterial (Instance)");

        if (_wireframeMaterial == null)
        {           
            Debug.Log($"{gameObject.name} doesn't have {MaterialName} on it");
            enabled = false;
            return;
        }

        _wireframeMaterial.color = _wireframeColor;
        _wireframeMaterial.SetFloat(Thickness, TransparentThickness);
    }

    private IEnumerator ChangeThickness()
    {
        _wireframeMaterial.SetFloat(Thickness, OpaqueThickness);

        while (_timer < _highlightedTime)
        {
            _timer += Time.deltaTime;
            yield return null;
        }

        _timer = 0f;
        _wireframeMaterial.DOFloat(TransparentThickness, Thickness, 2f);
        _highlightingCoroutine = null;
    }
}
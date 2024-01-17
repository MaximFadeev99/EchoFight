using UnityEngine;

[System.Serializable]
public class Raycaster 
{
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _groundLayerMask;

    public bool IsGrounded() 
    {
        return Physics.Raycast
            (_raycastPoint.position, Vector3.down, _groundCheckDistance, _groundLayerMask);
    }
}
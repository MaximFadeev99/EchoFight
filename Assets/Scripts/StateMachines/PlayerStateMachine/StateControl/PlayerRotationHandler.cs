using UnityEngine;

[System.Serializable]
public class PlayerRotationHandler : IRotationHandler  
{
    private readonly Transform _transform;

    [SerializeField] private float _mouseSensitivity = 1f;  
    
    private float _turn;

    public PlayerRotationHandler(Transform player) 
    {
        Cursor.lockState = CursorLockMode.Locked;
        _transform = player;
    }

    public void UpdateRotation() 
    {
        _turn += Input.GetAxis("Mouse X") * _mouseSensitivity;
        _transform.rotation = Quaternion.Euler(0f, _turn, 0f);
    }

    public void ResetTurn() 
    {
        _turn = _transform.rotation.eulerAngles.y;
    }
}
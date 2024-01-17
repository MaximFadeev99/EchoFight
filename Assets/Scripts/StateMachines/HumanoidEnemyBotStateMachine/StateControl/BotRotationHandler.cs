using UnityEngine;

public class BotRotationHandler : IRotationHandler
{
    private readonly Transform _botTransform;

    private Transform _playerTransform; 

    public BotRotationHandler(Transform botTransform) 
    {
        _botTransform = botTransform;
    }
    
    public void ResetTurn(){}

    public void UpdateRotation()
    {
        if (_playerTransform != null)
            _botTransform.LookAt(_playerTransform.position);    
    }

    public void SetPlayer(Transform playerTransform) 
    {
        _playerTransform = playerTransform;
    }
}
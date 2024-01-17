using UnityEngine;

public interface IInputHandler
{
    public void Update();
    
    public Vector3 GetMovementDirection();

    public bool GetAttackInput();

    public bool GetDodgeInput();
}
using UnityEngine;

public class PlayerInputHandler : IInputHandler    
{
    public Vector3 GetMovementDirection() 
    {      
        return new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));           
    }

    public bool GetDodgeInput() 
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetAttackInput() 
    {
        return Input.GetMouseButtonDown(0);
    }

    public void Update() {}
}
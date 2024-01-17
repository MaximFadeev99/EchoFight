using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public SuperState CurrentSuperState { get; protected set; }
    public State CurrentState { get; protected set; }

    private void Update()
    {
        CurrentSuperState = SetSuperState();
        CurrentState = CurrentSuperState.SetState();
    }

    private void FixedUpdate()
    {
        CurrentSuperState.CurrentState.Update();
    }

    protected abstract SuperState SetSuperState();

    protected void SetToDefault(SuperState defaultSuperState, State defaultState) 
    {
        CurrentSuperState = defaultSuperState;
        CurrentState = defaultState;
    }
}
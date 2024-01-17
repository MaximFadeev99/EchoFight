using UnityEngine;

public class FallingState : HumanoidState
{
    private readonly Animator _animator;
    private readonly int FallingStateHash = Animator.StringToHash("Falling");
       
    public FallingState(Humanoid humanoid) : base(humanoid)
    {
        _animator = Humanoid.Animator;       
    }

    public override void Enter()
    {
        _animator.Play(FallingStateHash);
    }

    public override void Exit(){}

    public override void Update(){}
}
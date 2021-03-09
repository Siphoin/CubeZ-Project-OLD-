using System.Collections;

public interface IStateBehavior
{
    void Enter();
    void Exit();
    void Update();

    IEnumerator UpdateWaiting();
}
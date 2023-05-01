namespace Common.ApplicationStateMachine
{
    public interface IState
    {
        void OnEnter();
        void OnExit();
    }
}
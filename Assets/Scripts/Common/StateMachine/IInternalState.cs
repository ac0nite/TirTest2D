namespace Common.ApplicationStateMachine
{
    public interface IInternalState<TEnum>
    {
        IInternalState<TEnum> GoesTo(TEnum nextStateEnum);
        IState State { get; }
        bool IsNextState();
        bool IsNextState(TEnum state);
        TEnum NextState();
    }
}
using System;

namespace Common.StateMachine
{
    public interface IStateMachine<TEnum> where TEnum : Enum
    {
        public TEnum CurrentTypeState { get; }
        InternalState<TEnum> Register(TEnum type, IState state);
        void NextState();
        void NextState(TEnum type);
        void ForceNextState(TEnum type);
        void Run(TEnum type);
    }
}
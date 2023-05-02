using System;
using System.Collections.Generic;
using ModestTree;

namespace Common.StateMachine
{
    public class InternalState<TEnum> : IInternalState<TEnum> where TEnum : Enum
    {
        private readonly IState _state;
        private readonly List<TEnum> _nextStates;

        public InternalState(IState state)
        {
            _state = state;
            _nextStates = new List<TEnum>();
        }
        public IInternalState<TEnum> GoesTo(TEnum nextStateEnum)
        {
            _nextStates.Add(nextStateEnum);
            return this;
        }
        public IState State => _state;
        public bool IsNextState() => !_nextStates.IsEmpty();
        public bool IsNextState(TEnum state) => _nextStates.Contains(state);
        public TEnum NextState()
        {
            if (!IsNextState()) throw new Exception("Next state is missing");
            return _nextStates[0];
        }
    }
}
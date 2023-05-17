using Application.UI.Common;
using UnityEngine;
using Zenject;

namespace Gameplay.StateMachine.States
{
    public class ResultGameplayState : BaseGameplayState
    {
        public ResultGameplayState(
            SignalBus signals,
            IScreenController screenController) : base(signals, screenController)
        {
            
        }
        public override void OnEnter()
        {
            Debug.Log($"RESULT STATE");
        }

        public override void OnExit()
        {
        }

        public class Factory : BaseFactory
        {
            
        }
    }
}
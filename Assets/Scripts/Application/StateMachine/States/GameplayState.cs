using Application.SceneLoader;
using Common.StateMachine;
using UnityEngine;
using Zenject;

namespace Application.StateMachine.States
{
    public class GameplayState : IState
    {
        private readonly ISceneLoader _sceneLoader;

        public GameplayState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        public void OnEnter()
        {
            ApplicationConstants.GameplaySceneRequest.OnCompleted = SceneLoaded;
            _sceneLoader.LoadSceneAsync(ApplicationConstants.GameplaySceneRequest);
        }

        private void SceneLoaded()
        {
            //
        }

        public void OnExit()
        {
        }
        
        #region FACTORY

        public class Factory : PlaceholderFactory<IState>
        {
        }

        #endregion
    }
}
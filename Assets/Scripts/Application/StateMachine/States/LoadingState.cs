using Common.ApplicationStateMachine;
using Common.SceneLoader;
using UnityEngine;
using Zenject;

namespace Application.StateMachine.States
{
    public class LoadingState : IState
    {
        private readonly SceneLoader _sceneLoader;

        public LoadingState(
            SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        public void OnEnter()
        {
            ApplicationConstants.LoadingSceneRequest.OnCompleted = SceneLoaded;
            _sceneLoader.LoadSceneAsync(ApplicationConstants.LoadingSceneRequest);
        }

        private void SceneLoaded()
        {
            Debug.Log($"SCENE 'LOADING' LOADED");
            ApplicationConstants.LoadingSceneRequest.OnCompleted = null;
        }

        public void OnExit()
        {
            _sceneLoader.UnLoadSceneAsync(ApplicationConstants.LoadingSceneRequest);
            Debug.Log($"SCENE 'LOADING' UNLOAD");
        }

        #region FACTORY

        public class Factory : PlaceholderFactory<IState>
        {
        }

        #endregion
    }
}
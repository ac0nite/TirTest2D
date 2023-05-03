using Application.UI;
using Application.UI.Common;
using Common.StateMachine;
using UnityEngine;
using Zenject;

namespace Application.StateMachine.States
{
    public class LoadingState : IState
    {
        private readonly SceneLoader.SceneLoader _sceneLoader;
        private readonly IScreenController _screenController;

        public LoadingState(
            SceneLoader.SceneLoader sceneLoader,
            IScreenController screenController)
        {
            _sceneLoader = sceneLoader;
            _screenController = screenController;
        }
        public void OnEnter()
        {
            _screenController.Show(GameplayScreenType.APPLICATION);
            ApplicationConstants.LoadingSceneRequest.OnCompleted = SceneLoaded;
            _sceneLoader.LoadSceneAsync(ApplicationConstants.LoadingSceneRequest);
        }

        private void SceneLoaded()
        {
            ApplicationConstants.LoadingSceneRequest.OnCompleted = null;
        }

        public void OnExit()
        {
            _sceneLoader.UnLoadSceneAsync(ApplicationConstants.LoadingSceneRequest);
        }

        #region FACTORY

        public class Factory : PlaceholderFactory<IState>
        {
        }

        #endregion
    }
}
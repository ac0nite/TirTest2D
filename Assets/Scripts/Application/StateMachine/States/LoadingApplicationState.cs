using Application.UI;
using Application.UI.Common;
using Common.StateMachine;
using Resources;
using UnityEngine;
using Zenject;

namespace Application.StateMachine.States
{
    public class LoadingApplicationState : IState
    {
        private readonly SceneLoader.SceneLoader _sceneLoader;
        private readonly IScreenController _screenController;

        public LoadingApplicationState(
            SceneLoader.SceneLoader sceneLoader,
            IScreenController screenController)
        {
            _sceneLoader = sceneLoader;
            _screenController = screenController;
        }
        public void OnEnter()
        {
            _screenController.Show(GameplayScreenType.SPLASH);
            Constants.LoadingSceneRequest.OnCompleted = SceneLoaded;
            _sceneLoader.LoadSceneAsync(Constants.LoadingSceneRequest);
        }

        private void SceneLoaded()
        {
            Constants.LoadingSceneRequest.OnCompleted = null;
            Debug.Log($"SCENE '{Constants.LoadingSceneRequest.Name}' LOADED");
        }

        public void OnExit()
        {
            Constants.LoadingSceneRequest.OnCompleted = null;
            _sceneLoader.UnLoadSceneAsync(Constants.LoadingSceneRequest);
        }

        #region FACTORY

        public class Factory : PlaceholderFactory<IState>
        {
        }

        #endregion
    }
}
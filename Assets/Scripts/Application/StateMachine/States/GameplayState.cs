using Application.SceneLoader;
using Application.UI;
using Application.UI.Common;
using ApplicationConstant;
using Common.StateMachine;
using UnityEngine;
using Zenject;

namespace Application.StateMachine.States
{
    public class GameplayState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IScreenController _screenController;

        public GameplayState(
            ISceneLoader sceneLoader,
            IScreenController screenController)
        {
            _sceneLoader = sceneLoader;
            _screenController = screenController;
        }
        public void OnEnter()
        {
            Constants.GameplaySceneRequest.OnCompleted = SceneLoaded;
            _sceneLoader.LoadSceneAsync(Constants.GameplaySceneRequest);
        }

        private void SceneLoaded()
        {
            Debug.Log($"SCENE '{Constants.GameplaySceneRequest.Name}' LOADED");
            _screenController.Hide(GameplayScreenType.APPLICATION);
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
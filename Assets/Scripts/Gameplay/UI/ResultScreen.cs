using Application.UI;
using Application.UI.Common;
using Gameplay.Enemy.Settings;
using Gameplay.Models;
using Gameplay.StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.UI
{
    public class ResultScreen : BaseGameplayScreen
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private TMP_Text _valueEasyBirdText;
        [SerializeField] private TMP_Text _valueHardBirdText;
        
        private SignalBus _signals;
        private IGameplayModelGetter _gameplayModel;

        [Inject]
        public void Construct(
            IScreenController screenController,
            SignalBus signals,
            IGameplayModelGetter gameplayModel)
        {
            _signals = signals;
            _gameplayModel = gameplayModel;

            GetUIComponents();
            screenController.Add(GameplayScreenType.RESULT, this);
            
            _nextButton.onClick.AddListener(OnNextButtonPressed);
        }

        public override void Show()
        {
            ShowUpdateData();
            _canvas.enabled = true;
        }

        public override void Hide()
        {
            _canvas.enabled = false;
        }
        
        private void ShowUpdateData()
        {
            var levelData = _gameplayModel.LevelData;
            var resultData = _gameplayModel.ResultData;
            
            var a = levelData[EnemyType.EASY];
            var b = resultData[EnemyType.EASY];
            _valueEasyBirdText.text = $"{a}-{a - b}";

            a = levelData[EnemyType.HARD];
            b = resultData[EnemyType.HARD];
            _valueHardBirdText.text = $"{a}-{a - b}";
        }
        
        private void OnNextButtonPressed()
        {
            _signals.Fire(new GameplayStateMachine.Signals.NextState(GameplayStateEnum.LOADING));
        }

        public override void Dispose()
        {
            _nextButton.onClick.RemoveListener(OnNextButtonPressed);
        }
    }
}
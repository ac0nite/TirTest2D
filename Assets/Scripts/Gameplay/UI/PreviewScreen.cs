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
    public class PreviewScreen : BaseGameplayScreen
    {
        [SerializeField] private Button _playButton;
        
        [SerializeField] private TMP_Text _valueLevelText;
        [SerializeField] private TMP_Text _valueTimeLevelText;
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
            screenController.Add(GameplayScreenType.PREVIEW, this);
            
            _playButton.onClick.AddListener(OnPlayButtonPressed);
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
            _valueLevelText.text = _gameplayModel.Level.ToString();
            _valueTimeLevelText.text = _gameplayModel.LevelTime.ToString();
            _valueEasyBirdText.text = levelData[EnemyType.EASY].ToString();
            _valueHardBirdText.text = levelData[EnemyType.HARD].ToString();
        }
        
        private void OnPlayButtonPressed()
        {
            _signals.Fire(new GameplayStateMachine.Signals.NextState(GameplayStateEnum.RUN_PLAY));
        }

        public override void Dispose()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonPressed);
        }
    }
}
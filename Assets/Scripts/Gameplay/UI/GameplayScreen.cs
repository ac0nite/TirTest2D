using System;
using Application.UI;
using Application.UI.Common;
using Gameplay.Bullets;
using Gameplay.Enemy;
using Gameplay.Enemy.Settings;
using Gameplay.Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace Gameplay.UI
{
    public class GameplayScreen : BaseGameplayScreen
    {
        [SerializeField] private TMP_Text _valueTimerText;
        [SerializeField] private TMP_Text _valueTargetsHitText;

        private SignalBus _signals;
        private IEnemySpawner _enemySpawner;
        private int _hitCounter;
        private CustomDoTweenTimer _timer;
        private int _timerValue;
        private IGameplayModelGetter _gameplayModel;

        [Inject]
        public void Construct(
            IScreenController screenController,
            SignalBus signals,
            IEnemySpawner enemySpawner,
            IGameplayModelGetter gameplayModel
        )
        {
            _signals = signals;
            _enemySpawner = enemySpawner;
            _gameplayModel = gameplayModel;

            GetUIComponents();
            _timer = new CustomDoTweenTimer(1);
            
            screenController.Add(GameplayScreenType.GAMEPLAY, this);
        }

        public override void Show()
        {
            InitShow();
            _canvas.enabled = true;
        }

        public override void Hide()
        {
            _canvas.enabled = false;
            ResetShow();
        }

        #region SHOW INFO

        private void InitShow()
        {
            _hitCounter = 0;
            _valueTargetsHitText.text = _hitCounter.ToString();
            _enemySpawner.OnTargetHit += ChangeShowHit;
            _timerValue = _gameplayModel.LevelTime;
            _valueTimerText.text = _timerValue.ToString();
            _timer.RunLoop(ChangeValueTimer);
        }

        private void ChangeValueTimer()
        {
            _timerValue--;
            _valueTimerText.text = Mathf.Clamp(_timerValue, 0, Single.MaxValue).ToString();
        }
        private void ChangeShowHit(EnemyType type)
        {
            _hitCounter++;
            _valueTargetsHitText.text = _hitCounter.ToString();
        }

        private void ResetShow()
        {
            _timer.Dispose();
            _enemySpawner.OnTargetHit -= ChangeShowHit;
        }

        #endregion

        public override void Dispose()
        {
        }
    }
}
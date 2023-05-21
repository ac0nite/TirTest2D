using System.Linq;
using DG.Tweening;
using Gameplay.Bullets;
using Gameplay.Enemy;
using Gameplay.Enemy.Settings;
using Gameplay.Models;
using Gameplay.Settings;
using Gameplay.StateMachine;
using MyBox;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public interface ILevelManager
    {
        void Initialise();
        void Start(float delayTime);
        bool IsNextLevel { get; }
        void Clear();
    }
    
    public class GameplayLevelManager : ILevelManager
    {
        private readonly IEnemySpawnGenerator _enemySpawnGenerator;
        private readonly GameplaySettings _gameplaySettings;
        private readonly IGameplayModelSetter _gameplayModel;
        private LevelBalanceSO _balance;
        private CustomDoTweenTimer _timer;
        private readonly IEnemySpawner _enemySpawner;
        private readonly SignalBus _signals;

        public GameplayLevelManager(
            IEnemySpawnGenerator enemySpawnGenerator,
            GameplaySettings gameplaySettings,
            IGameplayModelSetter gameplayModel,
            IEnemySpawner enemySpawner,
            SignalBus signals)
        {
            _enemySpawnGenerator = enemySpawnGenerator;
            _gameplaySettings = gameplaySettings;
            _gameplayModel = gameplayModel;
            _enemySpawner = enemySpawner;
            _signals = signals;
        }

        public void Initialise()
        {
            _enemySpawnGenerator.Initialise();
            _balance = _gameplaySettings.Levels[_gameplayModel.Level].Balance;
            _timer = new CustomDoTweenTimer(_balance.LevelTime);
            _enemySpawner.OnTargetHit += CountingDeaths;
            _gameplayModel.LevelTime = _balance.LevelTime;
            InitialiseCounters();
        }

        private void InitialiseCounters()
        {
            _gameplayModel.LevelData.Clear();
            _gameplayModel.ResultData.Clear();
            
            _balance.BirdCounters.ForEach(c =>
            {
                _gameplayModel.LevelData.Add(c.Type, c.Count);
                _gameplayModel.ResultData.Add(c.Type, c.Count);
            });
        }

        private void CountingDeaths(EnemyType type)
        {
            if (_gameplayModel.ResultData.ContainsKey(type))
            {
                if (_gameplayModel.ResultData[type] > 0)
                {
                    _gameplayModel.ResultData[type]--;
                }

                if (IsNextLevel) 
                    Completed();
            }
        }

        public void Start(float delayTime)
        {
            DOVirtual.DelayedCall(delayTime, () =>
            {
                _timer.Run(Completed);
                _enemySpawnGenerator.Run();
            });
        }

        public void Clear()
        {
            _enemySpawner.AllDeSpawn();
        }

        public bool IsNextLevel => !_gameplayModel.ResultData.IsNullOrEmpty() && _gameplayModel.ResultData.All(i => i.Value == 0);

        private void Completed()
        {
            _enemySpawner.OnTargetHit -= CountingDeaths;
            _timer.Dispose();
            _enemySpawnGenerator.Stop();
            
            _signals.Fire(new GameplayStateMachine.Signals.NextState(GameplayStateEnum.RESULT));
        }
    }
}
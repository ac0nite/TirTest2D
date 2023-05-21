using System;
using Gameplay.Bullets;
using Gameplay.Enemy.Settings;
using Gameplay.Models;
using Gameplay.Settings;
using MyBox;
using UnityEngine;

namespace Gameplay.Enemy
{
    public interface IEnemySpawnGenerator
    {
        void Initialise();
        void Run();
        void Stop();
    }
    
    public class EnemySpawnGenerator : IEnemySpawnGenerator, IDisposable
    {
        private readonly GameplayModel _gameplayModel;
        private readonly GameplaySettings _gameplaySettings;
        private EnemyParam[] _settings;
        private readonly IEnemySpawner _enemySpawner;
        private readonly EnemyResource[] _resources;
        private readonly Array _enemyTypes;
        private LevelBalanceSO _balance;
        private CustomDoTweenTimer _timer;
        private int _spawnAmount;

        public EnemySpawnGenerator(
            GameplayModel gameplayModel,
            GameplaySettings gameplaySettings,
            GameplayResources gameplayResources,
            IEnemySpawner enemySpawner)
        {
            _gameplayModel = gameplayModel;
            _gameplaySettings = gameplaySettings;
            _resources = gameplayResources.EnemyResources;
            _enemySpawner = enemySpawner;

            _enemyTypes = Enum.GetValues(typeof(EnemyType));
        }

        public void Initialise()
        {
            _settings = _gameplaySettings.Levels[_gameplayModel.Level].Birds.EnemyParams;
            _balance = _gameplaySettings.Levels[_gameplayModel.Level].Balance;
            _timer = new CustomDoTweenTimer(_balance.SpawnTime);
            _spawnAmount = 0;
            _balance.BirdCounters.ForEach(i => _spawnAmount += i.Count);
            
            //+20%
            _spawnAmount = Mathf.RoundToInt(_spawnAmount * 1.2f);
        }

        public void Run()
        {
            _timer.RunLoop(TryToSpawn);
        }

        private void TryToSpawn()
        {
            if(_spawnAmount >= _enemySpawner.Counter) 
                Spawn();
        }

        private void Spawn()
        {
            EnemyType randomType = (EnemyType)UnityEngine.Random.Range(0, _enemyTypes.Length);
            
            var r = GetResource(randomType);
            var p = GetParam(randomType);
            
            _enemySpawner.Spawn(p, r);
        }

        private EnemyResource GetResource(EnemyType type) => Array.Find(_resources, res => res.Type == type);
        private EnemyParam GetParam(EnemyType type) => Array.Find(_settings, set => set.Type == type);

        public void Stop()
        {
            _timer.Dispose();
        }

        public void Dispose()
        {
        }
    }
}
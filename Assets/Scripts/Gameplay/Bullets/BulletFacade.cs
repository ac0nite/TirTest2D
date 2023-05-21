using System;
using System.Collections.Generic;
using Gameplay.Bullets.Settings;
using Gameplay.Models;
using Gameplay.Settings;
using UnityEngine;

namespace Gameplay.Bullets
{
    public interface IBulletFacade
    {
        Bullet Spawn(Vector2 startPosition);
        BulletParam CurrentBulletParam { get; }
    }
    
    public class BulletFacade : IBulletFacade, IDisposable
    {
        private readonly Dictionary<BulletType, IBulletSpawner> _bulletDict;
        private readonly IGameplayModelGetter _gameplayModelGetter;
        private readonly GameplaySettings _gameplaySettings;
        
        private BulletType _currentType;
        private BulletParam _currentParam;

        public BulletFacade(
            GameplaySettings gameplaySettings,
            IGameplayModelGetter gameplayModelGetter,
            BombSpawner bombSpawner,
            CannonballSpawner cannonballSpawner)
        {
            _gameplaySettings = gameplaySettings;
            _gameplayModelGetter = gameplayModelGetter;

            _bulletDict = new Dictionary<BulletType, IBulletSpawner>();
            _bulletDict.Add(BulletType.BOMB, bombSpawner);
            _bulletDict.Add(BulletType.CANNONBALL, cannonballSpawner);

            _gameplayModelGetter.EventChangeWeapon += OnChangeGameplay;
            _gameplayModelGetter.EventChangeLevel += InitGameplayParam;
        }

        private void InitGameplayParam(int level)
        {
            var settings = _gameplaySettings.Levels[level];
            _currentParam = Array.Find(settings.Bullets.BulletParams, p => p.Type == _currentType);
        }

        private void OnChangeGameplay(BulletType type)
        {
            _currentType = type;
        }

        public Bullet Spawn(Vector2 position)
        {
            if (_bulletDict.ContainsKey(_currentType))
                return _bulletDict[_currentType].Spawn(position, _currentParam);

            return null;
        }

        public BulletParam CurrentBulletParam => _currentParam;

        public void Dispose()
        {
            _gameplayModelGetter.EventChangeWeapon -= OnChangeGameplay;
            _gameplayModelGetter.EventChangeLevel -= InitGameplayParam;
        }
    }
}
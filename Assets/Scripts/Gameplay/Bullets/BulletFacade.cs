using System;
using System.Collections.Generic;
using Gameplay.Settings;
using UnityEngine;

namespace Gameplay.Bullets
{
    public interface IBulletFacade
    {
        Bullet Spawn(Vector2 startPosition);
    }
    
    public class BulletFacade : IBulletFacade, IDisposable
    {
        private readonly Dictionary<BulletType, IBulletSpawner> _bulletDict;
        private readonly IWeaponModelGetter _weaponModelGetter;
        private readonly GameplaySettings _gameplaySettings;
        
        private BulletType _currentType;
        private BulletParam _currentParam;

        public BulletFacade(
            GameplaySettings gameplaySettings,
            IWeaponModelGetter weaponModelGetter,
            BombSpawner bombSpawner,
            CannonballSpawner cannonballSpawner)
        {
            _gameplaySettings = gameplaySettings;
            _weaponModelGetter = weaponModelGetter;

            _bulletDict = new Dictionary<BulletType, IBulletSpawner>();
            _bulletDict.Add(BulletType.BOMB, bombSpawner);
            _bulletDict.Add(BulletType.CANNONBALL, cannonballSpawner);

            _weaponModelGetter.EventChangeWeapon += OnChangeWeapon;
            _weaponModelGetter.EventChangeLevel += InitWeaponParam;
        }

        private void InitWeaponParam(int level)
        {
            var settings = _gameplaySettings.Levels[level];
            _currentParam = Array.Find(settings.Bullets.BulletParams, p => p.Type == _currentType);
        }

        private void OnChangeWeapon(BulletType type)
        {
            _currentType = type;
        }

        public Bullet Spawn(Vector2 position)
        {
            if (_bulletDict.ContainsKey(_currentType))
                return _bulletDict[_currentType].Spawn(position, _currentParam);

            return null;
        }

        public void Dispose()
        {
            _weaponModelGetter.EventChangeWeapon -= OnChangeWeapon;
            _weaponModelGetter.EventChangeLevel -= InitWeaponParam;
        }
    }

    public interface IWeaponModelSetter
    {
        BulletType BulletType { get; set; }
        int Level { set; get; }

        event Action<BulletType> EventChangeWeapon;
        event Action<int> EventChangeLevel;
    }
    public interface IWeaponModelGetter
    {
        BulletType BulletType { get; }
        int Level { set; }
        
        event Action<BulletType> EventChangeWeapon;
        event Action<int> EventChangeLevel;
    }
    
    public class WeaponModel : IWeaponModelSetter, IWeaponModelGetter
    {
        private BulletType _bulletType;
        private int _level;
        private readonly GameplaySettings _gameplaySettings;
        
        public WeaponModel(GameplaySettings gameplaySettings)
        {
            _gameplaySettings = gameplaySettings;
        }

        public event Action<BulletType> EventChangeWeapon;
        public event Action<int> EventChangeLevel;

        public BulletType BulletType
        {
            get => _bulletType;
            set
            {
                _bulletType = value;
                EventChangeWeapon?.Invoke(value);
            }
        }

        public int Level
        {
            get => _level;
            set
            {
                if(value > _gameplaySettings.MaxLevel)
                    return;
                
                _level = value;
                EventChangeLevel?.Invoke(value);
            }
        }
    }
}
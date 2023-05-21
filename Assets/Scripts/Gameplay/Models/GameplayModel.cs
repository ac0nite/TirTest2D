using System;
using System.Collections.Generic;
using Gameplay.Bullets.Settings;
using Gameplay.Enemy.Settings;
using Gameplay.Settings;

namespace Gameplay.Models
{
    
    public interface IGameplayModelSetter
    {
        BulletType BulletType { get; set; }
        int Level { set; get; }

        event Action<BulletType> EventChangeWeapon;
        event Action<int> EventChangeLevel;

        Dictionary<EnemyType, int> LevelData { get; set; }
        Dictionary<EnemyType, int> ResultData { get; set; }
        int LevelTime { get; set; }
    }
    public interface IGameplayModelGetter
    {
        BulletType BulletType { get; }
        int Level { get; }
        
        event Action<BulletType> EventChangeWeapon;
        event Action<int> EventChangeLevel;
        
        IReadOnlyDictionary<EnemyType, int> LevelData { get; }
        IReadOnlyDictionary<EnemyType, int> ResultData { get; }
        int LevelTime { get; }
    }
    
    public class GameplayModel : IGameplayModelSetter, IGameplayModelGetter
    {
        private BulletType _bulletType;
        private int _level;
        private readonly GameplaySettings _gameplaySettings;
        
        public GameplayModel(GameplaySettings gameplaySettings)
        {
            _gameplaySettings = gameplaySettings;
            LevelData = new Dictionary<EnemyType, int>();
            ResultData = new Dictionary<EnemyType, int>();
        }

        public event Action<BulletType> EventChangeWeapon;
        public event Action<int> EventChangeLevel;
        public Dictionary<EnemyType, int> LevelData { get; set; }
        public Dictionary<EnemyType, int> ResultData { get; set; }
        
        IReadOnlyDictionary<EnemyType, int> IGameplayModelGetter.ResultData { get => ResultData; }
        IReadOnlyDictionary<EnemyType, int> IGameplayModelGetter.LevelData { get => LevelData; }

        public int LevelTime { get; set; } = -1;

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
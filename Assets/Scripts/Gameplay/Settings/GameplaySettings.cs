using System;
using System.Linq;
using Gameplay.Bullets;
using Gameplay.Enemy;
using Gameplay.Enemy.Settings;
using Gameplay.Player;
using RotaryHeart.Lib.SerializableDictionary;

namespace Gameplay.Settings
{
    [Serializable]
    public class GameplaySettings
    {
        public LevelSettings Levels;
        public Cannon.Settings CannonSettings;
        public GeneratorRandomSpawnPoint.Settings GeneratorSpawnPointSettings;
        public int MaxLevel => Levels.Keys.ElementAt(Levels.Count - 1);
    }
    
    [Serializable]
    public class LevelSettings : SerializableDictionaryBase<int, LevelParam>
    { }
    
    [Serializable]
    public class LevelParam
    {
        public BulletSettingsSO Bullets;
        public EnemySettingsSO Birds; 
        public LevelBalanceSO Balance;
    }
}
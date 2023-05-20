using System;
using System.Linq;
using Gameplay.Bullets;
using Gameplay.Player;
using RotaryHeart.Lib.SerializableDictionary;

namespace Gameplay.Settings
{
    [Serializable]
    public class GameplaySettings
    {
        public LevelSettings Levels;
        public Cannon.Settings PLayerSettings;
        public int MaxLevel => Levels.Keys.ElementAt(Levels.Count - 1);
    }
    
    [Serializable]
    public class LevelSettings : SerializableDictionaryBase<int, LevelParam>
    { }
    
    [Serializable]
    public class LevelParam
    {
        public BulletSettingsSO Bullets;
        public LevelBalanceSO Balance;
    }
}
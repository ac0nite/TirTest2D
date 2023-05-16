using System;

namespace Gameplay.Bullets
{
    [Serializable]
    public class GameplaySettings
    {
        public LevelSettings[] Settings;
        public int MaxLevel => Settings[Settings.Length - 1].Level;
    }
    
    [Serializable]
    public class LevelSettings
    {
        public int Level;
        public BulletSettingsSO BulletSettings;
    }
}
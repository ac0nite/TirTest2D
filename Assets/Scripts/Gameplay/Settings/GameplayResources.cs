using System;
using Gameplay.Bullets.Settings;
using Gameplay.Enemy.BirdItem;
using Gameplay.Enemy.Settings;
using Gameplay.Player;

namespace Gameplay.Settings
{
    [Serializable]
    public class GameplayResources
    {
        public Cannon CannonPrefab;
        public BulletResource[] BulletResources;
        public Bird BirdPrefab;
        public EnemyResource[] EnemyResources;
    }
}
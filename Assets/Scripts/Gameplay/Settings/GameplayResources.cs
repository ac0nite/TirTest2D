using System;
using Gameplay.Bullets.Settings;
using Gameplay.Enemy.Settings;
using Gameplay.Player;
using UnityEngine.Serialization;

namespace Gameplay.Settings
{
    [Serializable]
    public class GameplayResources
    {
        [FormerlySerializedAs("cannonPrefab")] [FormerlySerializedAs("GunPrefab")] public Cannon CannonPrefab;
        public BulletResource[] BulletResources;
        public EnemyResource[] EnemyResources;
    }
}
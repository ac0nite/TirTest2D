using System;

namespace Gameplay.Bullets.Settings
{
    [Serializable]
    public class BulletParam
    {
        public BulletType Type;
        public int LifeTimeIsOver = 1;
        public float ShotPower = 1;
        public float ShotTimer = 1;
        public int Damage;
    }
}
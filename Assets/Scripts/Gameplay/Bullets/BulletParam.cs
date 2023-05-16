using System;

namespace Gameplay.Bullets
{

    public enum BulletType
    {
        BOMB,
        CANNONBALL
    }


    [Serializable]
    public class BulletParam
    {
        public BulletType Type;
        public int LifeTimeIsOver = 1;
    }
}
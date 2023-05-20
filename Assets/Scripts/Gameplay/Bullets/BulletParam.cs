using System;
using UnityEngine;

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
        public float ShotPower = 1;
        public float ShotTimer = 1;
    }
}
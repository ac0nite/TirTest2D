using System;
using UnityEngine;

namespace Gameplay.Enemy.Settings
{
    [Serializable]
    public class EnemyParam
    {
        public EnemyType Type;
        [Range(0f,20f)] public float Force = 1f;
        [Range(0f,10f)] public float Speed = 1;
        [Range(0f,90f)] public float Amplitude = 1;
        public int Health = 1;
    }
}
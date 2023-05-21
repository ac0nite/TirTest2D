using System;
using Gameplay.Enemy.Settings;
using UnityEngine;

namespace Gameplay.Settings
{
    [CreateAssetMenu(fileName = "_LevelBalance", menuName = "LevelBalance")]
    public class LevelBalanceSO : ScriptableObject
    {
        public int LevelTime;
        public float SpawnTime;
        public BirdCounter[] BirdCounters;
    }

    [Serializable]
    public class BirdCounter
    {
        public EnemyType Type;
        public int Count;
    }
}
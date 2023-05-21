using System;
using Common;
using Gameplay.Settings;
using UnityEngine;

namespace Gameplay.Enemy
{
    public interface IGeneratorRandomPoint
    {
        RandomPointAndDirectionGenerator.RandomData GetRandomData();
    }
    
    public class GeneratorRandomSpawnPoint : IGeneratorRandomPoint
    {
        private RandomPointAndDirectionGenerator _generator;

        public GeneratorRandomSpawnPoint(
            GameplaySettings settings,
            Camera camera)
        {
            var s = settings.GeneratorSpawnPointSettings;
            _generator = new RandomPointAndDirectionGenerator(camera, s.OffsetViewportX,s.OffsetViewportY, s.DirRange);
        }

        public RandomPointAndDirectionGenerator.RandomData GetRandomData()
        {
            var r = _generator.Random();
            _generator.DebugDraw(r);
            return r;
        }
        
        [Serializable]
        public class Settings
        {
            [Range(0, 1)] public float OffsetViewportX = 0.15f;
            [Range(0, 1)] public float OffsetViewportY = 0.15f;
            [Range(0, 1)] public float DirRange = 0.7f;
        }
    }
}
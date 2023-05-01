using System;
using Gameplay;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public interface ISpawner
    {
        
    }
    public class SpawnerBird : MonoBehaviour, ISpawner
    {
        private RandomPointAndDirectionGenerator _generator;

        private void Awake()
        {
            _generator = new RandomPointAndDirectionGenerator(0.15f,0.15f, .7f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _generator.DebugDraw();
            }
        }
    }
}
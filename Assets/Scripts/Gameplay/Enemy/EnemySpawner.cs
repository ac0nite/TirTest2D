using System;
using System.Collections.Generic;
using Gameplay.Enemy.BirdItem;
using Gameplay.Enemy.Settings;
using UnityEngine;

namespace Gameplay.Enemy
{
    public interface IEnemySpawner
    {
        Bird Spawn(EnemyParam param, EnemyResource resource);
        void DeSpawn(Bird bird);
        void AllDeSpawn();
        int Counter { get; }
        event Action<EnemyType> OnTargetHit;
    }
    
    public class EnemySpawner : IEnemySpawner
    {
        private readonly Bird.Pool _birdPool;
        private List<Bird> _enemyCollection;
        private Bird _bird;
        public int Counter => _enemyCollection.Count;
        public event Action<EnemyType> OnTargetHit;

        public EnemySpawner(Bird.Pool birdPool)
        {
            _birdPool = birdPool;
            _enemyCollection = new List<Bird>();
        }
        
        public Bird Spawn(EnemyParam param, EnemyResource resource)
        {
            _bird = _birdPool.Spawn(param, resource);
            _bird.OnDeath += OnDeathHandler;
            _enemyCollection.Add(_bird);
            return _bird;
        }

        private void OnDeathHandler(EnemyType type, Bird bird)
        {
            _enemyCollection.Remove(bird);
            OnTargetHit?.Invoke(type);
            DeSpawn(bird);
        }

        public void DeSpawn(Bird bird)
        {
            bird.OnDeath -= OnDeathHandler;
            _birdPool.Despawn(bird);
        }

        public void AllDeSpawn()
        {
            _enemyCollection.ForEach(b=> DeSpawn(b));
            _enemyCollection.Clear();
        }
    }
}
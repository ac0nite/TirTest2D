using System;
using UnityEngine;

namespace Gameplay.Bullets
{
    public class BombSpawner : IBulletSpawner
    {
        private readonly Bomb.Pool _poll;

        public BombSpawner(Bomb.Pool poll)
        {
            _poll = poll;
        }
        
        public Bullet Spawn(Vector2 position, BulletParam param)
        {
            return _poll.Spawn(position, param);
        }

        public void DeSpawn(Bullet bullet)
        {
            _poll.Despawn(bullet);
        }
    }
}
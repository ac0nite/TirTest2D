using System;
using Gameplay.Bullets.Settings;
using UnityEngine;

namespace Gameplay.Bullets
{
    public class BombSpawner : IBulletSpawner
    {
        private readonly Bomb.Pool _poll;
        private Bullet _bullet;

        public BombSpawner(Bomb.Pool poll)
        {
            _poll = poll;
        }
        
        public Bullet Spawn(Vector2 position, BulletParam param)
        {
            _bullet = _poll.Spawn(position, param);
            return _bullet;
        }

        public void DeSpawn(Bullet bullet)
        {
            _poll.Despawn(bullet);
        }
    }
}
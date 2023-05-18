using System;
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
            _bullet.RunLifeTimerIsOver(DeSpawn);
            return _bullet;
        }

        public void DeSpawn(Bullet bullet)
        {
            _poll.Despawn(bullet);
        }
    }
}
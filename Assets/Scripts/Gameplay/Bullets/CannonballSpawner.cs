using UnityEngine;

namespace Gameplay.Bullets
{
    public class CannonballSpawner : IBulletSpawner
    {
        private readonly Cannonball.Pool _pool;
        private Bullet _bullet;

        public CannonballSpawner(Cannonball.Pool pool)
        {
            _pool = pool;
        }
        public Bullet Spawn(Vector2 position, BulletParam param)
        {
            _bullet = _pool.Spawn(position, param);
            _bullet.RunLifeTimerIsOver(DeSpawn);
            return _bullet;
        }

        public void DeSpawn(Bullet bullet)
        {
            _pool.Despawn(bullet);
        }
    }
}
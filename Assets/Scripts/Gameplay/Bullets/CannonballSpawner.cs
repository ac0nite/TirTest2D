using UnityEngine;

namespace Gameplay.Bullets
{
    public class CannonballSpawner : IBulletSpawner
    {
        private readonly Cannonball.Pool _pool;

        public CannonballSpawner(Cannonball.Pool pool)
        {
            _pool = pool;
        }
        public Bullet Spawn(Vector2 position, BulletParam param)
        {
            return _pool.Spawn(position, param);
        }

        public void DeSpawn(Bullet bullet)
        {
            _pool.Despawn(bullet);
        }
    }
}
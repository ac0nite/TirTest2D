
using UnityEngine;

namespace Gameplay.Bullets
{
    public interface IBulletSpawner
    {
        Bullet Spawn(Vector2 position, BulletParam param);
        void DeSpawn(Bullet bullet);
    }
}
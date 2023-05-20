using UnityEngine;

namespace Gameplay.Bullets
{
    public class Bomb : Bullet
    {
        public override void Shot(Vector2 direction)
        {
            base.Shot(direction);
        }

        public class Pool : BasePool
        {
        }
    }
}
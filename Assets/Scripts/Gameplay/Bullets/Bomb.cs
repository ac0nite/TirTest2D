using UnityEngine;

namespace Gameplay.Bullets
{
    public class Bomb : Bullet
    {
        public override void Shot(Vector2 direction)
        {
            _rigidbody.AddForce(direction, ForceMode2D.Impulse);
        }
        
        public class Pool : BasePool
        {
        }
    }
}
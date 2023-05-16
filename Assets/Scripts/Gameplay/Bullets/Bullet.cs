using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _rigidbody;

        public event Action<Bullet, Collision2D> EventCollision;

        protected void Initialize(Vector2 position, BulletParam param)
        {
            transform.position = position;
            //initialize: texture, size etc.
        }

        private void Reset()
        {
            _rigidbody.velocity = Vector2.zero;
            transform.position = Vector3.zero;
        }
        
        public class BasePool : MonoMemoryPool<Vector2, BulletParam, Bullet>
        {
            protected override void Reinitialize(Vector2 position, BulletParam param, Bullet item)
            {
                item.Initialize(position, param);
                TryToLifeTimeIsOver(item, param.LifeTimeIsOver);
            }

            protected override void OnDespawned(Bullet item)
            {
                item.Reset();
                base.OnDespawned(item);
            }

            private void TryToLifeTimeIsOver(Bullet item, int lifeTime)
            {
                new CustomDoTweenTimer(lifeTime).Run(() =>
                {
                    if (item.isActiveAndEnabled) 
                        OnDespawned(item); 
                });
            }
        }

        public virtual void Shot(Vector2 direction)
        { }
    }
}
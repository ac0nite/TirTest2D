using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _rigidbody;

        public event Action<Bullet, Collision2D> EventCollision;
        protected BulletParam _param;

        protected void Initialize(Vector2 position, BulletParam param)
        {
            transform.position = position;
            _param = param;
            //initialize: texture, size etc.
        }

        private void Reset()
        {
            _rigidbody.velocity = Vector2.zero;
            transform.position = Vector3.zero;
        }

        protected internal Bullet RunLifeTimerIsOver(Action<Bullet> onTimer)
        {
            new CustomDoTweenTimer(_param.LifeTimeIsOver).Run(() => onTimer?.Invoke(this));
            return this;
        }
        
        public class BasePool : MonoMemoryPool<Vector2, BulletParam, Bullet>
        {
            protected override void Reinitialize(Vector2 position, BulletParam param, Bullet item)
            {
                item.Initialize(position, param);
                base.Reinitialize(position, param, item);
            }

            protected override void OnDespawned(Bullet item)
            {
                item.Reset();
                base.OnDespawned(item);
            }
        }

        public virtual void Shot(Vector2 direction)
        {
            _rigidbody.AddForce(direction * _param.ShotPower, ForceMode2D.Impulse);
        }
    }
}
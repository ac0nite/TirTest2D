using System;
using Gameplay.Bullets.Settings;
using Gameplay.Enemy.BirdItem;
using UnityEngine;
using Zenject;

namespace Gameplay.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _rigidbody;
        protected BulletParam _param;
        private CustomDoTweenTimer _lifeTimer;

        public event Action<Bullet> OnDestroy;
        
        public virtual void Shot(Vector2 direction)
        {
            _rigidbody.AddForce(direction * _param.ShotPower, ForceMode2D.Impulse);
            // Debug.Log($"[SHOT] {direction} * {_param.ShotPower}");
        }

        // private void OnTriggerEnter2D(Collider2D other)
        // {
        //     OnTrigger(other.GetComponent<IBird>());
        // }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnDestroy?.Invoke(this);
            
            if (other.collider.TryGetComponent(out Bird bird))
            {
                OnTrigger(bird);
            }
        }

        protected virtual void OnTrigger(IBird bird)
        {
            bird.ApplyDamage(_param.Damage);
        }

        private void Initialize(Vector2 position, BulletParam param)
        {
            transform.position = position;
            _param = param;
            _lifeTimer = new CustomDoTweenTimer(_param.LifeTimeIsOver).Run(() => OnDestroy?.Invoke(this));
        }
        private void Reset()
        {
            _rigidbody.velocity = Vector2.zero;
            transform.position = Vector3.zero;
            _lifeTimer.Dispose();
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
    }
}
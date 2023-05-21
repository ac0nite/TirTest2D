using Common;
using DG.Tweening;
using Gameplay.Enemy.Settings;
using MyBox;
using UnityEngine;

namespace Gameplay.Enemy.BirdItem
{
    public interface IMovement
    {
        Vector2 Velocity { get; set; }
        void Initialise(EnemyParam param, RandomPointAndDirectionGenerator.RandomData randomData);
    }
    public class Movement : MonoBehaviour, IMovement
    {
        [SerializeField] public Rigidbody2D _rigidbody;

        [SerializeField] private Vector2 _direction = Vector2.zero;
        [Range(0f, 20f), SerializeField] private float _force = 1f;
        [Range(0f,10f), SerializeField] private float _speed = 1;
        [Range(0f,90f), SerializeField] private float _amplitude = 1;

        private Vector2 _defaultVelocity = Vector3.zero;
        private int _sign = 1;
        private float _t;

        public Vector2 Velocity
        {
            get => _rigidbody.velocity;
            set
            {
                _t = 0f;
                _rigidbody.velocity = value.normalized * _force;
                _defaultVelocity = _rigidbody.velocity;
                _sign = RandomSign;
            }
        }
        
        public void Initialise(EnemyParam param, RandomPointAndDirectionGenerator.RandomData randomData)
        {
            _force = param.Force;
            _speed = param.Speed;
            _amplitude = 0;

            transform.position = randomData.Point;
            
            _direction = randomData.Direction;
            Velocity = _direction * _force;

            gameObject.SetLayerRecursively("Default");
            DOVirtual.DelayedCall(1, () =>
            {
                gameObject.SetLayerRecursively("Enemy");
                _amplitude = param.Amplitude;
            });
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _defaultVelocity.Rotate(DegreeWithFixedUpdate);
            
            // Draw(_rigidbody.position, _defaultVelocity, Color.red);
            // Draw(_rigidbody.position, _rigidbody.velocity, Color.green);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var normal = (-collision.contacts[0].normal).Rotate(UnityEngine.Random.Range(40, 50));
            var reflect = Vector3.Reflect(_rigidbody.velocity, normal);
            
            // Draw(_rigidbody.position, reflect, Color.blue);
            // Draw(_rigidbody.position, normal, Color.magenta);
            
            Velocity = reflect;
        }
        
        private float DegreeWithFixedUpdate => _sign * _amplitude * Mathf.Sin(_t += Time.fixedDeltaTime * _speed);
        private int RandomSign => UnityEngine.Random.value < .5 ? 1 : -1;
        
        private void Draw(Vector3 original, Vector3 direction, Color color)
        {
            var ray = new Ray(original, direction);
            Debug.DrawLine(ray.origin, ray.GetPoint(.8f), color, .1f);
            Debug.DrawLine(ray.GetPoint(.8f), ray.GetPoint(.85f), Color.cyan, .1f);
        }
    }
}
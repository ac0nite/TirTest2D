using Common;
using Gameplay.Enemy;
using Gameplay.Enemy.BirdItem;
using Gameplay.Enemy.Settings;
using UnityEngine;

namespace Gameplay
{
    public class MovementV2 : MonoBehaviour, IMovement
    {
        [SerializeField] public Rigidbody2D _rigidbody;
        
        [SerializeField] private Collider2D _collider;
        [SerializeField] private EdgeCollider2D _edgeCollder;
        
        private Vector3 _point0;
        private Vector3 _point1;
        private float _t;
        
        [SerializeField] private Vector2 _direction = Vector2.zero;
        [Range(0f, 20f), SerializeField] private float _force = 1f;
        [Range(0f,10f), SerializeField] private float _speed = 1;
        [Range(0f,90f), SerializeField] private float _amplitude = 1;

        private Vector2 _defaultVelocity = Vector3.zero;
        private int _sign = 1;
        
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
            throw new System.NotImplementedException();
        }

        private void Start()
        {
            Velocity = _direction * _force;
        }

        private void Draw(Vector3 original, Vector3 direction, Color color)
        {
            var ray = new Ray(original, direction);
            Debug.DrawLine(ray.origin, ray.GetPoint(.8f), color, .1f);
            Debug.DrawLine(ray.GetPoint(.8f), ray.GetPoint(.85f), Color.cyan, .1f);
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _defaultVelocity.Rotate(DegreeWithFixedUpdate);
            
            Draw(_rigidbody.position, _defaultVelocity, Color.red);
            Draw(_rigidbody.position, _rigidbody.velocity, Color.green);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var normal = (-collision.contacts[0].normal).Rotate(45);
            var reflect = Vector3.Reflect(_rigidbody.velocity, normal);
            
            Draw(_rigidbody.position, reflect, Color.blue);
            Draw(_rigidbody.position, normal, Color.magenta);
            
            Velocity = reflect;
        }

        private float DegreeWithUpdate => _sign * _amplitude * Mathf.Sin(_t += Time.deltaTime * _speed);
        private float DegreeWithFixedUpdate => _sign * _amplitude * Mathf.Sin(_t += Time.fixedDeltaTime * _speed);
        private float DegreeWithLateUpdate => _sign * _amplitude * Mathf.Sin(_t += Time.fixedDeltaTime * _speed);

        Vector2 GetClosestPoint(Vector2 position)
        {
            Vector2[] points = _edgeCollder.points;
            float shortestDistance = Vector2.Distance(position,points[0]);
            Vector2 closestPoint = points[0];
            foreach(Vector2 point in points)
            {
                if(Vector2.Distance(position,point) < shortestDistance)
                {
                    shortestDistance = Vector2.Distance(position,point);
                    closestPoint = point;
                }
            }
            return closestPoint;
        }
        private int RandomSign => UnityEngine.Random.value < .5 ? 1 : -1;
        //private int RandomSign => (int) ((UnityEngine.Random.Range(0,2) - 0.5) * 2);
    }
}
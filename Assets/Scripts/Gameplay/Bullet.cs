using UnityEngine;

namespace Gameplay
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void Stop()
        {
            _rigidbody.velocity = Vector2.zero;
        }

        public void Shot(Vector2 direction)
        {
            _rigidbody.AddForce(direction, ForceMode2D.Impulse);
        }

        // private void FixedUpdate()
        // {
        //     var point = _camera.WorldToViewportPoint(transform.position);
        //     Debug.Log(point);
        // }
    }
}
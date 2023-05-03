using Application.UI;
using Application.UI.Common;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class ForceTest : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _target;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _force = 1f;
        [SerializeField] private Bullet _bullet;

        private Bullet[] pool = new Bullet[10];
        private int index = 0;

        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                pool[i] = Instantiate(_bullet, Vector2.zero, Quaternion.identity);
                pool[i].gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                ApplyForce(Input.mousePosition);
                MuzzleTurning(Input.mousePosition);
                OneShot(Input.mousePosition);
            }
        }

        private void OneShot(Vector3 mousePosition)
        {
            Vector2 pos = _camera.ScreenToWorldPoint(mousePosition);
            var dir = _target.position - pos;
            //
            Debug.DrawLine(pos, _target.position, Color.red, .5f);
            //
            var p = _muzzle.position;
            p.y += 2f;
            //var bullet = Instantiate(_bullet, p, Quaternion.identity);
            //_bullet.Shot(-dir.normalized);

            var b = pool[index % 10];
            b.Stop();
            b.transform.position = _muzzle.position;
            b.gameObject.SetActive(true);
            b.Shot(-dir.normalized * .9f);
            index++;
        }

        private void MuzzleTurning(Vector3 mousePosition)
        {
            _muzzle.rotation = LookAt2D(mousePosition);
        }
        
        private Quaternion LookAt2D(Vector3 mousePosition)
        {
            Vector3 target = _camera.ScreenToWorldPoint(mousePosition);
            Vector3 direction = target - _muzzle.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void ApplyForce(Vector3 mousePosition)
        {
            Vector2 pos = _camera.ScreenToWorldPoint(mousePosition);
            var dir = _target.position - pos;
            _target.AddForce(dir * _force, ForceMode2D.Impulse);
        }
    }
}
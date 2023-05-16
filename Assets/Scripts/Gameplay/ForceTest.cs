using System;
using Gameplay.Bullets;
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
        
        private IInputService _input;
        private IBulletSpawner _bulletSpawner;
        private IWeaponModelSetter _weaponModelSetter;
        private IBulletFacade _bulletFacade;

        [Inject]
        public void Construct(IInputService inputService, IWeaponModelSetter weaponModelSetter, IBulletFacade bulletFacade)
        {
            _weaponModelSetter = weaponModelSetter;
            _bulletFacade = bulletFacade;
            
            _input = inputService;
            _input.EventTouchPosition += OnTouch;
        }

        private void Start()
        {
            _weaponModelSetter.Level = 1;
            _weaponModelSetter.BulletType = BulletType.BOMB;
        }

        private void OnTouch(Vector2 position)
        {
            ApplyForce(position);
            MuzzleTurning(position);
            OneShot(position);
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

            var bullet = _bulletFacade.Spawn(_muzzle.position);
            bullet.Shot(-dir.normalized * .9f);
            
            //var bullet = Instantiate(_bullet, p, Quaternion.identity);
            //_bullet.Shot(-dir.normalized);

            //_bulletSpawner.Spawn(_muzzle.position, -dir.normalized * .9f);

            // var b = _bulletSpawner.Spawn<Bomb>(_muzzle.position);
            // b.Shot(-dir.normalized * .9f);
            //
            // b.EventCollision += (bullet, collision2D) =>
            // {
            //     _bulletSpawner.DeSpawn<Bomb>(bullet);
            // };

            // var b = pool[index % 10];
            // b.Stop();
            // b.transform.position = _muzzle.position;
            // b.gameObject.SetActive(true);
            // b.Shot(-dir.normalized * .9f);
            // index++;
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
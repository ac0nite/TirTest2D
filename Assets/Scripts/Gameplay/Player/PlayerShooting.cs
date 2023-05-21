using Gameplay.Bullets;
using Gameplay.Input;
using Gameplay.Settings;
using UnityEngine;

namespace Gameplay.Player
{
    public interface IPlayerShooting
    {
        void Initialise(IPLayer player);
        void SetActive(bool active);
    }

    public class PlayerShooting : IPlayerShooting
    {
        private Vector3 _positionSpawn;
        
        private readonly Camera _camera;
        private readonly IBulletFacade _bulletFacade;
        private readonly IInputService _input;
        
        private IPLayer _player;
        private Vector3 _worldPosition;
        private readonly Cannon.Settings _cannonSettings;
        private Ray _directionShotRay;
        private CustomDoTweenTimer _timer;
        private Vector2 _screenPosition;
        private Vector2 _direction;

        public PlayerShooting(
            Camera camera,
            IInputService inputService, 
            IBulletFacade bulletFacade,
            GameplaySettings gameplaySettings)
        {
            _camera = camera;
            _bulletFacade = bulletFacade;
            _input = inputService;
            _cannonSettings = gameplaySettings.CannonSettings;
        }
        
        public void Initialise(IPLayer player)
        {
            _player = player;
            _timer = new CustomDoTweenTimer(_bulletFacade.CurrentBulletParam.ShotTimer);
        }

        public void SetActive(bool active)
        {
            if (active)
            {
                _timer.RunLoop(TryToShot);
            }
            else
            {
                _timer.Dispose();
            }
        }

        private void TryToShot()
        {
            if (CanShot)
            {
                Shot();
            }
        }
        
        private bool CanShot => _input.GetPressTouch(out _screenPosition);

        private void Shot()
        {
            //_worldPosition = WorldPosition(_screenPosition);
            //_worldPosition = DirectionNormalized(_screenPosition);
            
            _direction = DirectionNormalized(_screenPosition);
            OneShot(_direction);
            ApplyForce(_direction);
        }

        private Vector3 WorldPosition(Vector2 mousePosition)
        {
            return _camera.ScreenToWorldPoint(mousePosition);
        }
        
        private Vector2 DirectionNormalized(Vector2 mousePosition)
        {
            Vector2 direction = _camera.ScreenToWorldPoint(mousePosition) - _player.Transform.position;
            return (direction * 10f).normalized;
        }

        private void OneShot(Vector2 direction)
        {
            // var direction = (worldPosition - _player.Transform.position).normalized;
            //
            // direction = ((Vector2)direction * 10).normalized;
            //
            // Debug.Log($"[SHOT] [DIR] {direction}");

            //Debug.DrawLine(worldPosition, _player.Transform.position, Color.red, .5f);

            _directionShotRay = new Ray(_player.MuzzleTransform.position, direction);
            
            _positionSpawn = _directionShotRay.GetPoint(_cannonSettings.MuzzleSpawnBulletOffsetY);
            _bulletFacade.Spawn(_positionSpawn).Shot(direction);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_positionSpawn, .1f);
        }

        private void MuzzleTurning(Vector3 worldPosition)
        {
            _player.MuzzleTransform.rotation = LookAt2D(worldPosition);
        }
        
        private Quaternion LookAt2D(Vector3 worldPosition)
        {
            // Vector3 target = _camera.ScreenToWorldPoint(mousePosition);
            Vector3 direction = worldPosition - _player.Transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void ApplyForce(Vector2 direction)
        {
            // Vector2 pos = _camera.ScreenToWorldPoint(mousePosition);
            //var dir = _player.Transform.position - position;
            _player.Rigidbody.AddForce(-direction * _cannonSettings.PowerMovementImpulse, ForceMode2D.Impulse);
        }
    }
}
using MyBox;
using UnityEngine;

namespace Gameplay
{
    public interface IGameplayBackground
    {
        void ScreenFill(Vector2 offset);
    }
    
    public class GameplayBackground : IGameplayBackground
    {
        private readonly EnvironmentKeeper _environmentKeeper;
        private readonly Camera _camera;

        public GameplayBackground(
            EnvironmentKeeper environmentKeeper,
            Camera camera)
        {
            _environmentKeeper = environmentKeeper;
            _camera = camera;
        }
        
        public void ScreenFill(Vector2 offset)
        {
            var spriteRenderer = _environmentKeeper.BackgroundSpriteRenderer;

            float cameraHeight = _camera.orthographicSize * 2;
            Vector2 cameraSize = new Vector2(_camera.aspect * cameraHeight, cameraHeight);
            Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

            Vector2 scale = spriteRenderer.transform.localScale;
            scale *= cameraSize.x > cameraSize.y ? (cameraSize.x / spriteSize.x) : (cameraSize.y / spriteSize.y);

            spriteRenderer.transform.position = spriteRenderer.transform.position.SetXY(offset.x, offset.y);
            spriteRenderer.transform.localScale = scale;
        }
    }
}
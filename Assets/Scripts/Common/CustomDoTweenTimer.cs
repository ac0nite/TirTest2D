using DG.Tweening;

namespace Gameplay.Bullets
{
    public class CustomDoTweenTimer
    {
        private readonly float _duration;
        private Tween _tween;

        public CustomDoTweenTimer(float duration)
        {
            _duration = duration;
        }
        
        public CustomDoTweenTimer Run(TweenCallback callback)
        {
            _tween = DOVirtual.DelayedCall(_duration, callback, false);
            return this;
        }

        public CustomDoTweenTimer RunLoop(TweenCallback callback)
        {
            _tween = DOVirtual
                .DelayedCall(_duration, callback, false)
                .SetLoops(-1, LoopType.Incremental)
                .SetRelative(true)
                .OnComplete(callback);
            
            return this;
        }

        public CustomDoTweenTimer Stop()
        {
            Dispose();
            return this;
        }

        public void Dispose()
        {
            if (_tween.IsPlaying())
                _tween.Kill();
        }
    }
}
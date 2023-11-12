namespace TrexGame.Graphics
{
    public class AnimatedSpriteFrame
    {
        private readonly AnimatedSpriteFrame _previousFrame;
        private AnimatedSpriteFrame _nextFrame;

        public Sprite Sprite { get; set; }

        public float Duration { get; }

        public float TimeStamp
        {
            get
            {
                return _previousFrame == null ? 0 : _previousFrame.TimeStamp + _previousFrame.Duration;
            }
        }

        public bool IsFirstFrame
        {
            get => _previousFrame == null;
        }

        public bool IsLastFrame
        {
            get => _nextFrame == null;
        }

        public AnimatedSpriteFrame(Sprite sprite, float duration, AnimatedSpriteFrame previousFrame = null)
        {
            Sprite = sprite;
            Duration = duration;
            _previousFrame = previousFrame;
        }

        public void SetNextFrame(AnimatedSpriteFrame nextFrame)
        {
            _nextFrame = nextFrame;
        }
    }
}

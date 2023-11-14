using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrexGame.Graphics
{
    public class AnimatedSprite
    {
        private readonly List<AnimatedSpriteFrame> _frames = new();

        public bool IsPlaying { get; private set; } = false;

        public float Progress { get; private set; } = 0f;

        public bool IsInfinite { get; private set; }

        public AnimatedSpriteFrame this[int index]
        {
            get => GetFrame(index);
        }

        public AnimatedSpriteFrame CurrentFrame
        {
            get
            {
                return _frames
                    .Where(f => f.TimeStamp <= Progress)
                    .OrderBy(f => f.TimeStamp)
                    .LastOrDefault();
            }
        }

        public float Duration
        {
            get => _frames.Sum(f => f.Duration);
        }

        public AnimatedSprite(bool infinite)
        {
            IsInfinite = infinite;
        }

        public AnimatedSprite(bool infinite, List<AnimatedSpriteFrame> frames) : this(infinite)
        {
            frames.ForEach(f => AddFrame(f.Sprite, f.Duration));
        }

        private AnimatedSpriteFrame GetFrame(int index)
        {
            if (_frames.Count == 0)
            {
                throw new KeyNotFoundException("The Animation contains no frames to select from.");
            }
            else if (index < 0 || index >= _frames.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"No frame at this index. Available frames: {_frames.Count}, index: {index}.");
            }
            else
            {
                return _frames[index];
            }
        }

        public void ChangeFrameDuration(int index, float newDuration)
        {
            if (newDuration <= 0)
                return;
            AnimatedSpriteFrame frame = GetFrame(index);
            frame.Duration = newDuration;
        }

        public void AddFrame(Sprite sprite, float duration)
        {
            AnimatedSpriteFrame prevFrame = _frames.Count == 0 ? null : _frames.Where(f => f.IsLastFrame).FirstOrDefault();
            AnimatedSpriteFrame newFrame = new(sprite, duration, prevFrame);
            prevFrame?.SetNextFrame(newFrame);
            _frames.Add(newFrame);
        }

        public void Update(GameTime gameTime)
        {
            if (IsPlaying)
            {
                Progress += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Progress >= Duration)
                {
                    if (IsInfinite)
                    {
                        Progress -= Duration;
                    }
                    else
                    {
                        Stop();
                    }

                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            CurrentFrame?.Sprite.Draw(spriteBatch, position);
        }

        public void Play()
        {
            IsPlaying = true;
        }

        public void Stop()
        {
            IsPlaying = false;
            Progress = 0f;
        }

        public void ChangeFrames(List<AnimatedSpriteFrame> frames, bool infinite = true)
        {
            if (IsPlaying)
                Stop();
            _frames.Clear();
            frames.ForEach(f => AddFrame(f.Sprite, f.Duration));
            IsInfinite = infinite;
        }
    }
}

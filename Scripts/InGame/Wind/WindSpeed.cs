using System;
using UnityEngine;

namespace Unity1week202306.InGame.Wind
{
    /// <summary>
    /// 風速
    /// </summary>
    public readonly struct WindSpeed : IEquatable<WindSpeed>
    {
        public static WindSpeed Zero => new(Vector2.zero, 0);

        public float Speed { get; }
        
        public Vector2 Direction { get; }
        
        public Vector2 Value => Direction * Speed;

        public WindSpeed(Vector2 direction, float speed)
        {
            if (speed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(speed), "風速は0以上である必要があります。");
            }

            Direction = direction.normalized;
            Speed = speed;
        }

        public bool Equals(WindSpeed other)
        {
            return Speed.Equals(other.Speed) && Direction.Equals(other.Direction);
        }

        public override bool Equals(object obj)
        {
            return obj is WindSpeed other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Speed, Direction);
        }
    }
}
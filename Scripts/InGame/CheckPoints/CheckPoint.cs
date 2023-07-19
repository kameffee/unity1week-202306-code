using System;
using UnityEngine;

namespace Unity1week202306.InGame.CheckPoints
{
    public class CheckPoint : IEquatable<CheckPoint>
    {
        public CheckPointIdentifier Id { get; }
        public Vector3 WorldPosition { get; }

        public CheckPoint(CheckPointIdentifier id, Vector3 worldPosition)
        {
            Id = id;
            WorldPosition = worldPosition;
        }
        
        public bool Equals(CheckPoint other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            return obj is CheckPoint other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
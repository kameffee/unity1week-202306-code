using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity1week202306.InGame.CheckPoints
{
    public class FieldCheckPointRepository
    {
        private readonly List<CheckPointObject> _checkPoints = new();

        public CheckPoint Get(CheckPointIdentifier id)
        {
            var checkPointObject = _checkPoints.FirstOrDefault(point => point.Id == id.Value);
            if (checkPointObject == null)
            {
                Debug.LogError($"idが{id}のチェックポイントがありません");
                return default;
            }

            return checkPointObject.ToStartPoint();
        }

        public void Set(params CheckPointObject[] pointObject)
        {
            _checkPoints.AddRange(pointObject);
        }

        public void Clear()
        {
            _checkPoints.Clear();
        }
    }
}
using UnityEngine;

namespace Unity1week202306.InGame.CheckPoints
{
    public class CheckPointService
    {
        private readonly FieldCheckPointRepository _fieldCheckPointRepository;

        private CheckPointIdentifier? _currentCheckPointId = null;

        public CheckPointService(FieldCheckPointRepository fieldCheckPointRepository)
        {
            _fieldCheckPointRepository = fieldCheckPointRepository;
        }

        public void SetCurrentCheckPoint(CheckPointIdentifier id)
        {
            Debug.Log($"SetCurrentCheckPoint id: {id.Value}");
            _currentCheckPointId = id;
        }

        public bool HasCurrentCheckPoint()
        {
            return _currentCheckPointId.HasValue;
        }

        public CheckPoint GetCurrentCheckPoint()
        {
            if (!_currentCheckPointId.HasValue)
            {
                return null;
            }

            return _fieldCheckPointRepository.Get(_currentCheckPointId.Value);
        }
    }
}
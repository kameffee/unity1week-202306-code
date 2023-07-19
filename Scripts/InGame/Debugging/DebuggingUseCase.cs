using Unity1week202306.InGame.CheckPoints;
using Unity1week202306.InGame.Players;
using VContainer;

namespace Unity1week202306.InGame.Debugging
{
    public class DebuggingUseCase
    {
        [Inject]
        private readonly PlayerController _playerController;

        [Inject]
        private readonly FieldCheckPointRepository _fieldCheckPointRepository;

        [Inject]
        private readonly CheckPointService _checkPointService;

        public void PlayerTeleport(CheckPointIdentifier id)
        {
            var checkPoint = _fieldCheckPointRepository.Get(id);
            _playerController.SetPosition(checkPoint.WorldPosition);
        }

        public void TeleportToNextCheckPoint()
        {
            var currentCheckPoint = _checkPointService.GetCurrentCheckPoint();
            var nextCheckPointId = currentCheckPoint == null
                ? new CheckPointIdentifier(0)
                : currentCheckPoint.Id.Next();

            var nextCheckPoint = _fieldCheckPointRepository.Get(nextCheckPointId);
            _playerController.SetPosition(nextCheckPoint.WorldPosition);
        }
    }
}
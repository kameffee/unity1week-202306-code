using Unity1week202306.InGame.CheckPoints;
using Unity1week202306.InGame.Players;

namespace Unity1week202306.InGame
{
    public class RespawnUseCase
    {
        private readonly CheckPointService _checkPointService;
        private readonly PlayerController _playerController;

        public RespawnUseCase(
            CheckPointService checkPointService,
            PlayerController playerController)
        {
            _checkPointService = checkPointService;
            _playerController = playerController;
        }
        
        public void Respawn()
        {
            var currentCheckPoint = _checkPointService.GetCurrentCheckPoint();
            // プレイヤーの位置をチェックポイントの位置にする
            // ここでプレイヤーの位置を変更する
            _playerController.SetPosition(currentCheckPoint.WorldPosition);
        }
    }
}
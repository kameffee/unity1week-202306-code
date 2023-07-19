namespace Unity1week202306.InGame.CheckPoints
{
    public class SetCheckPointUseCase
    {
        private readonly CheckPointService _checkPointService;

        public SetCheckPointUseCase(CheckPointService checkPointService)
        {
            _checkPointService = checkPointService;
        }

        public void Execute(CheckPointIdentifier id)
        {
            // まだチェックポイントがセットされていない場合は, 即時セットする
            if (!_checkPointService.HasCurrentCheckPoint())
            {
                _checkPointService.SetCurrentCheckPoint(id);
                return;
            }

            // ここで現在のチェックポイントより前のチェックポイントには戻れないようにする
            var currentCheckPoint = _checkPointService.GetCurrentCheckPoint();
            if (id.Value <= currentCheckPoint.Id.Value)
            {
                return;
            }

            // チェックポイントをセットする
            _checkPointService.SetCurrentCheckPoint(id);
        }
    }
}
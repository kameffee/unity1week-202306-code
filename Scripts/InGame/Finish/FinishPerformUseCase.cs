using System.Threading;
using Cysharp.Threading.Tasks;

namespace Unity1week202306.InGame.Finish
{
    /// <summary>
    /// ゲームのクリア演出
    /// </summary>
    public class FinishPerformUseCase
    {
        private readonly FinishSequencePresenter _finishSequencePresenter;

        public FinishPerformUseCase(FinishSequencePresenter finishSequencePresenter)
        {
            _finishSequencePresenter = finishSequencePresenter;
        }

        public async UniTask PerformAsync(CancellationToken cancellationToken)
        {
            // クリア演出
            await _finishSequencePresenter.Play(cancellationToken);
        }
    }
}
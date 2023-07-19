using System.Threading;
using Cysharp.Threading.Tasks;

namespace Unity1week202306.LetterBox
{
    public class LetterBoxUseService
    {
        private readonly LetterBoxView _letterBoxView;
        
        public LetterBoxUseService(LetterBoxView letterBoxView)
        {
            _letterBoxView = letterBoxView;
        }
        
        public async UniTask ShowAsync(float? duration = null, CancellationToken cancellationToken = default)
        {
            await _letterBoxView.ShowAsync(duration, cancellationToken);
        }
        
        public async UniTask HideAsync(float? duration = null, CancellationToken cancellationToken = default)
        {
            await _letterBoxView.HideAsync(duration, cancellationToken);
        }
    }
}
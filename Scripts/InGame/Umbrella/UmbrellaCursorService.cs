using Cysharp.Threading.Tasks;

namespace Unity1week202306.InGame.Umbrella
{
    public class UmbrellaCursorService
    {
        private readonly UmbrellaCursorView _cursorView;

        public UmbrellaCursorService(UmbrellaCursorView cursorView)
        {
            _cursorView = cursorView;
        }

        public async UniTask ShowAsync(float? duration = null)
        {
            await _cursorView.Show(duration);
        }

        public async UniTask HideAsync(float? duration = null)
        {
            await _cursorView.Hide(duration);
        }
    }
}
using UniRx;

namespace Unity1week202306.InGame.Wind
{
    public class WindSituation
    {
        public IReadOnlyReactiveProperty<WindSpeed> WindSpeed => _windSpeed;

        private readonly ReactiveProperty<WindSpeed> _windSpeed = new(Wind.WindSpeed.Zero);

        public WindSituation()
        {
        }

        public void SetWindSpeed(WindSpeed speed)
        {
            _windSpeed.Value = speed;
        }
    }
}
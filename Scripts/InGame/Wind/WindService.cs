
using UnityEngine;

namespace Unity1week202306.InGame.Wind
{
    /// <summary>
    /// 風向きの変更
    /// </summary>
    public class WindService
    {
        private readonly WindSituation _windSituation;

        public WindService(WindSituation windSituation)
        {
            _windSituation = windSituation;
        }

        public void Change(Vector2 direction, float speed)
        {
            var windSpeed = new WindSpeed(direction, speed);
            _windSituation.SetWindSpeed(windSpeed);
        }
    }
}
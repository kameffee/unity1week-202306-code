using UnityEngine;

namespace Unity1week202306.InGame.Wind
{
    public class ChangeWindUseCase
    {
        private readonly WindService _windService;

        public ChangeWindUseCase(WindService windService)
        {
            _windService = windService;
        }

        /// <summary>
        /// ランダムで風向きを変更する
        /// </summary>
        public void ChangeRandom()
        {
            var direction = new Vector2(Random.Range(-1f, 1f) * 5f, 0f);
            var windSpeed = Random.Range(10f, 10f) * 2;
            _windService.Change(direction, windSpeed);
        }
    }
}
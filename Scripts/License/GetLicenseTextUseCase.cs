using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Unity1week202306.License
{
    public class GetLicenseTextUseCase
    {
        public async UniTask<string> Get()
        {
            var textAsset = await Resources.LoadAsync<TextAsset>("License") as TextAsset;
            Assert.IsNotNull(textAsset);

            return textAsset.text;
        }
    }
}
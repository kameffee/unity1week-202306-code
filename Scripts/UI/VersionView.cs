using TMPro;
using UnityEngine;

namespace Unity1week202306.UI
{
    public class VersionView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private string _textFormat = "v{0}";

        private void Start()
        {
            _text.text = string.Format(_textFormat, Application.version);
        }
    }
}
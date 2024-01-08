using UnityEngine;
using TMPro;

namespace GyroSpace.UI
{

    public class ShowVersion : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _text.text = $"V {Application.version}";
        }
    }
}
using UnityEngine;
using TMPro;

namespace GyroSpace.UI
{

    public class HighScorePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _place, _name, _score;

        public void SetFields(int newPlace, string newName, int newScore)
        {
            _place.text = newPlace.ToString();
            _name.text = newName;
            _score.text = newScore.ToString();
        }
    }
}
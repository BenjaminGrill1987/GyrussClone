using GyroSpace.Interface;
using GyroSpace.Utility;
using TMPro;
using UnityEngine;

namespace GyroSpace.UI
{

    public class ScoreHandler : Singleton<ScoreHandler>
    {
        private int _score;
        [SerializeField] private LeaderBoards _leaderboard;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private TMP_InputField _input;
        [SerializeField] private GameObject _endScreen;

        public static int _Score { get => _Instance._score; }
        public static LeaderBoards _Leaderboard { get => _Instance._leaderboard; }

        void Start()
        {
            _score = 0;
            _text.text = _score.ToString();
        }

        public static void AddScore(int addPoints)
        {
            _Instance._score += addPoints;
            _Instance._text.text = _Score.ToString();
        }

        public static async void GameOver()
        {
            if (await _Leaderboard.CheckIfNewHighscore(_Score))
            {
                _Instance._endScreen.SetActive(true);
            }
            else
            {
                GameState.TryToChange(Gamestates.Menu);
            }
        }

        public void BackToMenuOnClick()
        {
            if (_input.text == "")
            {
                _input.text = "DEV";
            }

            _leaderboard.AddScore(_input.text, _score);
            GameState.TryToChange(Gamestates.Menu);
        }
    }
}
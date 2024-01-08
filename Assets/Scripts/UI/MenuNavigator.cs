using GyroSpace.Interface;
using GyroSpace.Utility;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

namespace GyroSpace.UI
{

    public class MenuNavigator : MonoBehaviour
    {
        [SerializeField] LeaderBoards _leaderBoardScore;
        [SerializeField] GameObject _highScorePanel, _menuButtons, _leaderBoard;
        [SerializeField] Transform _content;

        public void StartOnClick()
        {
            GameState.TryToChange(Gamestates.Game);
        }

        public async void HighscoreOnClick()
        {
            _menuButtons.SetActive(false);
            _leaderBoard.SetActive(true);

            ShowHighScore(await _leaderBoardScore.GetLeaderBoard());
        }

        public void QuitOnClick()
        {
            GameState.TryToChange(Gamestates.Quit);
        }

        public void BackOnClick()
        {
            DeleteBoard();
            _menuButtons.SetActive(true);
            _leaderBoard.SetActive(false);
        }

        private void ShowHighScore(LeaderboardScoresPage newList)
        {

            foreach (var entry in newList.Results)
            {
                GameObject panel = Instantiate(_highScorePanel, _content);
                panel.GetComponent<HighScorePanel>().SetFields(entry.Rank + 1, entry.PlayerName, (int)entry.Score);
            }
        }

        private void DeleteBoard()
        {
            foreach (Transform child in _content)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

namespace GyroSpace.UI
{

    public class LeaderBoards : MonoBehaviour
    {
        private const string _leaderBoardID = "Gyruss";
        private string _playerID;

        async Task SignIn()
        {
            await UnityServices.InitializeAsync();

            await SignInAnonymously();
        }

        async Task SignInAnonymously()
        {
            AuthenticationService.Instance.SignedIn += () =>
            {
                _playerID = AuthenticationService.Instance.PlayerId;
                Debug.Log($"Signed in as: {AuthenticationService.Instance.PlayerId}");
            };
            AuthenticationService.Instance.SignInFailed += s =>
            {
                Debug.Log(s);
            };
            if (AuthenticationService.Instance.IsSignedIn) return;

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        public async void AddScore(string name, int newScore)
        {
            var playerName = name;
            if (playerName == "")
            {
                playerName = "Dev";
            }
            await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
            var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(_leaderBoardID, newScore);
        }

        public async Task<bool> CheckIfNull()
        {
            await SignIn();

            var leaderBoard = await LeaderboardsService.Instance.GetScoresAsync(_leaderBoardID, new GetScoresOptions { Limit = 10 });

            return leaderBoard != null;
        }

        public async Task<bool> CheckIfNewHighscore(int newScore)
        {
            await SignIn();

            var newHighScore = false;
            var leaderBoard = await LeaderboardsService.Instance.GetScoresAsync(_leaderBoardID, new GetScoresOptions { Limit = 10 });
            var playerID = false;

            foreach (var score in leaderBoard.Results)
            {
                if (score.PlayerId == _playerID)
                {
                    playerID = true;
                    break;
                }
            }

            if (playerID)
            {
                var scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(_leaderBoardID);

                if (scoreResponse.Score < newScore)
                {
                    newHighScore = true;
                }
            }
            else
            {
                newHighScore = true;
            }
            return newHighScore;
        }

        public async Task<LeaderboardScoresPage> GetLeaderBoard()
        {
            await SignIn();

            return await LeaderboardsService.Instance.GetScoresAsync(_leaderBoardID, new GetScoresOptions { Limit = 10 });
        }

        public void OnClickCancel()
        {
            gameObject.SetActive(false);
        }
    }
}
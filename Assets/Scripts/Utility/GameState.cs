using UnityEngine;
using UnityEngine.SceneManagement;
using GyroSpace.Interface;
using GyroSpace.UI;

namespace GyroSpace.Utility
{

    public class GameState : Singleton<GameState>
    {
        private Gamestates _currentState;
        public bool _testScene = false;
        public static Gamestates _CurrentState { get => _Instance._currentState; }

        public override void Awake()
        {
            base.Awake();
            if (!_testScene)
            {
                _currentState = Gamestates.Menu;
            }
            else
            {
                _currentState = Gamestates.TestScene;
            }

        }

        void Start()
        {
        }

        public static void TryToChange(Gamestates newState)
        {
            switch (newState)
            {
                case Gamestates.init:
                    {
                        if (IsGranted(newState))
                        {
                            Debug.LogWarning("Gamestate Changed from: " + _CurrentState + " to: " + newState);
                        }
                        else
                        {
                            Debug.LogError("Can't change state. Current State: " + _CurrentState + " and new state: " + newState + " are the same!");
                        }
                        break;
                    }
                case Gamestates.Menu:
                    {
                        if (IsGranted(newState))
                        {
                            Debug.LogWarning("Gamestate Changed from: " + _CurrentState + " to: " + newState);

                            SceneManager.LoadScene(0);
                        }
                        else
                        {
                            Debug.LogError("Can't change state. Current State: " + _CurrentState + " and new state: " + newState + " are the same!");
                        }
                        break;
                    }
                case Gamestates.Game:
                    {
                        if (IsGranted(newState))
                        {
                            Debug.LogWarning("Gamestate Changed from: " + _CurrentState + " to: " + newState);

                            SceneManager.LoadScene(1);
                        }
                        else
                        {
                            Debug.LogError("Can't change state. Current State: " + _CurrentState + " and new state: " + newState + " are the same!");
                        }
                        break;
                    }
                case Gamestates.GameOver:
                    {
                        if (IsGranted(newState))
                        {
                            Debug.LogWarning("Gamestate Changed from: " + _CurrentState + " to: " + newState);
                            ScoreHandler.GameOver();
                        }
                        else
                        {
                            Debug.LogError("Can't change state. Current State: " + _CurrentState + " and new state: " + newState + " are the same!");
                        }
                        break;
                    }
                case Gamestates.Pause:
                    {
                        if (IsGranted(newState))
                        {
                            Debug.LogWarning("Gamestate Changed from: " + _CurrentState + " to: " + newState);

                        }
                        else
                        {
                            Debug.LogError("Can't change state. Current State: " + _CurrentState + " and new state: " + newState + " are the same!");
                        }
                        break;
                    }
                case Gamestates.TestScene:
                    {
                        if (IsGranted(newState))
                        {
                            Debug.LogWarning("Gamestate Changed from: " + _CurrentState + " to: " + newState);
                        }
                        else
                        {
                            Debug.LogError("Can't change state. Current State: " + _CurrentState + " and new state: " + newState + " are the same!");
                        }
                        break;
                    }
                case Gamestates.Quit:
                    {
                        if (IsGranted(newState))
                        {
                            Debug.LogWarning("Gamestate Changed from: " + _CurrentState + " to: " + newState);

                            Application.Quit();
                        }
                        else
                        {
                            Debug.LogError("Can't change state. Current State: " + _CurrentState + " and new state: " + newState + " are the same!");
                        }
                        break;
                    }
            }

            if (IsGranted(newState))
            {
                _Instance._currentState = newState;
            }
        }

        private static bool IsGranted(Gamestates newState)
        {
            return _CurrentState != newState;
        }

        public static void SetTestScene(bool newBool)
        {
            _Instance._testScene = newBool;
        }
    }
}
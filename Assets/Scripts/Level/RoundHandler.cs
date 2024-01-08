using GyroSpace.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GyroSpace.Level
{

    public class RoundHandler : Singleton<RoundHandler>
    {
        [SerializeField] private int _maxRounds;
        [SerializeField] private GameObject _subraum;

        private Animator _animator;
        private List<GameObject> _amountOfShips = new();

        private int _currentRound = 0;

        public static List<GameObject> _AmountOfShips => _Instance._amountOfShips;
        public static int _CurrentRound => _Instance._currentRound;
        public static Animator _Animator => _Instance._animator;

        private void Start()
        {
            _animator = _subraum.GetComponent<Animator>();
        }

        public static void Jump()
        {
            _Instance.StartCoroutine("WaitTillAnimationEnd");
        }

        public static void AddShip(GameObject newShip)
        {
            if (!_AmountOfShips.Contains(newShip))
            {
                _Instance._amountOfShips.Add(newShip);
            }
        }

        public static void SubShip(GameObject newShip)
        {
            if (_AmountOfShips.Contains(newShip))
            {
                _Instance._amountOfShips.Remove(newShip);
            }
            if (IsReadyForJump())
            {
                Jump();
            }
        }

        public static void CountRoundUp()
        {
            _Instance._currentRound++;
        }

        public static bool IsRoundFinished()
        {
            return _CurrentRound >= _Instance._maxRounds;
        }

        public static bool NoShips()
        {
            return _AmountOfShips.Count == 0;
        }

        public static bool IsReadyForJump()
        {
            return NoShips() && IsRoundFinished();
        }

        IEnumerator WaitTillAnimationEnd()
        {
            _Instance._subraum.SetActive(true);

            yield return new WaitForSeconds(_Animator.GetCurrentAnimatorStateInfo(0).length);
            BackgroundChanger.ChangeBackGround();
            _Instance._subraum.SetActive(false);
            _currentRound = 0;
        }
    }
}
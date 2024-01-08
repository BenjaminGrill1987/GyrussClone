using GyroSpace.Level;
using UnityEngine;

namespace GyroSpace.Utility
{

    public class DebugWindow : MonoBehaviour
    {
        private void OnGUI()
        {
            GUI.TextArea(new Rect(10, 10, 150, 100),
                $"Amount of Ships: {RoundHandler._AmountOfShips.Count} " +
                $"\n" +
                $"Ready for Jump: {RoundHandler.IsReadyForJump()}");
        }
    }
}
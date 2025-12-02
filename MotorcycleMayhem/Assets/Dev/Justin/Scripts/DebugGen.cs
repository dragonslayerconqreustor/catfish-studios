using UnityEngine;

public class DebugGen : MonoBehaviour
{
    public void ResetMultiplayer()
    {
        FindAnyObjectByType<MultiplayerJoin>().ResetSystem();
    }
}

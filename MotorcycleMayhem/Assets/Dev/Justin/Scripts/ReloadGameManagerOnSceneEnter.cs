using Unity.VisualScripting;
using UnityEngine;

public class ReloadGameManagerOnSceneEnter : MonoBehaviour
{
    [SerializeField] bool CanJoinInScene;
    [SerializeField] bool FullReset;

    private void Start()
    {
        MultiplayerJoin temp = FindAnyObjectByType<MultiplayerJoin>();
        if (temp != null)
        {
            temp.Reload(CanJoinInScene);
            if (FullReset)
            {
                temp.ResetSystem();
            }
        }
        else
        {
            Debug.LogError("Scene has no MultiplayerJoin");
            Destroy(this); return;
        }
    }
}

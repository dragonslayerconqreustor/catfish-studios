using Unity.VisualScripting;
using UnityEngine;

public class ReloadGameManagerOnSceneEnter : MonoBehaviour
{
    [SerializeField] bool CanJoinInScene;
    private void Start()
    {
        MultiplayerJoin temp = FindAnyObjectByType<MultiplayerJoin>();
        if (temp != null)
        {
            FindAnyObjectByType<MultiplayerJoin>().Reload(CanJoinInScene);
        }
        else
        {
            Debug.LogError("Scene has no MultiplayerJoin");
            Destroy(this); return;
        }
    }
}

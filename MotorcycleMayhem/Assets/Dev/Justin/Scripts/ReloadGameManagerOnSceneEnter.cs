using UnityEngine;

public class ReloadGameManagerOnSceneEnter : MonoBehaviour
{
    [SerializeField] bool CanJoinInScene;
    private void Start()
    {
        FindAnyObjectByType<MultiplayerJoin>().Reload(CanJoinInScene);
    }
}

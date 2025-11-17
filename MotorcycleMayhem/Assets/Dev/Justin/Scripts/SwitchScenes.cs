using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    [SerializeField] private string scene;
    public void SwitchScene()
    {
        SceneManager.LoadScene(scene);
    }
}

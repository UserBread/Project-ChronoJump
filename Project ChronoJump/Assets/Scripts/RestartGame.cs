using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public void loadCurrentScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }
}

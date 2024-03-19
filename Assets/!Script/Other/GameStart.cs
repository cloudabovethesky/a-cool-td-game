using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public GameObject audioManager;

    public void StartLevel(int levelIndex)
    {
        audioManager.GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene(levelIndex);
    }
}

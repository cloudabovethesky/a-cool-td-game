using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameController : MonoBehaviour
{
    public static MainGameController instance;
    public int sellValue;
    public NodeUIController nodeUI;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;

        winUI.SetActive(false);
    }

    public int gold
    {
        get { return _gold; }
        set
        {
            _gold = value;
            goldText.text = "Gold : $" + gold;
        }
    }

    public int life
    {
        get { return _life; }
        set
        {
            _life = value;
            lifeText.text = "Life : " + _life;
            if (_life == 0)
            {
                gameOverUI.SetActive(true);
                endGame = true;
            }
        }
    }

    public void WinLevel()
    {
        endGame = true;
        PlayerPrefs.SetInt("LevelPassed", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
        winUI.SetActive(true);
    }

    public int _gold = 9999999;
    private int _life = 5;
    public bool endGame = false;

    public TMP_Text goldText;
    public TMP_Text lifeText;

    public GameObject gameOverUI;
    public GameObject winUI;

    public EnemySpawner enemySpawner;


    public void OnRetryBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnExitBtnClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnNextLevelBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnSellBtnClick()
    {
        gold += sellValue;
        Destroy(gameObject);
    }
}

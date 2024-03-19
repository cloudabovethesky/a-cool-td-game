using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
    public bool clearSaveData;
    public int levelPassed = 0;
    public Button[] levelButtons = new Button[0];

    public GameObject levelSelectUI;
    public GameObject controlSelectUI;
    public GameObject quitConfirmUI;
    public GameObject optionUI;
    public TMP_Dropdown aspectRatioDropdown; // Add a reference to your aspect ratio dropdown UI element
    public Camera mainCamera; // Reference to the main camera
    public RectTransform canvasRect; // Reference to the canvas RectTransform

    public AudioSource audioManager;
    private Dictionary<string, Vector2> aspectRatioOptions = new Dictionary<string, Vector2>
    {
        {"16:9", new Vector2(16f, 9f)},
        {"16:10", new Vector2(16f, 10f)},
        {"4:3", new Vector2(4f, 3f)},
        {"21:9", new Vector2(21f, 9f)}
    };

    private int nativeScreenWidth;
    private int nativeScreenHeight;

    private void Start()
    {
        // Store the native screen resolution
        nativeScreenWidth = Screen.currentResolution.width;
        nativeScreenHeight = Screen.currentResolution.height;

        // Initialize the aspect ratio dropdown with options
        aspectRatioDropdown.ClearOptions();
        aspectRatioDropdown.AddOptions(new List<string>(aspectRatioOptions.Keys));

        // Set the default aspect ratio
        SetAspectRatio("16:9");
    }

    public void OnStartBtnClick()
    {
        levelSelectUI.SetActive(true);

        if (clearSaveData)
        {
            PlayerPrefs.DeleteAll();
        }

        if (!PlayerPrefs.HasKey("LevelPassed"))
        {
            PlayerPrefs.SetInt("LevelPassed", 0);
            PlayerPrefs.Save();
        }
        levelPassed = PlayerPrefs.GetInt("LevelPassed");

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i <= levelPassed)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void OnBackLevelSelectBtnClick()
    {
        levelSelectUI.SetActive(false);
    }

    public void OnLevelBtnClick(int level)
    {
        SceneManager.LoadScene(level);
        audioManager.GetComponent<AudioSource>().Stop();
    }

    public void OnControlBtnClick()
    {
        controlSelectUI.SetActive(true);
    }

    public void OnQuitButton()
    {
        quitConfirmUI.SetActive(true);
    }

    public void OnNoButton()
    {
        quitConfirmUI.SetActive(false);
    }

    public void OnYesButton()
    {
        Application.Quit();
    }

    public void OnBackControlBtnClick()
    {
        controlSelectUI.SetActive(false);
    }

    public void OnOptionBtnClick()
    {
        optionUI.SetActive(true);
    }

    public void OnBackOptionBtnClick()
    {
        optionUI.SetActive(false);
    }

    public void CompleteLevel(int level)
    {
        if (level == levelPassed + 1)
        {
            levelPassed++;
            PlayerPrefs.SetInt("LevelPassed", levelPassed);
            PlayerPrefs.Save();

            for (int i = 0; i < levelButtons.Length; i++)
            {
                if (i <= levelPassed)
                {
                    levelButtons[i].interactable = true;
                }
                else
                {
                    levelButtons[i].interactable = false;
                }
            }
        }
    }

    public void OnAspectRatioDropdownValueChanged()
    {
        // Get the selected aspect ratio option from the dropdown
        string selectedOption = aspectRatioDropdown.options[aspectRatioDropdown.value].text;

        // Set the selected aspect ratio
        SetAspectRatio(selectedOption);
    }

    private void SetAspectRatio(string aspectRatio)
    {
        if (aspectRatioOptions.ContainsKey(aspectRatio))
        {
            Vector2 aspectRatioValue = aspectRatioOptions[aspectRatio];
            float targetAspect = aspectRatioValue.x / aspectRatioValue.y;

            // Calculate the new width and height while maintaining the native screen resolution
            int newWidth = nativeScreenWidth;
            int newHeight = Mathf.RoundToInt(newWidth / targetAspect);

            // If the calculated height exceeds the native screen height, calculate based on height instead
            if (newHeight > nativeScreenHeight)
            {
                newHeight = nativeScreenHeight;
                newWidth = Mathf.RoundToInt(newHeight * targetAspect);
            }

            // Set the screen resolution
            Screen.SetResolution(newWidth, newHeight, Screen.fullScreen);

            // Adjust the canvas size to match the new resolution
            canvasRect.sizeDelta = new Vector2(nativeScreenWidth, nativeScreenHeight);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public LevelSelector LevelSelector;
    public GameObject levelSelect;
    public GameObject buttons;
    public void Play()
    {
        LevelSelector.StartCoroutine(LevelSelector.SelectLevel(PlayerPrefs.GetInt("LevelReached", 1)));
    }
    public void ToggleLevelSelect()
    {
        buttons.SetActive(!buttons.activeSelf);
        levelSelect.SetActive(!levelSelect.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game!");
    }
}

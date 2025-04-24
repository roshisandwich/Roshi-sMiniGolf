using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI strokeUI;
    [Space(10)]
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private TextMeshProUGUI levelCompletedStrokeUI;
    [Space(10)]
    [SerializeField] private GameObject gameOverUI;

    [Header("Attributes")]
    [SerializeField] private int maxStrokes;
    private int strokes;
    [HideInInspector] public bool outOfStrokes;
    [HideInInspector] public bool levelCompleted;

    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateStrokeUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStrokeUI();

        if (strokes >= maxStrokes)
        {
            outOfStrokes = true;
        }
    }

    public void LevelComplete() 
    {
        levelCompleted = true;
        levelCompletedStrokeUI.text = strokes > 1 ? "Strokes Taken: " + strokes : "HOLE IN ONE!!!";
        PlayerPrefs.SetInt("LevelReached", SceneManager.GetActiveScene().buildIndex + 1);
        levelCompleteUI.SetActive(true);
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void IncreaseStroke()
    {
        strokes++;
    }

    private void UpdateStrokeUI()
    {
        strokeUI.text = strokes + "/" + maxStrokes;
    }
}

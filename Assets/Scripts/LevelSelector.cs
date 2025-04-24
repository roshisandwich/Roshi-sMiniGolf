using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Transform content;
    public Animator circleWipe;
    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

        for (int i = 0; i < content.transform.childCount; i++)
        {
            if (i + 1 > levelReached)
            {
                content.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
        }

        SetLevelButtons();
    }
    public void SetLevelButtons()
    {
        Transform[] children = new Transform[content.transform.childCount];
        for (int i = 0; i < content.transform.childCount; i++)
        {
            children[i] = content.transform.GetChild(i);

            TMP_Text childText = children[i].GetChild(0).GetComponent<TMP_Text>();
            Button childButton = children[i].GetComponent<Button>();
            int levelIndex = i + 1;

            childText.text = levelIndex.ToString();
            childButton.onClick.AddListener(() => StartCoroutine(SelectLevel(levelIndex)));

        }
    }

    public IEnumerator SelectLevel(int levelIndex)
    {
        circleWipe.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
        Debug.Log("hello");
    }

}

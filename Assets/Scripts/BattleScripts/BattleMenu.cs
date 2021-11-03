using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject battleMenuUI;
    public TextMeshProUGUI textReference;

    void Start()
    {
    }
    void Update()
    {

    }

    void Resume()
    {
        battleMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {

        StartCoroutine(PauseGame());
    }
    IEnumerator PauseGame()
    {
        yield return new WaitForSeconds(1f);

        battleMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StoryOpener : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.UnloadSceneAsync("OpeningScene");
        SceneManager.LoadSceneAsync("MariaWest", LoadSceneMode.Additive);
    }
}

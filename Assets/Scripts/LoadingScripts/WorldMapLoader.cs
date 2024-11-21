using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 2f;
    public string sceneUnload;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            LoadWorldMap();
        }

    }

    public void LoadWorldMap()
    {
        StartCoroutine(LoadMap("WorldMap"));
    }

    IEnumerator LoadMap(string worldMap)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.UnloadSceneAsync(sceneUnload);

        SceneManager.LoadSceneAsync(worldMap);
    }
}

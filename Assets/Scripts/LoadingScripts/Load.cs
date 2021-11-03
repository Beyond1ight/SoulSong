using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Load : MonoBehaviour
{

    public string scene;
    public string sceneUnload;

    public bool loaded;
    public bool unloaded;
    public Animator transition;
    public bool indoors;
    public bool worldMap = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !loaded)
        {
            Engine.e.loadTimer = true;
            Engine.e.inBattle = true;
            GameObject.Find("DayNightCycle").SetActive(false);
            unloaded = false;

            transition.SetTrigger("Start");

            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            loaded = true;
            Engine.e.currentScene = scene;
            Engine.e.indoors = indoors;
            Engine.e.inWorldMap = worldMap;
            if (Engine.e.inWorldMap)
            {
                if (Engine.e.mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize == 5)
                {
                    Engine.e.mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 7.5f;
                    Engine.e.activeParty.GetComponent<PlayerController>().speed = 3.5f;
                    if (Engine.e.activeParty.activeParty[1] != null)
                    {
                        Engine.e.activePartyMember2.GetComponent<APFollow>().speed = 3.5f;
                    }
                    if (Engine.e.activeParty.activeParty[2] != null)
                    {
                        Engine.e.activePartyMember3.GetComponent<APFollow>().speed = 3.5f;
                    }
                }
            }
            else
            {
                if (Engine.e.mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize == 7.5f)
                {
                    Engine.e.mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 5;
                    Engine.e.activeParty.GetComponent<PlayerController>().speed = 4.5f;
                    if (Engine.e.activeParty.activeParty[1] != null)
                    {
                        Engine.e.activePartyMember2.GetComponent<APFollow>().speed = 4.5f;
                    }
                    if (Engine.e.activeParty.activeParty[2] != null)
                    {
                        Engine.e.activePartyMember3.GetComponent<APFollow>().speed = 4.5f;
                    }
                }

                if (Engine.e.activeParty.transform.position != GetComponent<Teleport>().toLocation.transform.position)
                {
                    Engine.e.activeParty.transform.position = GetComponent<Teleport>().toLocation.transform.position;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && loaded)
        {
            Engine.e.loadTimer = true;

            unloaded = true;
            Engine.e.UnloadScene(sceneUnload);
            loaded = false;
            sceneUnload = string.Empty;


        }
    }

}


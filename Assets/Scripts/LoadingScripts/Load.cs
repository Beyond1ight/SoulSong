using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class Load : MonoBehaviour
{

    public string scene;
    public string sceneUnload;

    public bool loaded;
    public bool unloaded;
    public bool indoors, indoorLighting, inTown, partyShown, worldMap = false, loadOnInteraction;
    public static Load l;
    public GameObject toLocation;
    public GameObject activeParty2Location;
    public GameObject activeParty3Location;
    public string onLoadSceneReference;
    float animSpeed;

    public void Start()
    {
        l = this;
    }

    public void Teleport()
    {

        Engine.e.activeParty.transform.position = new Vector3(toLocation.transform.position.x, toLocation.transform.position.y);
        Engine.e.activePartyMember2.transform.position = new Vector3(activeParty2Location.transform.position.x, activeParty2Location.transform.position.y);
        Engine.e.activePartyMember3.transform.position = new Vector3(activeParty3Location.transform.position.x, activeParty3Location.transform.position.y);
    }

    public void SceneUnload()
    {
        sceneUnload = Engine.e.currentScene;

        Engine.e.zoneTitleReference.GetComponent<TextMeshProUGUI>().text = string.Empty;
        Engine.e.zoneTitleReference.GetComponent<TextMeshProUGUI>().text = onLoadSceneReference;
        Engine.e.canvasReference.GetComponent<PauseMenu>().partyLocationDisplay.text = string.Empty;
        Engine.e.canvasReference.GetComponent<PauseMenu>().partyLocationDisplay.text = "Location: " + onLoadSceneReference;

        Engine.e.UnloadScene(sceneUnload);

        Teleport();

        unloaded = true;
        loaded = false;
        sceneUnload = string.Empty;
        Engine.e.loadTimer = true;
        Engine.e.inBattle = true;
    }

    public void SceneLoadManager()
    {
        Engine.e.zoneTransition.GetComponent<Animator>().speed = 0f;

        Engine.e.inBattle = true;

        //Engine.e.loadTimer = true;
        //GameObject.Find("DayNightCycle").SetActive(false);
        //referenceObj.GetComponent<SceneMisc>().SceneManagement();
        unloaded = false;

        //Engine.e.zoneTransition.SetActive(true);

        loaded = true;
        Engine.e.currentScene = scene;
        Engine.e.indoors = indoors;
        Engine.e.indoorLighting = indoorLighting;
        Engine.e.inWorldMap = worldMap;

        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);


        if (Engine.e.inWorldMap)
        {
            if (Engine.e.mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize == 6.5f)
            {
                Engine.e.mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 10f;
            }

            Engine.e.activeParty.gameObject.transform.localScale = new Vector3(0.65f, 0.65f, 1f);
            Engine.e.activeParty.GetComponent<PlayerController>().speed = 3.5f;

            if (partyShown)
            {
                Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().enabled = true;
                Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().enabled = true;

                if (Engine.e.activeParty.activeParty[1] != null)
                {
                    Engine.e.activePartyMember2.GetComponent<APFollow>().speed = 3.5f;
                    Engine.e.activePartyMember2.GetComponent<APFollow>().distance = 1.0f;
                    Engine.e.activePartyMember2.transform.localScale = new Vector3(0.65f, 0.65f, 1f);

                }
                if (Engine.e.activeParty.activeParty[2] != null)
                {
                    Engine.e.activePartyMember3.GetComponent<APFollow>().speed = 3.5f;
                    Engine.e.activePartyMember3.GetComponent<APFollow>().distance = 1.0f;
                    Engine.e.activePartyMember3.transform.localScale = new Vector3(0.65f, 0.65f, 1f);
                }
            }
            else
            {
                Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().enabled = false;
                Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().enabled = false;

            }

            Engine.e.canvasReference.GetComponent<PauseMenu>().partyLocationDisplay.text = string.Empty;
            Engine.e.canvasReference.GetComponent<PauseMenu>().partyLocationDisplay.text = "Location: World Map";
            Engine.e.ableToSave = true;


        }
        else
        {
            if (Engine.e.mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize == 10f)
            {
                Engine.e.mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 6.5f;
            }

            Engine.e.activeParty.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1f);
            Engine.e.activeParty.GetComponent<PlayerController>().speed = 5.5f;

            Engine.e.ableToSave = false;

            Engine.e.zoneTitleReference.SetActive(true);


            if (partyShown)
            {
                Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().enabled = true;
                Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().enabled = true;

                if (Engine.e.activeParty.activeParty[1] != null)
                {
                    Engine.e.activePartyMember2.GetComponent<APFollow>().speed = 5.5f;
                    Engine.e.activePartyMember2.GetComponent<APFollow>().distance = 1.25f;
                    Engine.e.activePartyMember2.transform.localScale = new Vector3(1.0f, 1.0f, 1f);

                }
                if (Engine.e.activeParty.activeParty[2] != null)
                {
                    Engine.e.activePartyMember3.GetComponent<APFollow>().speed = 5.5f;
                    Engine.e.activePartyMember3.GetComponent<APFollow>().distance = 1.25f;
                    Engine.e.activePartyMember3.transform.localScale = new Vector3(1.0f, 1.0f, 1f);

                }
            }
            else
            {
                Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().enabled = false;
                Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().enabled = false;

            }
        }

        Engine.e.zoneTransition.GetComponent<Animator>().speed = 1f;

        Engine.e.inBattle = false;
        Engine.e.SaveGame(3);

    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "Player" && !loaded)
        {
            if (loadOnInteraction)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Engine.e.zoneTransition.SetActive(true);
                    //SceneLoadManager();
                }
            }
            else
            {
                Engine.e.zoneTransition.SetActive(true);
                //SceneUnload();
            }
        }
    }

}


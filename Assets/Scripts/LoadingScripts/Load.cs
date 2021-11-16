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
    public Animator transition;
    public bool indoors;
    public bool worldMap = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!Engine.e.inWorldMap)
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
                    if (Engine.e.mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize == 6.5f)
                    {
                        Engine.e.mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 10f;
                    }

                    Engine.e.activeParty.gameObject.transform.localScale = new Vector3(0.65f, 0.65f, 1f);
                    Engine.e.activeParty.GetComponent<PlayerController>().speed = 3.5f;

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
                    Engine.e.canvasReference.GetComponent<PauseMenu>().partyLocationDisplay.text = string.Empty;
                    Engine.e.canvasReference.GetComponent<PauseMenu>().partyLocationDisplay.text = "Location: World Map";
                    Engine.e.ableToSave = true;
                }
                else
                {
                    if (Engine.e.mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize == 10f)
                    {
                        Engine.e.mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 6.5f;
                    }

                    Engine.e.activeParty.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1f);
                    Engine.e.activeParty.GetComponent<PlayerController>().speed = 4.5f;
                    if (Engine.e.activeParty.activeParty[1] != null)
                    {
                        Engine.e.activePartyMember2.GetComponent<APFollow>().speed = 4.5f;
                        Engine.e.activePartyMember2.GetComponent<APFollow>().distance = 1.25f;
                        Engine.e.activePartyMember2.transform.localScale = new Vector3(1.0f, 1.0f, 1f);

                    }
                    if (Engine.e.activeParty.activeParty[2] != null)
                    {
                        Engine.e.activePartyMember3.GetComponent<APFollow>().speed = 4.5f;
                        Engine.e.activePartyMember3.GetComponent<APFollow>().distance = 1.25f;
                        Engine.e.activePartyMember3.transform.localScale = new Vector3(1.0f, 1.0f, 1f);

                    }
                    Engine.e.canvasReference.GetComponent<PauseMenu>().partyLocationDisplay.text = string.Empty;
                    Engine.e.canvasReference.GetComponent<PauseMenu>().partyLocationDisplay.text = "Location: " + GetComponent<Teleport>().onLoadSceneReference;
                    Engine.e.ableToSave = false;
                }

                if (Engine.e.activeParty.transform.position != GetComponent<Teleport>().toLocation.transform.position)
                {
                    Engine.e.activeParty.transform.position = GetComponent<Teleport>().toLocation.transform.position;
                }

                if (Engine.e.activeParty.activeParty[1] != null)
                {
                    Engine.e.activePartyMember2.transform.position = Engine.e.activeParty.transform.position;
                }
                if (Engine.e.activeParty.activeParty[2] != null)
                {
                    Engine.e.activePartyMember3.transform.position = Engine.e.activeParty.transform.position;
                }
            }
        }
        else
        {
            if (other.tag == "Player" && !loaded)
            {
                Engine.e.interactionPopup.GetComponent<TMP_Text>().text = scene;

                if (Input.GetKeyDown(KeyCode.E))
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
                        if (Engine.e.mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize == 6.5f)
                        {
                            Engine.e.mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 10f;
                        }

                        Engine.e.activeParty.gameObject.transform.localScale = new Vector3(0.65f, 0.65f, 1f);
                        Engine.e.activeParty.GetComponent<PlayerController>().speed = 3.5f;

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
                        if (Engine.e.mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize == 10f)
                        {
                            Engine.e.mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 6.5f;
                        }

                        Engine.e.activeParty.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1f);
                        Engine.e.activeParty.GetComponent<PlayerController>().speed = 4.5f;
                        if (Engine.e.activeParty.activeParty[1] != null)
                        {
                            Engine.e.activePartyMember2.GetComponent<APFollow>().speed = 4.5f;
                            Engine.e.activePartyMember2.GetComponent<APFollow>().distance = 1.25f;
                            Engine.e.activePartyMember2.transform.localScale = new Vector3(1.0f, 1.0f, 1f);

                        }
                        if (Engine.e.activeParty.activeParty[2] != null)
                        {
                            Engine.e.activePartyMember3.GetComponent<APFollow>().speed = 4.5f;
                            Engine.e.activePartyMember3.GetComponent<APFollow>().distance = 1.25f;
                            Engine.e.activePartyMember3.transform.localScale = new Vector3(1.0f, 1.0f, 1f);

                        }

                    }

                    if (Engine.e.activeParty.transform.position != GetComponent<Teleport>().toLocation.transform.position)
                    {
                        Engine.e.activeParty.transform.position = GetComponent<Teleport>().toLocation.transform.position;
                    }
                    if (Engine.e.activeParty.activeParty[1] != null)
                    {
                        Engine.e.activePartyMember2.transform.position = Engine.e.activeParty.transform.position;
                    }
                    if (Engine.e.activeParty.activeParty[2] != null)
                    {
                        Engine.e.activePartyMember3.transform.position = Engine.e.activeParty.transform.position;
                    }
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


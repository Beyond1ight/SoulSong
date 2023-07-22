using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector2 movement;
    Vector2 partyPosition;
    Character character;
    public bool isMoving;
    ActiveParty activeParty;
    Vector3 targetPos;
    GameObject targetGO;
    public bool controlledMovement = false;


    void FixedUpdate()
    {
        if (!controlledMovement)
        {
            rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
        }
        else
        {
            StartCoroutine(MoveCharacter());
        }
    }

    IEnumerator MoveCharacter()
    {
        targetPos = Vector3.MoveTowards(rb.transform.position, targetGO.transform.position, speed * Time.fixedDeltaTime);

        rb.MovePosition(targetPos);

        yield return new WaitForSeconds(0.3f);

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Cutscene Move To Start")
        {
            targetGO = other.gameObject;
            Engine.e.inBattle = true;
            controlledMovement = true;
        }

        if (other.tag == "Scene Transition")
        {
            Engine.e.sceneToBeLoaded = other.GetComponent<Load>().scene;
            Engine.e.sceneToBeLoadedName = other.GetComponent<Load>().onLoadSceneReference;
        }
        if (other.tag == "Cutscene Trigger" && other.GetComponent<CutsceneTrigger>().oneTimeCutscene)
        {

            Engine.e.AddOneTimeCutscenesForDataReference(other.gameObject);

        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Cutscene Trigger" && Vector3.Distance(transform.position, targetPos) < 0.01)
        {
            other.GetComponent<PlayableDirector>().Play();

            controlledMovement = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Engine.e.gridReference.gridDisplayed)
        {
            if (!Engine.e.inBattle)
            {
                if (!Engine.e.onRamp)
                {
                    movement.x = Input.GetAxisRaw("Horizontal") * speed;
                    movement.y = Input.GetAxisRaw("Vertical") * speed;
                }
                else
                {
                    movement.x = Input.GetAxisRaw("Horizontal") * speed;
                    movement.y = Input.GetAxisRaw("Horizontal") * speed;

                }
            }
            else
            {
                movement.x = 0;
                movement.y = 0;
            }

            if (movement.x != 0 || movement.y != 0)
            {
                isMoving = true;


                //Engine.e.mainCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0.20f;
                //Engine.e.mainCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0.20f;

            }
            else
            {
                isMoving = false;

                if (Engine.e.mainVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping != 0)
                {
                    Engine.e.mainVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping -= 0.05f;
                }
                if (Engine.e.mainVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping != 0)
                {
                    Engine.e.mainVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping -= 0.05f;
                }

                if (Engine.e.mainVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping < 0)
                {
                    Engine.e.mainVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
                }

                if (Engine.e.mainVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping < 0)
                {
                    Engine.e.mainVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
                }


            }
        }
    }
}

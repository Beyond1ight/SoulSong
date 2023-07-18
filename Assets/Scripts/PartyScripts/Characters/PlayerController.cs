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
    void Awake()
    {

    }

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
        Debug.Log("Bru");

        rb.MovePosition(targetPos);

        if (Vector3.Distance(transform.position, targetPos) < 0.1)
        {
            controlledMovement = false;
        }
        yield return new WaitForSeconds(0.3f);

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Cutscene Trigger")
        {
            other.GetComponent<PlayableDirector>().Play();
            controlledMovement = false;
        }

        if (other.tag == "Cutscene Move To Start")
        {
            targetGO = other.gameObject;
            controlledMovement = true;
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

            if (Engine.e.loadTimer)
            {
                StartCoroutine(UnloadTimer());
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

    IEnumerator UnloadTimer()
    {
        if (Engine.e.loadTimer)
        {
            Engine.e.loadTimer = false;
            yield return new WaitForSeconds(1f);
            Engine.e.inBattle = false;
        }
    }
}

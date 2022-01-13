using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector2 movement;
    Vector2 partyPosition;
    Character character;
    public bool isMoving;
    ActiveParty activeParty;
    void Awake()
    {

    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
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
                // Engine.e.mainCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0.20f;

            }
            else
            {
                isMoving = false;

                if (Engine.e.mainCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping != 0)
                {
                    Engine.e.mainCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping -= 0.05f;
                }
                if (Engine.e.mainCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping != 0)
                {
                    Engine.e.mainCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping -= 0.05f;
                }

                if (Engine.e.mainCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping < 0)
                {
                    Engine.e.mainCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
                }

                if (Engine.e.mainCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping < 0)
                {
                    Engine.e.mainCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
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

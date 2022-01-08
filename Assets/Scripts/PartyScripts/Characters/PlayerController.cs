using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector2 movement;
    Vector2 partyPosition;
    Character character;
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

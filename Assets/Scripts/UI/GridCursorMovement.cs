using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCursorMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector2 movement;
    public bool isMoving = false;

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);

    }


    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal") * speed;
        movement.y = Input.GetAxisRaw("Vertical") * speed;

        if (movement.x != 0 || movement.y != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        //  if (Engine.e.loadTimer)
        //  {
        //      StartCoroutine(UnloadTimer());
        //   }

        if (Engine.e.abilityScreenReference.gridDisplayed)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                GetComponent<GridCursor>().SetGridPerspective();
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
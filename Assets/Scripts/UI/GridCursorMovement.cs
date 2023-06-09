using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class GridCursorMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector2 movement;
    public bool isMoving, switchingChar = false;

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (movement * 3) * speed * Time.fixedDeltaTime);

    }


    // Update is called once per frame
    void Update()
    {

        if (!Engine.e.gridReference.abilitiesListDisplayed)
        {
            movement.x = Input.GetAxisRaw("Horizontal") * speed;
            movement.y = Input.GetAxisRaw("Vertical") * speed;

            if (movement.x != 0 || movement.y != 0)
            {
                isMoving = true;


                Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
                Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;

            }
            else
            {
                isMoving = false;


                Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
                Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;

            }
            //  if (Engine.e.loadTimer)
            //  {
            //      StartCoroutine(UnloadTimer());
            //   }
        }

        if (Engine.e.gridReference.gridDisplayed)
        {
            if (GetComponent<GridCursor>().currentNode != null)
            {
                GetComponent<GridCursor>().DisplayNodeInformation();
            }
            else
            {
                if (Engine.e.gridReference.helpReference.GetComponentInChildren<TextMeshProUGUI>().text != string.Empty)
                {
                    GetComponent<GridCursor>().ClearNodeInformation();
                }

            }

            if (Input.GetKeyDown(KeyCode.Z) && !Engine.e.gridReference.abilitiesListDisplayed)
            {
                GetComponent<GridCursor>().SetGridPerspective();
            }

            if (Input.GetKeyDown(KeyCode.Escape) && !Engine.e.gridReference.abilitiesListDisplayed)
            {
                Engine.e.gridReference.CloseGrid();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                if (!Engine.e.gridReference.abilitiesListDisplayed)
                {
                    Engine.e.gridReference.DisplayAbilitiesList();
                }
                else
                {
                    Engine.e.gridReference.CloseAbilitiesList();
                }
            }

            if (Input.GetKeyDown(KeyCode.RightBracket))
            {
                if (Engine.e.gridReference.grieveScreen)
                {
                    if (Engine.e.party[1] != null)
                    {
                        Engine.e.gridReference.ClearConnectionLines();
                        Engine.e.gridReference.SetMacScreen();
                    }
                }
                else
                {
                    if (Engine.e.gridReference.macScreen)
                    {
                        if (Engine.e.party[2] != null)
                        {
                            Engine.e.gridReference.ClearConnectionLines();
                            Engine.e.gridReference.SetFieldScreen();
                        }
                    }
                    else
                    {
                        if (Engine.e.gridReference.fieldScreen)
                        {
                            if (Engine.e.party[3] != null)
                            {
                                Engine.e.gridReference.ClearConnectionLines();
                                Engine.e.gridReference.SetRiggsScreen();
                            }
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftBracket))
            {
                /*switchingChar = true;

                if (switchingChar)
                {
                    Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
                    Engine.e.gridReference.gridPerspective.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
                }*/

                if (Engine.e.gridReference.riggsScreen)
                {
                    Engine.e.gridReference.ClearConnectionLines();
                    Engine.e.gridReference.SetFieldScreen();
                }
                else
                {
                    if (Engine.e.gridReference.fieldScreen)
                    {
                        Engine.e.gridReference.ClearConnectionLines();
                        Engine.e.gridReference.SetMacScreen();

                    }
                    else
                    {
                        if (Engine.e.gridReference.macScreen)
                        {
                            Engine.e.gridReference.ClearConnectionLines();
                            Engine.e.gridReference.SetGrieveScreen();

                        }
                    }
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

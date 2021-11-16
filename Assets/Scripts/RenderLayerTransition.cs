using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLayerTransition : MonoBehaviour
{

    bool entered, movingUp, movingDown = false;
    public Transform upperExit, lowerExit;
    public GameObject layer1Collision, layer2Collision;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !entered)
        {
            entered = true;

            if (entered)
            {
                if (other.transform.position.y <= lowerExit.position.y)
                {
                    movingUp = true;
                    Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingLayerName = "Layer2";
                    Engine.e.activeParty.gameObject.transform.position = new Vector3(Engine.e.activeParty.transform.position.x, Engine.e.activeParty.transform.position.y, 2);

                    Engine.e.aboveLayer = true;

                    layer1Collision.SetActive(false);
                    layer2Collision.SetActive(true);
                }

                if (other.transform.position.y >= upperExit.position.y)
                {
                    movingDown = true;
                    Engine.e.aboveLayer = false;
                    Engine.e.activeParty.gameObject.transform.position = new Vector3(Engine.e.activeParty.transform.position.x, Engine.e.activeParty.transform.position.y, 1);

                    layer1Collision.SetActive(true);
                    layer2Collision.SetActive(false);
                }
            }
        }

        if (Engine.e.party[1] != null)
        {

            Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().sortingOrder = 2;
            Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().sortingLayerName = "Layer2";
            Engine.e.activePartyMember2.transform.position = new Vector3(Engine.e.activePartyMember2.transform.position.x, Engine.e.activePartyMember2.transform.position.y, 2);


        }
        if (Engine.e.party[2] != null)
        {

            Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().sortingOrder = 2;
            Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().sortingLayerName = "Layer2";
            Engine.e.activePartyMember3.gameObject.transform.position = new Vector3(Engine.e.activePartyMember3.transform.position.x, Engine.e.activePartyMember3.transform.position.y, 2);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (entered)
        {
            if (other.tag == "Player")
            {
                if (movingUp)
                {
                    if (other.transform.position.y >= upperExit.position.y)
                    {
                        Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingOrder = 2;
                        Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingLayerName = "Layer2";
                        Engine.e.aboveLayer = true;
                        Engine.e.activeParty.gameObject.transform.position = new Vector3(Engine.e.activeParty.transform.position.x, Engine.e.activeParty.transform.position.y, 2);


                    }
                    else
                    {
                        Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingLayerName = "Layer1";
                        Engine.e.aboveLayer = false;
                        Engine.e.activeParty.gameObject.transform.position = new Vector3(Engine.e.activeParty.transform.position.x, Engine.e.activeParty.transform.position.y, 1);

                        layer1Collision.SetActive(true);
                        layer2Collision.SetActive(false);
                    }
                }

                if (movingDown)
                {
                    if (other.transform.position.y <= lowerExit.position.y)
                    {
                        Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingLayerName = "Layer1";
                        Engine.e.aboveLayer = false;
                        Engine.e.activeParty.gameObject.transform.position = new Vector3(Engine.e.activeParty.transform.position.x, Engine.e.activeParty.transform.position.y, 1);
                    }
                    else
                    {
                        Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingOrder = 2;
                        Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingLayerName = "Layer2";
                        Engine.e.aboveLayer = true;
                        Engine.e.activeParty.gameObject.transform.position = new Vector3(Engine.e.activeParty.transform.position.x, Engine.e.activeParty.transform.position.y, 2);
                        layer1Collision.SetActive(false);
                        layer2Collision.SetActive(true);
                    }
                }
                movingUp = false;
                movingDown = false;
                entered = false;
            }
        }

        if (other.tag == "PartyMember2")
        {
            Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().sortingOrder = Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingOrder;
            Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().sortingLayerName = Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingLayerName;

        }


        if (other.tag == "PartyMember3")
        {
            Engine.e.activePartyMember3.transform.position = new Vector3(Engine.e.activePartyMember3.transform.position.x, Engine.e.activePartyMember3.transform.position.y, Engine.e.activeParty.gameObject.transform.position.z);
            Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().sortingOrder = Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingOrder;
            Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().sortingLayerName = Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingLayerName;

        }
    }
}



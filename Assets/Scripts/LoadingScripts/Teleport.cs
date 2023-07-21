using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Was merged with Load class.
public class Teleport : MonoBehaviour
{
    public GameObject toLocation;
    public GameObject activeParty2Location;
    public GameObject activeParty3Location;
    public string onLoadSceneReference;

    public void OnTriggerStay2D(Collider2D other)
    {
        if (!Engine.e.inWorldMap)
        {
            if (other.name == "ActiveParty")
            {
                StartCoroutine(Teleport());
            }
        }
        else
        {
            if (other.name == "ActiveParty" && Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Teleport());
            }
        }
        IEnumerator Teleport()
        {
            yield return new WaitForSeconds(0.1f);
            Engine.e.activeParty.transform.position = new Vector3(toLocation.transform.position.x, toLocation.transform.position.y);
            Engine.e.activePartyMember2.transform.position = new Vector3(activeParty2Location.transform.position.x, activeParty2Location.transform.position.y);
            Engine.e.activePartyMember3.transform.position = new Vector3(activeParty3Location.transform.position.x, activeParty3Location.transform.position.y);

            //if (Engine.e.activeParty.transform.position != toLocation.transform.position)
            //{
            //    Engine.e.activeParty.transform.position = toLocation.transform.position;
            //}
        }
    }
}

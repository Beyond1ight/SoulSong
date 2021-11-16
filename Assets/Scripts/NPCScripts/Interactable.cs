using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Signal context;
    public bool playerInRange;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && GetComponent<SpriteRenderer>().sortingLayerName == Engine.e.activeParty.GetComponent<SpriteRenderer>().sortingLayerName)
        {
            context.Raise();
            playerInRange = true;
            Engine.e.inRange = true;
            Engine.e.interactionPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false;
            Engine.e.inRange = false;
            Engine.e.interactionPopup.SetActive(false);
        }
    }
}

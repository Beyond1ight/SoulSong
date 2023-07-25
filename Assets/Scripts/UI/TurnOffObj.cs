using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffObj : MonoBehaviour
{
    public void SetActiveFalse()
    {
        this.gameObject.SetActive(false);
    }

    public void TurnOffLeaderSprite()
    {
        Engine.e.activeParty.GetComponent<SpriteRenderer>().enabled = false;
    }
}

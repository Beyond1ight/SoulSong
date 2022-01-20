using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimations : MonoBehaviour
{

    //public GameObject[] allAnimations;

    // Items
    public GameObject healthPotionAnim, manaPotionAnim, antidoteAnim;

    // Drops
    public GameObject fireBlastAnim;

    // Skills

    public void StartDropAnimation(GameObject _spawnLoc, GameObject _targetLoc, Drops drop)
    {
        switch (drop.dropName)
        {
            case "Bolt":
                fireBlastAnim.transform.position = _targetLoc.transform.position;
                fireBlastAnim.GetComponent<Animator>().enabled = true;
                fireBlastAnim.SetActive(true);
                Engine.e.battleSystem.currentAnimation = fireBlastAnim.GetComponent<Animator>();
                break;
        }
        Engine.e.battleSystem.animState = AnimState.DROPANIM;
        Engine.e.battleSystem.animExists = true;
        Engine.e.battleSystem.animationTimer = drop.animationClip.length;
        Engine.e.battleSystem.currentAnimation.GetComponent<Animator>().Play("Start");
    }

    public void StartItemAnimation(GameObject _spawnLoc, GameObject _targetLoc, Item item)
    {
        switch (item.itemName)
        {
            case "Health Potion":
                healthPotionAnim.transform.position = _targetLoc.transform.position;
                healthPotionAnim.GetComponent<Animator>().enabled = true;
                healthPotionAnim.SetActive(true);
                Engine.e.battleSystem.currentAnimation = healthPotionAnim.GetComponent<Animator>();
                break;
            case "Mana Potion":
                manaPotionAnim.transform.position = _targetLoc.transform.position;
                manaPotionAnim.GetComponent<Animator>().enabled = true;
                manaPotionAnim.SetActive(true);
                Engine.e.battleSystem.currentAnimation = manaPotionAnim.GetComponent<Animator>();
                break;
            case "Antidote":
                antidoteAnim.transform.position = _targetLoc.transform.position;
                antidoteAnim.GetComponent<Animator>().enabled = true;
                antidoteAnim.SetActive(true);
                Engine.e.battleSystem.currentAnimation = antidoteAnim.GetComponent<Animator>();
                break;
        }
        Engine.e.battleSystem.animState = AnimState.ITEMANIM;
        Engine.e.battleSystem.animExists = true;
        Engine.e.battleSystem.animationTimer = item.animationClip.length;
        Engine.e.battleSystem.currentAnimation.GetComponent<Animator>().Play("Start");
    }
}
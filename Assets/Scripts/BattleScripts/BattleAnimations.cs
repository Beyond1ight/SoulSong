using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimations : MonoBehaviour
{
    public GameObject[] allAnimations;

    // Items
    public GameObject healthPotionAnim, manaPotionAnim;

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
                GetComponent<BattleSystem>().animState = AnimState.DROPANIM;
                GetComponent<BattleSystem>().currentAnimation = fireBlastAnim.GetComponent<Animator>();
                GetComponent<BattleSystem>().animExists = true;
                break;
        }
        GetComponent<BattleSystem>().animationTimer = drop.animationClip.length;
        GetComponent<BattleSystem>().currentAnimation.GetComponent<Animator>().Play("Start");
    }

    public void StartItemAnimation(GameObject _spawnLoc, GameObject _targetLoc, Item item)
    {
        switch (item.itemName)
        {
            case "Health Potion":
                healthPotionAnim.transform.position = _targetLoc.transform.position;
                healthPotionAnim.GetComponent<Animator>().enabled = true;
                healthPotionAnim.SetActive(true);
                GetComponent<BattleSystem>().animState = AnimState.ITEMANIM;
                GetComponent<BattleSystem>().currentAnimation = healthPotionAnim.GetComponent<Animator>();
                GetComponent<BattleSystem>().animExists = true;

                break;
            case "Mana Potion":
                manaPotionAnim.transform.position = _targetLoc.transform.position;
                manaPotionAnim.GetComponent<Animator>().enabled = true;
                manaPotionAnim.SetActive(true);
                GetComponent<BattleSystem>().animState = AnimState.ITEMANIM;
                GetComponent<BattleSystem>().currentAnimation = manaPotionAnim.GetComponent<Animator>();
                GetComponent<BattleSystem>().animExists = true;

                break;
        }
        GetComponent<BattleSystem>().animationTimer = item.animationClip.length;
        GetComponent<BattleSystem>().currentAnimation.GetComponent<Animator>().Play("Start");
    }
}
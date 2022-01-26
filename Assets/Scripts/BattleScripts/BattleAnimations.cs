using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimations : MonoBehaviour
{


    // Single Target w/ Drop
    public void StartDropAnimation(GameObject _spawnLoc, GameObject _targetLoc, Drops drop)
    {

        Engine.e.battleSystem.currentAnimation[0].GetComponent<Animator>().runtimeAnimatorController = drop.GetComponent<Animator>().runtimeAnimatorController;
        Engine.e.battleSystem.currentAnimation[0].transform.position = _targetLoc.transform.position;
        Engine.e.battleSystem.currentAnimation[0].GetComponent<Animator>().enabled = true;
        Engine.e.battleSystem.currentAnimation[0].SetActive(true);
        Engine.e.battleSystem.animState = AnimState.ITEMANIM;
        Engine.e.battleSystem.animExists = true;
        Engine.e.battleSystem.animationTimer = drop.GetComponent<Item>().animationClip.length;
    }
    // Targeting all active party members w/ Drop
    public void StartDropAnimationAllTeam(GameObject _spawnLoc, Drops drop)
    {


        for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
        {
            if (Engine.e.activeParty.activeParty[i] != null)
            {
                Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController = drop.GetComponent<Animator>().runtimeAnimatorController;

                if (i == 0)
                {
                    Engine.e.battleSystem.currentAnimation[i].transform.position = Engine.e.activeParty.gameObject.transform.position;
                }
                if (i == 1)
                {
                    Engine.e.battleSystem.currentAnimation[i].transform.position = Engine.e.activePartyMember2.transform.position;
                }
                if (i == 2)
                {
                    Engine.e.battleSystem.currentAnimation[i].transform.position = Engine.e.activePartyMember3.transform.position;
                }

                Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().enabled = true;
                Engine.e.battleSystem.currentAnimation[i].SetActive(true);
            }
        }


        Engine.e.battleSystem.animState = AnimState.DROPANIM;
        Engine.e.battleSystem.animExists = true;
        Engine.e.battleSystem.animationTimer = drop.GetComponent<Item>().animationClip.length;

    }

    // Targeting all enemies w/ Drop
    public void StartDropAnimationAllEnemies(GameObject _spawnLoc, Drops drop)
    {

        for (int i = 0; i < Engine.e.battleSystem.enemies.Length; i++)
        {
            if (Engine.e.battleSystem.enemies[i] != null && Engine.e.battleSystem.enemies[i].currentHealth > 0)
            {
                Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController = drop.GetComponent<Animator>().runtimeAnimatorController;
                Engine.e.battleSystem.currentAnimation[i].transform.position = Engine.e.battleSystem.enemies[i].transform.position;
                Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().enabled = true;
                Engine.e.battleSystem.currentAnimation[i].SetActive(true);
            }
        }

        Engine.e.battleSystem.animState = AnimState.DROPANIM;
        Engine.e.battleSystem.animExists = true;
        Engine.e.battleSystem.animationTimer = drop.GetComponent<Item>().animationClip.length;


    }

    public void StartItemAnimation(GameObject _spawnLoc, GameObject _targetLoc, Item item)
    {

        Engine.e.battleSystem.currentAnimation[0].GetComponent<Animator>().runtimeAnimatorController = item.GetComponent<Animator>().runtimeAnimatorController;
        Engine.e.battleSystem.currentAnimation[0].transform.position = _targetLoc.transform.position;
        Engine.e.battleSystem.currentAnimation[0].GetComponent<Animator>().enabled = true;
        Engine.e.battleSystem.currentAnimation[0].SetActive(true);

        Engine.e.battleSystem.animState = AnimState.ITEMANIM;
        Engine.e.battleSystem.animExists = true;
        Engine.e.battleSystem.animationTimer = item.animationClip.length;
        for (int i = 0; i < Engine.e.battleSystem.currentAnimation.Length; i++)
        {
            if (Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController != null)
            {
                Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().Play("Start");
            }
        }
    }
    public void StartItemAnimationAllTeam(GameObject _spawnLoc, Item item)
    {

        for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
        {
            if (Engine.e.activeParty.activeParty[i] != null)
            {
                Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController = item.GetComponent<Animator>().runtimeAnimatorController;

                if (i == 0)
                {
                    Engine.e.battleSystem.currentAnimation[i].transform.position = Engine.e.activeParty.gameObject.transform.position;
                }
                if (i == 1)
                {
                    Engine.e.battleSystem.currentAnimation[i].transform.position = Engine.e.activePartyMember2.transform.position;
                }
                if (i == 2)
                {
                    Engine.e.battleSystem.currentAnimation[i].transform.position = Engine.e.activePartyMember3.transform.position;
                }

                Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().enabled = true;
                Engine.e.battleSystem.currentAnimation[i].SetActive(true);
            }
        }

        Engine.e.battleSystem.animState = AnimState.ITEMANIM;
        Engine.e.battleSystem.animExists = true;
        Engine.e.battleSystem.animationTimer = item.animationClip.length;

        for (int i = 0; i < Engine.e.battleSystem.currentAnimation.Length; i++)
        {
            if (Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController != null)
            {
                Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().Play("Start");
            }
        }
    }

    public void StartItemAnimationAllEnemies(GameObject _spawnLoc, Item item)
    {

        for (int i = 0; i < Engine.e.battleSystem.enemies.Length; i++)
        {
            if (Engine.e.battleSystem.enemies[i] != null && Engine.e.battleSystem.enemies[i].currentHealth > 0)
            {
                Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController = item.GetComponent<Animator>().runtimeAnimatorController;
                Engine.e.battleSystem.currentAnimation[i].transform.position = Engine.e.battleSystem.enemies[i].transform.position;
                Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().enabled = true;
                Engine.e.battleSystem.currentAnimation[i].SetActive(true);
            }
        }

        Engine.e.battleSystem.animState = AnimState.ITEMANIM;
        Engine.e.battleSystem.animExists = true;
        Engine.e.battleSystem.animationTimer = item.animationClip.length;

        for (int i = 0; i < Engine.e.battleSystem.currentAnimation.Length; i++)
        {
            if (Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController != null)
            {
                Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().Play("Start");
            }
        }
    }
}
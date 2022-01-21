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

    // Single Target w/ Drop
    public void StartDropAnimation(GameObject _spawnLoc, GameObject _targetLoc, Drops drop)
    {
        switch (drop.dropName)
        {
            case "Bolt":
                fireBlastAnim.transform.position = _targetLoc.transform.position;
                fireBlastAnim.GetComponent<Animator>().enabled = true;
                fireBlastAnim.SetActive(true);
                // Engine.e.battleSystem.currentAnimation[0] = fireBlastAnim.GetComponent<Animator>();
                break;
        }
        Engine.e.battleSystem.animState = AnimState.DROPANIM;
        Engine.e.battleSystem.animExists = true;
        Engine.e.battleSystem.animationTimer = drop.animationClip.length;
        for (int i = 0; i < Engine.e.battleSystem.currentAnimation.Length; i++)
        {
            if (Engine.e.battleSystem.currentAnimation[i] != null)
            {
                Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().Play("Start");
            }
        }
    }

    // Targeting all active party members w/ Drop
    public void StartDropAnimationAllTeam(GameObject _spawnLoc, Drops drop)
    {
        switch (drop.dropName)
        {
            case "Fire Blast":
                for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
                {
                    if (Engine.e.activeParty.activeParty[i] != null)
                    {
                        Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController = fireBlastAnim.GetComponent<Animator>().runtimeAnimatorController;

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
                break;
        }
        Engine.e.battleSystem.animState = AnimState.DROPANIM;
        Engine.e.battleSystem.animExists = true;
        Engine.e.battleSystem.animationTimer = drop.animationClip.length;

        for (int i = 0; i < Engine.e.battleSystem.currentAnimation.Length; i++)
        {
            if (Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController != null)
            {
                Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().Play("Start");
            }
        }
    }

    // Targeting all enemies w/ Drop
    public void StartDropAnimationAllEnemies(GameObject _spawnLoc, Drops drop)
    {
        switch (drop.dropName)
        {
            case "Fire Blast":
                for (int i = 0; i < Engine.e.battleSystem.enemies.Length; i++)
                {
                    if (Engine.e.battleSystem.enemies[i] != null && Engine.e.battleSystem.enemies[i].currentHealth > 0)
                    {
                        Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController = fireBlastAnim.GetComponent<Animator>().runtimeAnimatorController;
                        Engine.e.battleSystem.currentAnimation[i].transform.position = Engine.e.battleSystem.enemies[i].transform.position;
                        Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().enabled = true;
                        Engine.e.battleSystem.currentAnimation[i].SetActive(true);
                    }
                }
                break;
        }
        Engine.e.battleSystem.animState = AnimState.DROPANIM;
        Engine.e.battleSystem.animExists = true;
        Engine.e.battleSystem.animationTimer = drop.animationClip.length;

        for (int i = 0; i < Engine.e.battleSystem.currentAnimation.Length; i++)
        {
            if (Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController != null)
            {
                Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().Play("Start");
            }
        }
    }

    public void StartItemAnimation(GameObject _spawnLoc, GameObject _targetLoc, Item item)
    {
        switch (item.itemName)
        {
            case "Health Potion":
                Engine.e.battleSystem.currentAnimation[0].GetComponent<Animator>().runtimeAnimatorController = healthPotionAnim.GetComponent<Animator>().runtimeAnimatorController;
                Engine.e.battleSystem.currentAnimation[0].transform.position = _targetLoc.transform.position;
                Engine.e.battleSystem.currentAnimation[0].GetComponent<Animator>().enabled = true;
                Engine.e.battleSystem.currentAnimation[0].SetActive(true);
                break;
            case "Mana Potion":
                Engine.e.battleSystem.currentAnimation[0].GetComponent<Animator>().runtimeAnimatorController = manaPotionAnim.GetComponent<Animator>().runtimeAnimatorController;
                Engine.e.battleSystem.currentAnimation[0].transform.position = _targetLoc.transform.position;
                Engine.e.battleSystem.currentAnimation[0].GetComponent<Animator>().enabled = true;
                Engine.e.battleSystem.currentAnimation[0].SetActive(true);
                break;
            case "Antidote":
                Engine.e.battleSystem.currentAnimation[0].GetComponent<Animator>().runtimeAnimatorController = manaPotionAnim.GetComponent<Animator>().runtimeAnimatorController;
                Engine.e.battleSystem.currentAnimation[0].transform.position = _targetLoc.transform.position;
                Engine.e.battleSystem.currentAnimation[0].GetComponent<Animator>().enabled = true;
                Engine.e.battleSystem.currentAnimation[0].SetActive(true);
                break;
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
    public void StartItemAnimationAllTeam(GameObject _spawnLoc, Item item)
    {
        switch (item.itemName)
        {
            case "Health Potion":
                for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
                {
                    if (Engine.e.activeParty.activeParty[i] != null)
                    {
                        Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController = healthPotionAnim.GetComponent<Animator>().runtimeAnimatorController;

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
                break;
            case "Mana Potion":
                for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
                {
                    if (Engine.e.activeParty.activeParty[i] != null)
                    {
                        Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController = manaPotionAnim.GetComponent<Animator>().runtimeAnimatorController;

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
                break;
            case "Antidote":
                for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
                {
                    if (Engine.e.activeParty.activeParty[i] != null)
                    {
                        Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController = antidoteAnim.GetComponent<Animator>().runtimeAnimatorController;

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
                break;
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
        switch (item.itemName)
        {
            case "Health Potion":
                for (int i = 0; i < Engine.e.battleSystem.enemies.Length; i++)
                {
                    if (Engine.e.battleSystem.enemies[i] != null && Engine.e.battleSystem.enemies[i].currentHealth > 0)
                    {
                        Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController = healthPotionAnim.GetComponent<Animator>().runtimeAnimatorController;
                        Engine.e.battleSystem.currentAnimation[i].transform.position = Engine.e.battleSystem.enemies[i].transform.position;
                        Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().enabled = true;
                        Engine.e.battleSystem.currentAnimation[i].SetActive(true);
                    }
                }
                break;
            case "Mana Potion":
                for (int i = 0; i < Engine.e.battleSystem.enemies.Length; i++)
                {
                    if (Engine.e.battleSystem.enemies[i] != null && Engine.e.battleSystem.enemies[i].currentHealth > 0)
                    {
                        Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController = manaPotionAnim.GetComponent<Animator>().runtimeAnimatorController;
                        Engine.e.battleSystem.currentAnimation[i].transform.position = Engine.e.battleSystem.enemies[i].transform.position;
                        Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().enabled = true;
                        Engine.e.battleSystem.currentAnimation[i].SetActive(true);
                    }
                }
                break;
            case "Antidote":
                for (int i = 0; i < Engine.e.battleSystem.enemies.Length; i++)
                {
                    if (Engine.e.battleSystem.enemies[i] != null && Engine.e.battleSystem.enemies[i].currentHealth > 0)
                    {
                        Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController = antidoteAnim.GetComponent<Animator>().runtimeAnimatorController;
                        Engine.e.battleSystem.currentAnimation[i].transform.position = Engine.e.battleSystem.enemies[i].transform.position;
                        Engine.e.battleSystem.currentAnimation[i].GetComponent<Animator>().enabled = true;
                        Engine.e.battleSystem.currentAnimation[i].SetActive(true);
                    }
                }
                break;
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
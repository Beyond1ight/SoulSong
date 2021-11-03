using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class EnemyDropMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject drop;
    bool dmgPopupDisplay = false;
    int charBeingTargeted = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Engine.e.battleSystem.dropExists = true;

        if (Engine.e.timeOfDay < 300 || Engine.e.timeOfDay > 700)
        {
            GetComponent<Light2D>().intensity = 0.5f;
        }
        else
        {
            GetComponent<Light2D>().intensity = 1f;
        }

    }

    public IEnumerator CheckDistance()
    {
        GameObject characterLocation = null;
        Character character = null;
        SpriteRenderer characterObjectSprite = null;
        SpriteRenderer characterSprite = null;
        charBeingTargeted = Engine.e.battleSystem.previousTargetReferenceEnemy;
        if (charBeingTargeted == 0)
        {
            characterLocation = Engine.e.activeParty.gameObject;
            character = Engine.e.activeParty.activeParty[0].GetComponent<Character>();
            characterObjectSprite = Engine.e.activeParty.GetComponent<SpriteRenderer>();
            characterSprite = Engine.e.activeParty.activeParty[0].GetComponent<SpriteRenderer>();
        }

        if (charBeingTargeted == 1)
        {
            characterLocation = Engine.e.activePartyMember2.gameObject;
            character = Engine.e.activeParty.activeParty[1].GetComponent<Character>();
            characterObjectSprite = Engine.e.activePartyMember2.GetComponent<SpriteRenderer>();
            characterSprite = Engine.e.activeParty.activeParty[1].GetComponent<SpriteRenderer>();

        }

        if (charBeingTargeted == 2)
        {
            characterLocation = Engine.e.activePartyMember3.gameObject;
            character = Engine.e.activeParty.activeParty[2].GetComponent<Character>();
            characterObjectSprite = Engine.e.activePartyMember3.GetComponent<SpriteRenderer>();
            characterSprite = Engine.e.activeParty.activeParty[2].GetComponent<SpriteRenderer>();

        }



        Vector3 targetPos = Vector3.MoveTowards(transform.position, characterLocation.transform.position, 5 * Time.deltaTime);
        rb.MovePosition(targetPos);

        if (Vector3.Distance(rb.transform.position, characterLocation.transform.position) < 0.3)
        {
            if (dmgPopupDisplay == false)
            {
                dmgPopupDisplay = true;
                GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, characterLocation.transform.position, Quaternion.identity);
                dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = Engine.e.battleSystem.damageTotal.ToString();

                if (Engine.e.battleSystem.lastDropChoice.dropName == "Bio")
                {
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = string.Empty;

                    if (character.isPoisoned)
                    {
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Poisoned";
                    }
                    else
                    {
                        if (character.poisonDefense < 100)
                        {
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Resisted";
                        }
                        else
                        {
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Immune";
                        }
                    }
                }

                if (Engine.e.battleSystem.lastDropChoice.dropName == "Knockout")
                {
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = string.Empty;

                    if (character.isAsleep)
                    {
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Sleeping";
                    }
                    else
                    {
                        if (character.sleepDefense < 100)
                        {
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Resisted";
                        }
                        else
                        {
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Immune";
                        }
                    }
                }

                if (Engine.e.battleSystem.lastDropChoice.dropName == "Blind")
                {
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = string.Empty;

                    if (character.isConfused)
                    {
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Confused";
                    }
                    else
                    {
                        if (character.sleepDefense < 100)
                        {
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Resisted";
                        }
                        else
                        {
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Immune";
                        }
                    }
                }

                if (Engine.e.battleSystem.lastDropChoice.dropName == "Death")
                {
                    character.InflictDeathAttack();
                }
                Destroy(dmgPopup, 1f);
            }

            characterObjectSprite.color = GetComponent<SpriteRenderer>().color;
            GetComponent<ParticleSystem>().Emit(1);
            Engine.e.battleSystem.hud.displayHealth[Engine.e.charBeingTargeted].text = Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].gameObject.GetComponent<Character>().currentHealth.ToString();
            yield return new WaitForSeconds(1.0f);
            Engine.e.battleSystem.dropExists = false;

            if (!character.isPoisoned && !character.isAsleep)
            {
                characterSprite.color = Color.white;
                characterObjectSprite.color = Color.white;
            }
            else
            {
                if (character.isPoisoned)
                {

                    characterSprite.color = Color.green;
                    characterObjectSprite.color = Color.green;
                }

                if (character.isAsleep)
                {
                    characterSprite.color = Color.grey;
                    characterObjectSprite.color = Color.grey;
                }
            }
            Destroy(this.gameObject);
        }
    }


    void FixedUpdate()
    {
        StartCoroutine(CheckDistance());
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class SupportDropMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject drop;
    bool dmgPopupDisplay = false;

    // Start is called before the first frame update
    void Awake()
    {
        Engine.e.battleSystem.dropExists = true;
        Engine.e.battleSystem.DeactivateAttackButtons();

    }

    public IEnumerator CheckDistance()
    {
        /*if (GameManager.gameManager.battleSystem.lastDropChoice.dropName == "Soothing Rain")
        {
            yield return new WaitForSeconds(4f);
            GameManager.gameManager.weatherRain.SetActive(false);
        }*/

        if (Engine.e.charBeingTargeted == 0)
        {
            Vector3 targetPos = Vector3.MoveTowards(transform.position, Engine.e.activeParty.transform.position, 5 * Time.deltaTime);
            rb.MovePosition(targetPos);

            if (Vector3.Distance(rb.transform.position, Engine.e.activeParty.transform.position) < 0.1)
            {
                if (dmgPopupDisplay == false)
                {
                    dmgPopupDisplay = true;
                    GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activeParty.transform.position, Quaternion.identity);
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = Engine.e.battleSystem.damageTotal.ToString();
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);
                    Destroy(dmgPopup, 1f);
                }

                Engine.e.activeParty.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
                GetComponent<ParticleSystem>().Emit(1);

                Engine.e.battleSystem.hud.displayHealth[Engine.e.charBeingTargeted].text = Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].gameObject.GetComponent<Character>().currentHealth.ToString();
                Engine.e.battleSystem.hud.displayMana[Engine.e.charBeingTargeted].text = Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].gameObject.GetComponent<Character>().currentMana.ToString();
                Engine.e.battleSystem.hud.displayEnergy[Engine.e.charBeingTargeted].text = Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].gameObject.GetComponent<Character>().currentEnergy.ToString();

                yield return new WaitForSeconds(1.0f);
                Engine.e.battleSystem.dropExists = false;
                Engine.e.activeParty.GetComponent<SpriteRenderer>().color = Color.white;
                Destroy(this.gameObject);

            }
        }
        if (Engine.e.charBeingTargeted == 1)
        {
            Vector3 targetPos = Vector3.MoveTowards(transform.position, Engine.e.activePartyMember2.transform.position, 5 * Time.deltaTime);
            rb.MovePosition(targetPos);

            if (Vector3.Distance(rb.transform.position, Engine.e.activePartyMember2.transform.position) < 0.1)
            {
                if (dmgPopupDisplay == false)
                {
                    dmgPopupDisplay = true;
                    GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activePartyMember2.transform.position, Quaternion.identity);
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = Engine.e.battleSystem.damageTotal.ToString();
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);

                    Destroy(dmgPopup, 1f);
                }
                Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
                GetComponent<ParticleSystem>().Emit(1);
                Engine.e.battleSystem.hud.displayHealth[Engine.e.charBeingTargeted].text = Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].gameObject.GetComponent<Character>().currentHealth.ToString();
                Engine.e.battleSystem.hud.displayMana[Engine.e.charBeingTargeted].text = Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].gameObject.GetComponent<Character>().currentMana.ToString();
                Engine.e.battleSystem.hud.displayEnergy[Engine.e.charBeingTargeted].text = Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].gameObject.GetComponent<Character>().currentEnergy.ToString();
                yield return new WaitForSeconds(1.0f);
                Engine.e.battleSystem.dropExists = false;
                Engine.e.activeParty.activePartyMember2.GetComponent<SpriteRenderer>().color = Color.white;
                Destroy(this.gameObject);
            }
        }
        if (Engine.e.charBeingTargeted == 2)
        {
            Vector3 targetPos = Vector3.MoveTowards(transform.position, Engine.e.activePartyMember3.transform.position, 5 * Time.deltaTime);
            rb.MovePosition(targetPos);

            if (Vector3.Distance(rb.transform.position, Engine.e.activePartyMember3.transform.position) < 0.1)
            {
                if (dmgPopupDisplay == false)
                {
                    dmgPopupDisplay = true;
                    GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activePartyMember3.transform.position, Quaternion.identity);
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = Engine.e.battleSystem.damageTotal.ToString();
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);

                    Destroy(dmgPopup, 1f);
                }

                Engine.e.activeParty.activePartyMember3.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
                GetComponent<ParticleSystem>().Emit(1);
                Engine.e.battleSystem.hud.displayHealth[Engine.e.charBeingTargeted].text = Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].gameObject.GetComponent<Character>().currentHealth.ToString();
                Engine.e.battleSystem.hud.displayMana[Engine.e.charBeingTargeted].text = Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].gameObject.GetComponent<Character>().currentMana.ToString();
                Engine.e.battleSystem.hud.displayEnergy[Engine.e.charBeingTargeted].text = Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].gameObject.GetComponent<Character>().currentEnergy.ToString();
                yield return new WaitForSeconds(1.0f);
                Engine.e.battleSystem.dropExists = false;
                Engine.e.activeParty.activePartyMember3.GetComponent<SpriteRenderer>().color = Color.white;
                Destroy(this.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        StartCoroutine(CheckDistance());
    }
}

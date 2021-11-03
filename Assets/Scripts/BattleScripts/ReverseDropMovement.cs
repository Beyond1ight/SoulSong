using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class ReverseDropMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject drop;
    bool dmgPopupDisplay = false;
    // Start is called before the first frame update
    void Awake()
    {

        Engine.e.battleSystem.dropExists = true;
        Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
        GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].transform.position, Quaternion.identity);
        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = Engine.e.battleSystem.skillBoostTotal.ToString();
        Destroy(dmgPopup, 1f);

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
        SpriteRenderer characterObjectSprite = null;
        SpriteRenderer characterSprite = null;

        characterObjectSprite = Engine.e.battleSystem.characterDropTarget.GetComponent<SpriteRenderer>();
        characterSprite = Engine.e.activeParty.activeParty[0].GetComponent<SpriteRenderer>();

        Vector3 targetPos = Vector3.MoveTowards(transform.position, Engine.e.battleSystem.characterDropTarget.transform.position, 5 * Time.deltaTime);
        rb.MovePosition(targetPos);

        if (Vector3.Distance(rb.transform.position, Engine.e.battleSystem.characterDropTarget.transform.position) < 0.1)
        {
            if (dmgPopupDisplay == false)
            {
                dmgPopupDisplay = true;

                GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.battleSystem.characterDropTarget.transform.position, Quaternion.identity);
                // Change this value
                dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = Engine.e.battleSystem.skillBoostTotal.ToString();
                if (Engine.e.battleSystem.mpRestore)
                {
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 54, 255, 255);
                }
                Destroy(dmgPopup, 1f);
            }
            Engine.e.battleSystem.hud.displayEnemyHealth[Engine.e.battleSystem.target].text = Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].gameObject.GetComponent<Enemy>().health.ToString();
            GetComponent<ParticleSystem>().Emit(1);
            Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;

            if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].gameObject.GetComponent<Enemy>().health <= 0)
            {
                if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].gameObject.GetComponent<EnemyMovement>())
                {
                    Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].gameObject.GetComponent<EnemyMovement>().enabled = false;
                }

            }

            characterObjectSprite.color = GetComponent<SpriteRenderer>().color;

            yield return new WaitForSeconds(1.0f);
            Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<SpriteRenderer>().color = Color.white;
            characterSprite.color = Color.white;
            characterObjectSprite.color = Color.white;

            if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].gameObject.GetComponent<Enemy>().health <= 0)
            {
                if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].gameObject.GetComponent<Light2D>())
                {
                    Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].gameObject.GetComponent<Light2D>().enabled = false;
                }

                Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].gameObject.GetComponent<SpriteRenderer>().enabled = false;

                Engine.e.battleSystem.enemyUI[Engine.e.battleSystem.target].SetActive(false);
            }
            Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<SpriteRenderer>().color = Color.white;
            Engine.e.battleSystem.mpRestore = false;
            Engine.e.battleSystem.dropExists = false;


            Destroy(this.gameObject);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(CheckDistance());
    }
}


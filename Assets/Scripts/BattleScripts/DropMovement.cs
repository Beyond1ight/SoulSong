using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class DropMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject drop;
    bool dmgPopupDisplay = false;
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
        //Vector3 targetPos = Vector3.MoveTowards(transform.position, GameManager.gameManager.battleSystem.leaderPos, 4 * Time.deltaTime);
        Vector3 targetPos = Vector3.MoveTowards(transform.position, Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].transform.position, 5 * Time.deltaTime);
        rb.MovePosition(targetPos);

        //if (Vector3.Distance(rb.transform.position, GameManager.gameManager.battleSystem.leaderPos) < 0.1)
        if (Vector3.Distance(rb.transform.position, Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].transform.position) < 0.1)
        {
            if (dmgPopupDisplay == false)
            {

                dmgPopupDisplay = true;
                GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].transform.position, Quaternion.identity);

                dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = Engine.e.battleSystem.damageTotal.ToString();
                if (Engine.e.battleSystem.lastDropChoice != null)
                {

                    if (Engine.e.battleSystem.lastDropChoice.dropName == "Bio")
                    {
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = string.Empty;
                        if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().isPoisoned)
                        {
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Poisoned";
                        }
                        else
                        {
                            if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().poisonDefense < 100)
                            {
                                dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Resisted";
                            }
                            if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().poisonDefense == 100)
                            {
                                dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Immune";
                            }
                            if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().poisonDefense > 100)
                            {
                                float dmgTotal = Engine.e.battleSystem.damageTotal * -1;
                                dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = dmgTotal.ToString();
                                dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);

                            }
                        }
                    }


                    if (Engine.e.battleSystem.lastDropChoice.dropName == "Knockout")
                    {
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = string.Empty;

                        if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().isAsleep)
                        {
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Sleeping";
                            if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<EnemyMovement>() != null)
                            {
                                Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<EnemyMovement>().enabled = false;
                            }
                        }
                        else
                        {
                            if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().sleepDefense < 100)
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

                        if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().isConfused)
                        {
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Confused";

                        }
                        else
                        {
                            if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().confuseDefense < 100)
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
                        Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().InflictDeathDrop();
                    }


                    if (Engine.e.battleSystem.lastDropChoice.dropType == "Fire")
                    {
                        if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().fireDefense > 100)
                        {
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = string.Empty;
                            float dmgTotal = (Engine.e.battleSystem.damageTotal * -1);
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = dmgTotal.ToString();
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);

                        }
                    }
                    if (Engine.e.battleSystem.lastDropChoice.dropType == "Ice")
                    {
                        if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().iceDefense > 100)
                        {
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = string.Empty;
                            float dmgTotal = (Engine.e.battleSystem.damageTotal * -1);
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = dmgTotal.ToString();
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);

                        }
                    }
                    if (Engine.e.battleSystem.lastDropChoice.dropType == "Lightning")
                    {
                        if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().lightningDefense > 100)
                        {
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = string.Empty;
                            float dmgTotal = (Engine.e.battleSystem.damageTotal * -1);
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = dmgTotal.ToString();
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);

                        }
                    }
                    if (Engine.e.battleSystem.lastDropChoice.dropType == "Water")
                    {
                        if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().waterDefense > 100)
                        {
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = string.Empty;
                            float dmgTotal = (Engine.e.battleSystem.damageTotal * -1);
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = dmgTotal.ToString();
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);

                        }
                    }
                    if (Engine.e.battleSystem.lastDropChoice.dropType == "Shadow")
                    {
                        if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().shadowDefense > 100)
                        {
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = string.Empty;
                            float dmgTotal = (Engine.e.battleSystem.damageTotal * -1);
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = dmgTotal.ToString();
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);

                        }
                    }
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
            yield return new WaitForSeconds(1.0f);

            if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].gameObject.GetComponent<Enemy>().health <= 0)
            {
                if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].gameObject.GetComponent<Light2D>())
                {
                    Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].gameObject.GetComponent<Light2D>().enabled = false;
                }

                Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].gameObject.GetComponent<SpriteRenderer>().enabled = false;

                Engine.e.battleSystem.enemyUI[Engine.e.battleSystem.target].SetActive(false);
            }

            if (!Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().isPoisoned && !Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().isAsleep)
            {
                Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().isPoisoned)
                {
                    Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<SpriteRenderer>().color = Color.green;
                }

                if (Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<Enemy>().isAsleep)
                {
                    Engine.e.battleSystem.enemies[Engine.e.battleSystem.target].GetComponent<SpriteRenderer>().color = Color.grey;
                }
            }

            Destroy(this.gameObject);

            Engine.e.battleSystem.dropExists = false;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(CheckDistance());
    }
}

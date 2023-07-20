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
    int enemyTarget = 0;

    // Start is called before the first frame update
    void Awake()
    {

        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR1TURN)
        {
            enemyTarget = Engine.e.battleSystem.char1Target;
        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR2TURN)
        {
            enemyTarget = Engine.e.battleSystem.char2Target;

        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR3TURN)
        {
            enemyTarget = Engine.e.battleSystem.char3Target;

        }

        Engine.e.battleSystem.animExists = true;

        /*if (Engine.e.timeOfDay < 300 || Engine.e.timeOfDay > 700)
        {
            GetComponent<Light2D>().intensity = 0.5f;
        }
        else
        {
            GetComponent<Light2D>().intensity = 1f;
        }*/
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

                // Change this value
                if (Engine.e.battleSystem.mpRestore)
                {
                }
            }
            GetComponent<ParticleSystem>().Emit(1);

            characterObjectSprite.color = GetComponent<SpriteRenderer>().color;

            yield return new WaitForSeconds(1.0f);

            Engine.e.battleSystem.mpRestore = false;
            Engine.e.battleSystem.animExists = false;


            Destroy(this.gameObject);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(CheckDistance());
    }
}


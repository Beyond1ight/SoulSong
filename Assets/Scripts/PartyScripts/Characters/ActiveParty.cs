using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveParty : MonoBehaviour
{

    public GameObject[] activeParty;
    public GameObject[] aPChar1ChoiceButtons;
    public GameObject[] aPChar2ChoiceButtons;
    public GameObject[] aPChar3ChoiceButtons;
    int choice1 = 0;
    int choice2 = 0;
    public Engine gameManager;
    public GameObject activePartyMember2;
    public GameObject activePartyMember3;
    public Vector3 posX;
    public Vector3 posY;

    public void SetActiveParty()
    {
        activeParty = new GameObject[3];

        try
        {
            for (int i = 0; i < activeParty.Length; i++)
            {
                for (int j = i; j < gameManager.party.Length; j++)
                {
                    if (gameManager.party[j] != null)
                    {
                        activeParty[j] = gameManager.party[j];
                        Engine.e.party[j].GetComponent<Character>().isInActiveParty = true;
                        if (activeParty[1] != null)
                        {
                            activePartyMember2.GetComponent<APFollow>().SetSprite(1);
                        }
                        if (activeParty[2] != null)
                        {
                            activePartyMember3.GetComponent<APFollow>().SetSprite(2);
                        }
                    }
                    break;
                }
            }
        }
        catch (System.InvalidOperationException)
        {
            throw;
        }
    }

    public void SetLeaderSprite()
    {
        if (this.GetComponent<SpriteRenderer>().sprite != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = activeParty[0].GetComponent<SpriteRenderer>().sprite;
            this.GetComponent<SpriteRenderer>().color = activeParty[0].GetComponent<SpriteRenderer>().color;

        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = activeParty[0].GetComponent<SpriteRenderer>().sprite;
            this.GetComponent<SpriteRenderer>().color = activeParty[0].GetComponent<SpriteRenderer>().color;
        }
    }

    public void SetActiveParty2Sprite()
    {
        if (Engine.e.activePartyMember2 != null)
        {
            Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().sprite = activeParty[1].GetComponent<SpriteRenderer>().sprite;
            Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().color = activeParty[1].GetComponent<SpriteRenderer>().color;
        }
        else
        {
            Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().sprite = activeParty[1].GetComponent<SpriteRenderer>().sprite;
            Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().color = activeParty[1].GetComponent<SpriteRenderer>().color;
        }
    }

    public void SetActiveParty3Sprite()
    {
        if (Engine.e.activePartyMember3 != null)
        {
            Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().sprite = activeParty[2].GetComponent<SpriteRenderer>().sprite;
            Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().color = activeParty[2].GetComponent<SpriteRenderer>().color;

        }
        else
        {
            Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().sprite = activeParty[2].GetComponent<SpriteRenderer>().sprite;
            Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().color = activeParty[2].GetComponent<SpriteRenderer>().color;

        }
    }



    public void ArrangeActiveParty()
    {
        for (int i = 0; i < 3; i++)
        {
            //activeParty[i].GetComponent<Character>().activePartyGO = null;
            activeParty[i].GetComponent<Character>().isInActiveParty = false;
        }

        activeParty = new GameObject[3];
        Engine.e.inBattle = true;
    }

    public void ArrangeActivePartyChar1(int char1)
    {
        activeParty[0] = gameManager.party[char1];
        //activeParty[char1].GetComponent<Character>().activePartyGO = Engine.e.activeParty.gameObject;
        Engine.e.party[char1].GetComponent<Character>().isInActiveParty = true;
        aPChar2ChoiceButtons[char1].SetActive(false);
        choice1 += char1;
    }

    public void ArrangeActivePartyChar2(int char2)
    {
        activeParty[1] = gameManager.party[char2];
        Engine.e.party[char2].GetComponent<Character>().isInActiveParty = true;
        aPChar3ChoiceButtons[choice1].SetActive(false);
        aPChar3ChoiceButtons[char2].SetActive(false);
        activePartyMember2.GetComponent<APFollow>().SetSprite(1);
        choice2 += char2;
    }

    public void ArrangeActivePartyChar3(int char3)
    {
        activeParty[2] = gameManager.party[char3];
        Engine.e.party[char3].GetComponent<Character>().isInActiveParty = true;
        aPChar2ChoiceButtons[choice1].SetActive(true);
        aPChar3ChoiceButtons[choice1].SetActive(true);
        aPChar3ChoiceButtons[choice2].SetActive(true);
        activePartyMember3.GetComponent<APFollow>().SetSprite(2);
        choice1 -= choice1;
        choice2 -= choice2;
        Engine.e.inBattle = false;

        SetActivePartyIndexes();
    }

    public void InstantiateActivePartyMembers()
    {
        if (activeParty[1] != null)
        {
            Instantiate(activeParty[1].gameObject, new Vector3(this.transform.position.x, this.transform.position.y - 2), Quaternion.identity);
        }
        if (activeParty[2] != null)
        {
            Instantiate(activeParty[2], new Vector3(this.transform.position.x, this.transform.position.y - 4), Quaternion.identity);
        }
    }

    public void InstantiateBattleLeader(int index)
    {
        activeParty[0].GetComponent<Character>().isInActiveParty = false;
        activeParty[0] = Engine.e.party[index];
        activeParty[0].GetComponent<Character>().isInActiveParty = true;
        SetLeaderSprite();
        activeParty[0].GetComponent<Character>().activePartyIndex = 0;
        Engine.e.battleSystem.enemyGroup.moveToPosition = true;
        Engine.e.activeParty.gameObject.transform.position = Engine.e.battleSystem.enemyGroup.char1SwitchPos.transform.position;
        if (Vector3.Distance(Engine.e.activeParty.transform.position, Engine.e.battleSystem.leaderPos) < 0.2)
        {
            Engine.e.activeParty.transform.position = Engine.e.battleSystem.leaderPos;
        }

        Engine.e.activePartyMember2.gameObject.transform.position = Engine.e.battleSystem.enemyGroup.GetComponent<Teleport>().activeParty2Location.transform.position;
        Engine.e.activePartyMember3.gameObject.transform.position = Engine.e.battleSystem.enemyGroup.GetComponent<Teleport>().activeParty3Location.transform.position;
    }
    public void InstantiateBattleActiveParty2(int index)
    {

        activeParty[1].GetComponent<Character>().isInActiveParty = false;
        activeParty[1] = Engine.e.party[index];
        activeParty[1].GetComponent<Character>().isInActiveParty = true;
        SetActiveParty2Sprite();
        activeParty[1].GetComponent<Character>().activePartyIndex = 1;
        Engine.e.battleSystem.enemyGroup.moveToPosition = true;
        Engine.e.activePartyMember2.gameObject.transform.position = Engine.e.battleSystem.enemyGroup.char2SwitchPos.transform.position;
        if (Vector3.Distance(Engine.e.activePartyMember2.transform.position, Engine.e.battleSystem.activeParty2Pos) < 0.2)
        {
            Engine.e.activePartyMember2.transform.position = Engine.e.battleSystem.activeParty2Pos;
        }


    }

    public void InstantiateBattleActiveParty3(int index)
    {

        activeParty[2].GetComponent<Character>().isInActiveParty = false;
        activeParty[2] = Engine.e.party[index];
        activeParty[2].GetComponent<Character>().isInActiveParty = true;
        SetActiveParty3Sprite();
        activeParty[2].GetComponent<Character>().activePartyIndex = 2;
        Engine.e.battleSystem.enemyGroup.moveToPosition = true;
        Engine.e.activePartyMember3.gameObject.transform.position = Engine.e.battleSystem.enemyGroup.char3SwitchPos.transform.position;
        if (Vector3.Distance(Engine.e.activePartyMember3.transform.position, Engine.e.battleSystem.activeParty3Pos) < 0.2)
        {
            Engine.e.activePartyMember3.transform.position = Engine.e.battleSystem.activeParty3Pos;
        }
    }

    // Be smart where you (me, so weird I need to get out more) call this. Pretty damn important for battles in particular
    public void SetActivePartyIndexes()
    {
        for (int i = 0; i < Engine.e.party.Length; i++)
        {
            if (Engine.e.party[i] != null)
            {
                if (Engine.e.party[i].GetComponent<Character>() == activeParty[0].GetComponent<Character>())
                {
                    Engine.e.party[i].GetComponent<Character>().activePartyIndex = 0;
                }
                else
                {
                    if (Engine.e.party[i].GetComponent<Character>() == activeParty[1].GetComponent<Character>())
                    {
                        Engine.e.party[i].GetComponent<Character>().activePartyIndex = 1;
                    }
                    else
                    {
                        if (Engine.e.party[i].GetComponent<Character>() == activeParty[2].GetComponent<Character>())
                        {
                            Engine.e.party[i].GetComponent<Character>().activePartyIndex = 2;
                        }
                        else
                        {
                            Engine.e.party[i].GetComponent<Character>().activePartyIndex = -1;
                        }
                    }
                }
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{

    public string[] enemyNames;
    public float[] enemyHealth;
    public int[] enemyMana;

    public TextMeshProUGUI[] displayNames;
    public TextMeshProUGUI[] displayHealth;
    public TextMeshProUGUI[] displayMaxHealth;

    public TextMeshProUGUI[] displayMana;
    public TextMeshProUGUI[] displayMaxMana;

    public TextMeshProUGUI[] displayEnergy;
    public TextMeshProUGUI[] displayMaxEnergy;

    public TextMeshProUGUI[] displayEnemyNames;
    public TextMeshProUGUI[] displayEnemyHealth;
    public TextMeshProUGUI[] displayEnemyMana;


    public void SetPlayerHUD()
    {
        string[] charNames = new string[Engine.e.activeParty.activeParty.Length];

        float[] charHealth = new float[Engine.e.activeParty.activeParty.Length];
        float[] charMaxHealth = new float[Engine.e.activeParty.activeParty.Length];

        float[] charMana = new float[Engine.e.activeParty.activeParty.Length];
        float[] charMaxMana = new float[Engine.e.activeParty.activeParty.Length];

        float[] charEnergy = new float[Engine.e.activeParty.activeParty.Length];
        float[] charMaxEnergy = new float[Engine.e.activeParty.activeParty.Length];


        for (int i = 0; i < charNames.Length; i++)
        {
            if (Engine.e.activeParty.activeParty[i] != null)
            {
                charNames[i] = Engine.e.activeParty.activeParty[i].gameObject.GetComponent<Character>().characterName;
                displayNames[i].text = charNames[i];

                charHealth[i] = Engine.e.activeParty.activeParty[i].gameObject.GetComponent<Character>().currentHealth;
                charMaxHealth[i] = Engine.e.activeParty.activeParty[i].gameObject.GetComponent<Character>().maxHealth;
                displayHealth[i].text = charHealth[i].ToString();
                displayMaxHealth[i].text = "\n / " + charMaxHealth[i].ToString();


                charMana[i] = Engine.e.activeParty.activeParty[i].gameObject.GetComponent<Character>().currentMana;
                charMaxMana[i] = Engine.e.activeParty.activeParty[i].gameObject.GetComponent<Character>().maxMana;
                displayMana[i].text = charMana[i].ToString();
                displayMaxMana[i].text = "\n / " + charMaxMana[i].ToString();


                charEnergy[i] = Engine.e.activeParty.activeParty[i].gameObject.GetComponent<Character>().currentEnergy;
                charMaxEnergy[i] = Engine.e.activeParty.activeParty[i].gameObject.GetComponent<Character>().maxEnergy;

                displayEnergy[i].text = charEnergy[i].ToString();
                displayMaxEnergy[i].text = "\n / " + charMaxEnergy[i].ToString();

            }
        }
    }

    public void SetEnemyGroupHUD()
    {

        enemyNames = new string[Engine.e.battleSystem.enemies.Length];
        enemyHealth = new float[Engine.e.battleSystem.enemies.Length];
        enemyMana = new int[Engine.e.battleSystem.enemies.Length];

        for (int i = 0; i < enemyNames.Length; i++)
        {
            if (Engine.e.battleSystem.enemies[i] != null)
            {
                enemyNames[i] = Engine.e.battleSystem.enemies[i].gameObject.GetComponent<Enemy>().enemyName;
                displayEnemyNames[i].text = enemyNames[i];

                enemyHealth[i] = Engine.e.battleSystem.enemies[i].GetComponent<Enemy>().health;
                displayEnemyHealth[i].text = enemyHealth[i].ToString();

            }
        }
    }

    public void SetEnemyHUD(Enemy enemy)
    {
        string[] enemyNames = new string[Engine.e.battleSystem.enemies.Length];
        float[] enemyHealth = new float[Engine.e.battleSystem.enemies.Length];
        int[] enemyMana = new int[Engine.e.battleSystem.enemies.Length];

        for (int i = 0; i < enemyNames.Length; i++)
        {
            if (Engine.e.battleSystem.enemies[i] != null)
            {
                enemyNames[i] = enemy.gameObject.GetComponent<Enemy>().enemyName;
                displayEnemyNames[i].text = enemyNames[i];

                enemyHealth[i] = enemy.gameObject.GetComponent<Enemy>().health;
                displayEnemyHealth[i].text = enemyHealth[i].ToString();



                // charMana[i] = GameManager.gameManager.activeParty.activeParty[i].gameObject.GetComponent<Character>().mana;
                //displayMana[i].text = charMana[i].ToString();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{

    public string currentScene;
    public Vector3 savePointPosition;

    /*public string CheckCurrentScene()
    {

        Scene _currentScene = SceneManager.GetActiveScene();
        return _currentScene.name;
    }*/

    public void SetSaveScene()
    {
        if (!Engine.e.inWorldMap)
        {

            savePointPosition = this.transform.position;
        }
        else
        {
            currentScene = "WorldMap";
            savePointPosition = Engine.e.activeParty.gameObject.transform.position;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Engine.e.ableToSave = true;

            for (int i = 0; i < Engine.e.party.Length; i++)
            {
                if (Engine.e.party[i] != null)
                {
                    if (Engine.e.party[i].GetComponent<Character>().weapon == null)
                    {
                        Engine.e.party[i].GetComponent<Character>().weapon = Engine.e.charEquippedWeapons[i].GetComponent<Item>();
                    }
                    if (Engine.e.party[i].GetComponent<Character>().chestArmor == null)
                    {
                        Engine.e.party[i].GetComponent<Character>().chestArmor = Engine.e.charEquippedChestArmor[i].GetComponent<ChestArmor>();
                    }
                }
            }
            if (Engine.e.currentScene != currentScene || Engine.e.currentScene == string.Empty)
            {
                SetSaveScene();
            }

            if (!Engine.e.inWorldMap)
            {
                for (int i = 0; i < Engine.e.party.Length; i++)
                {
                    if (Engine.e.party[i] != null)
                    {
                        Engine.e.party[i].GetComponent<Character>().currentHealth = Engine.e.party[i].GetComponent<Character>().maxHealth;
                        Engine.e.party[i].GetComponent<Character>().currentMana = Engine.e.party[i].GetComponent<Character>().maxMana;
                        Engine.e.party[i].GetComponent<Character>().currentEnergy = Engine.e.party[i].GetComponent<Character>().maxEnergy;

                        Engine.e.party[i].GetComponent<Character>().isPoisoned = false;
                        Engine.e.party[i].GetComponent<Character>().poisonDmg = 0;
                        Engine.e.party[i].GetComponent<Character>().isConfused = false;
                        Engine.e.party[i].GetComponent<Character>().confuseTimer = 0;
                        Engine.e.party[i].GetComponent<Character>().isAsleep = false;
                        Engine.e.party[i].GetComponent<Character>().sleepTimer = 0;
                        Engine.e.party[i].GetComponent<Character>().miterInflicted = false;
                        Engine.e.party[i].GetComponent<Character>().haltInflicted = false;
                        Engine.e.party[i].GetComponent<Character>().deathInflicted = false;


                    }
                }
            }
            //Debug.Log();

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // GameManager.gameManager.currentScene = "";
            Engine.e.ableToSave = false;
        }
    }
}
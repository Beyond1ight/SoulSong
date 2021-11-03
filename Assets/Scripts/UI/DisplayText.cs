using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DisplayText : MonoBehaviour
{
    //public Transform playerReference;
    public TextMeshProUGUI partyTextReference;


    public TextMeshProUGUI inventoryTextReference;


    public EnemyInformation info;
    public GameObject objectReference;


    //public void DisplayText(EnemyInformation enemy)
    //
    //objectReference = GameObject.Find("EnemyInformation");
    //textReference.text = objectReference.GetComponent<Character>().characterName;
    //}


    public void ClearPartyText()
    {
        partyTextReference.text = string.Empty;
    }
    public void ClearItemText()
    {
        inventoryTextReference.text = string.Empty;
    }



    public void DisplayItems()
    {
        // for (int i = 0; i < GameManager.gameManager.partyInventoryDisplay.Count; i++)
        {
            // inventoryTextReference.text += GameManager.gameManager.partyInventory[i].itemName + " " + GameManager.gameManager.partyInventory[i].numberHeld + "\n";
        }
    }

    void Update()
    {

        // textReference.text = enemyInfo.GetComponent<EnemyInformation>().informationName;

        //textReference.text = "x-pos: " + playerReference.position.x.ToString("0")
        // + "\n" + "y-pos: " + playerReference.position.y.ToString("0");

    }
}

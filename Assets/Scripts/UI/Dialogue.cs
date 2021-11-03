using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject endButton;
    public GameObject dialogueScreen;
    bool talking = false;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "ActiveParty" && GetComponent<NPC>().playerInRange && !Engine.e.inBattle)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Engine.e.interactionPopup.SetActive(false);
                talking = true;
                Engine.e.inBattle = true;
                textDisplay.text = string.Empty;
                dialogueScreen.SetActive(true);

                StartCoroutine(Type());
            }
        }
    }
    void Update()
    {
        if (talking)
        {
            if (textDisplay.text == sentences[index])
            {
                continueButton.SetActive(true);
            }
        }
        else
        {
            continueButton.SetActive(false);
        }
    }
    IEnumerator Type()
    {
        talking = false;
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;

            yield return new WaitForSeconds(typingSpeed);

        }
        if (index == sentences.Length - 1)
        {
            talking = false;
            endButton.SetActive(true);
            continueButton.SetActive(false);
        }
        else
            talking = true;
    }


    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = string.Empty;
            StartCoroutine(Type());
            continueButton.SetActive(false);
        }
    }

    public void EndDialogue()
    {
        textDisplay.text = string.Empty;
        textDisplay.text = sentences[index];
        index = 0;
        dialogueScreen.SetActive(false);
        endButton.SetActive(false); continueButton.SetActive(false);
        Engine.e.inBattle = false;
        //GameManager.gameManager.AddCharacterToParty("Field");
    }
}


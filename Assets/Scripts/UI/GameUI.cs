using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public Animator menuTransition;

    public void PlayTransition()
    {
        StartCoroutine(StartTransition());
    }
    public IEnumerator StartTransition()
    {
        menuTransition.gameObject.SetActive(true);
        menuTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        menuTransition.gameObject.SetActive(false);

    }
}

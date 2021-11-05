using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleMenuButton : MonoBehaviour
{
    public void TargetedTextWhite()
    {
        if (GetComponentInChildren<TMP_Text>().color != Color.white)
        {
            GetComponentInChildren<TMP_Text>().color = Color.white;
        }
    }

    public void TargetedTextGray()
    {
        if (GetComponentInChildren<TMP_Text>().color != Color.gray)
        {
            GetComponentInChildren<TMP_Text>().color = Color.gray;
        }
    }
}

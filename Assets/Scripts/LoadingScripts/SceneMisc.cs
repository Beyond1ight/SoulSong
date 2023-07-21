using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMisc : MonoBehaviour
{
    public GameObject[] lockedObjects, activationLights;

    public void SceneManagement()
    {

        if (Engine.e.daylight)
        {   // Daytime
            for (int i = 0; i < lockedObjects.Length; i++)
            {
                if (lockedObjects[i] != null)
                {
                    lockedObjects[i].SetActive(true);
                }
            }

            for (int i = 0; i < activationLights.Length; i++)
            {
                if (activationLights[i] != null)
                {
                    activationLights[i].SetActive(false);
                }
            }
        }
        else // Nighttime
        {
            for (int i = 0; i < lockedObjects.Length; i++)
            {
                if (lockedObjects[i] != null)
                {
                    lockedObjects[i].SetActive(false);
                }
            }

            for (int i = 0; i < activationLights.Length; i++)
            {
                if (activationLights[i] != null)
                {
                    activationLights[i].SetActive(true);
                }
            }
        }
    }

    void FixedUpdate()
    {
        SceneManagement();
    }

}

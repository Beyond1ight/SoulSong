using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private Gradient lightColor;
    [SerializeField] private GameObject lighting;

    public GameObject[] lockedDoors;
    public GameObject[] landLightSources;
    public bool daylight;

    private void Update()
    {
        if ((int)Engine.e.timeOfDay >= 400 && (int)Engine.e.timeOfDay <= 650)
        {
            if (daylight == true)
            {
                daylight = false;
            }

            foreach (var door in lockedDoors)
            {
                if (door != null && door.activeInHierarchy == true)
                {
                    door.SetActive(false);
                }
            }
            foreach (var lightSources in landLightSources)
            {
                if (lightSources != null && lightSources.activeInHierarchy == false)
                {
                    lightSources.gameObject.GetComponent<Light2D>().intensity = 1;
                    lightSources.SetActive(true);
                }
            }
        }

        if (Engine.e.timeOfDay > 645)
        {
            daylight = true;
            foreach (var door in lockedDoors)
            {
                if (door != null && door.activeInHierarchy == false)
                {
                    door.SetActive(true);
                }
            }
            foreach (var lightSources in landLightSources)
            {
                if (lightSources != null && lightSources.activeInHierarchy == true && lightSources.gameObject.GetComponent<Light2D>().intensity > 0)
                    lightSources.gameObject.GetComponent<Light2D>().intensity -= 0.001f;
            }

            if (Engine.e.timeOfDay > 700)
            {
                foreach (var lightSources in landLightSources)
                {
                    if (lightSources != null && lightSources.activeInHierarchy == true)
                    {
                        lightSources.SetActive(false);
                    }
                }
            }

            if (Engine.e.timeOfDay > 900)
            {
                Engine.e.timeOfDay = 0;
            }
        }

        // Rain 
        /*if (GameManager.gameManager.weatherRainOn == true)
        {
            if (GameManager.gameManager.indoors)
            {
                GameManager.gameManager.weatherRain.SetActive(false);
            }
            else
            {
                GameManager.gameManager.weatherRain.SetActive(true);
            }
        }*/

        lighting.GetComponent<Light2D>().color = lightColor.Evaluate(Engine.e.timeOfDay * 0.001f);
    }

    private void FixedUpdate()
    {
        if (Engine.e.timeOfDay == 0)
        {
            daylight = true;
        }

        // Rain
        /* if (GameManager.gameManager.weatherRainOn == false)
         {
             if (GameManager.gameManager.startTimer == true && GameManager.gameManager.stopTimer == false)
             {
                 GameManager.gameManager.rainChance = Random.Range(30000, 50000);
                 GameManager.gameManager.startTimer = false;
             }

             GameManager.gameManager.rainTimer++;

             if (GameManager.gameManager.rainTimer >= GameManager.gameManager.rainChance)
             {
                 GameManager.gameManager.weatherRainOn = true;
                 GameManager.gameManager.stopTimer = true;
                 GameManager.gameManager.rainTimer = 0;
                 GameManager.gameManager.weatherRain.SetActive(true);
                 GetComponent<Light2D>().intensity -= 0.1f;


             }
         }

         if (GameManager.gameManager.weatherRainOn == true)
         {
             if (GameManager.gameManager.startTimer == false && GameManager.gameManager.stopTimer == true)
             {
                 GameManager.gameManager.rainOff = Random.Range(2000, 10000);
                 GameManager.gameManager.stopTimer = false;
             }

             GameManager.gameManager.rainTimer++;

             if (GameManager.gameManager.rainTimer >= GameManager.gameManager.rainOff)
             {
                 GameManager.gameManager.weatherRainOn = false;
                 GameManager.gameManager.startTimer = true;
                 GameManager.gameManager.rainTimer = 0;
                 GameManager.gameManager.weatherRain.SetActive(false);
                 GetComponent<Light2D>().intensity += 0.1f;

             }
         }
        */

    }
}


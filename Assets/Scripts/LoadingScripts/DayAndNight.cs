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
    public bool daylight, am;

    public int hour, militaryHour, minute;
    public float timeOfDay = 0.5f, timer = 0.5f;



    /*if (Engine.e.timeOfDay < 50)
    {
        Engine.e.recentAutoSave = false;
    }
    if (Engine.e.timeOfDay > 50 && !Engine.e.recentAutoSave && !Engine.e.autoSaveReady && !Engine.e.inBattle)
    {
        Engine.e.autoSaveReady = true;
    }

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

}
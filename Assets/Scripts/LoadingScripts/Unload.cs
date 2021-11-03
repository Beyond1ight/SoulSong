using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unload : MonoBehaviour
{
    public string scene;
    public bool unloaded;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !unloaded)
        {
            unloaded = true;
            Engine.e.UnloadScene(scene);
        }
    }
}

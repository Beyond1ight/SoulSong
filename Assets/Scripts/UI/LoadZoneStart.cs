using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadZoneStart : MonoBehaviour
{
    public void StartUnloadForAnim()
    {
        Load.l.SceneUnload();
    }

    public void StartLoadForAnim()
    {
        Load.l.SceneLoadManager();
    }
}

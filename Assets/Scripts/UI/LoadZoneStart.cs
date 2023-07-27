using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void StartLoadIntoGameForAnim()
    {
        if (Engine.e.gameStart)
        {
            GameObject startingPos = GameObject.FindGameObjectWithTag("NewGameSpawnLocation");
            SceneManager.UnloadSceneAsync("GrieveNameInput");
            //Engine.e.SceneLoadManageOnStartup("OpeningCutscene", false, false, false, false, startingPos.transform.position.x, startingPos.transform.position.y);
            Engine.e.SceneLoadManageOnStartup("Sturgeon", false, false, false, true, startingPos.transform.position.x, startingPos.transform.position.y);
        }
        else
        {
            if (Engine.e.loading)
            {
                Engine.e.LoadGame(Engine.e.loadFileReference);
            }
            else
            {
                Engine.e.SceneLoadManageOnStartup("GrieveNameInput", false, false, false, false, 0f, 0f);
            }
        }
    }

    public void GiveBackControl()
    {
        Engine.e.inBattle = false;
    }
}

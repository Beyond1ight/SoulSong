using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneTrigger : MonoBehaviour
{

    public PlayableDirector director;
    public TimelineAsset tlAsset;

    void Start()
    {
        FindEventReferences();
    }

    public void FindEventReferences()
    {
        GameObject activeParty = GameObject.Find("ActiveParty");
        director.SetGenericBinding(tlAsset.GetOutputTrack(1), activeParty.GetComponent<Animator>());
    }
}

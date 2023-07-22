using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneTrigger : MonoBehaviour
{

    public PlayableDirector director;
    public TimelineAsset tlAsset;
    public bool oneTimeCutscene;
    void Start()
    {
        director = this.gameObject.GetComponent<PlayableDirector>();
        //tlAsset = this.gameObject.GetComponent<PlayableDirector>().
        FindEventReferences();
    }

    public void FindEventReferences()
    {
        GameObject activeParty = GameObject.Find("ActiveParty");
        director.SetGenericBinding(tlAsset.GetOutputTrack(1), activeParty.GetComponent<Animator>());
    }
}

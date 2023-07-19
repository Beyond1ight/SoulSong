using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class DialogueMarker : Marker, INotification, INotificationOptionProvider
{

    [SerializeField] private bool retroactive = false;
    [SerializeField] private bool emitOnce = false;
    [SerializeField] private bool continueConversation = false;

    public PropertyName id => new PropertyName();
    public NotificationFlags flags =>
    (retroactive ? NotificationFlags.Retroactive : default) |
    (emitOnce ? NotificationFlags.TriggerOnce : default) |
    (continueConversation ? NotificationFlags.TriggerOnce : default);

}

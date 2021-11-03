using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraCollisionSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Engine.e.mainCamera.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GetComponent<PolygonCollider2D>();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APFollow : MonoBehaviour
{
    public float speed;
    public GameObject aP;
    private Transform target;
    public float distance;

    void Start()
    {
        target = aP.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (!Engine.e.inBattle)
        {
            if (Vector3.Distance(transform.position, target.position) > distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
        }
    }

    public void SetSprite(int index)
    {
        if (Engine.e.activeParty.activeParty[index] != null)
            GetComponent<SpriteRenderer>().sprite = Engine.e.activeParty.activeParty[index].GetComponent<SpriteRenderer>().sprite;
    }

}

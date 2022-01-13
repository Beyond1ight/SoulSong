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
            //speed = Engine.e.activeParty.GetComponent<PlayerController>().speed;
            if (Vector3.Distance(transform.position, target.position) > distance)
            {
                GetComponent<Rigidbody2D>().velocity = (aP.transform.position - transform.position).normalized * speed;
                //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }
    }

    public void SetSprite(int index)
    {
        if (Engine.e.activeParty.activeParty[index] != null)
            GetComponent<SpriteRenderer>().sprite = Engine.e.activeParty.activeParty[index].GetComponent<SpriteRenderer>().sprite;
    }

}

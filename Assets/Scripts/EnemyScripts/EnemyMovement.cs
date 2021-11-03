using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;
    public GameObject enemy;
    public Rigidbody2D rb;
    Vector2 movement;
    public float speed = 0f;

    void FixedUpdate()
    {
        // Debug.Log(rb.position.x);
        if (!Engine.e.inBattle)
        {
            CheckDistance();
        }
        else
        {
            if (GetComponentInParent<EnemyGroup>().groupInBattle == true)
            {
                CheckDistance();
            }
            //rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    void ChangeGoal()
    {
        if (currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }

    public void CheckDistance()
    {
        if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, speed * Time.deltaTime);
            rb.MovePosition(temp);
        }
        else
        {
            ChangeGoal();
        }
    }
    void Update()
    {
    }
}

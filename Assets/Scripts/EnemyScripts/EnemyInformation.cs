using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInformation : MonoBehaviour
{

    public string informationName;
    public float informationHealth;
    public int informationLvl;
    public int informationDmg;
    public Transform informationPos;
    public Vector3 informationVector;
    public bool informationIsDead;
    public string informationWorldZone;
    public Enemy enemy;
    public GameObject enemyObject;
    public bool toCreateInformation = false;
    // public DisplayTextTest textTest;
    void Awake()
    {

    }

    public void CreateInformation(Enemy _enemy)
    {

        Debug.Log("Creating Information...");

        informationName = _enemy.enemyName;
        informationHealth = _enemy.currentHealth;
        informationLvl = _enemy.lvl;
        informationDmg = _enemy.damage;
        informationPos = _enemy.enemyPos;
        informationIsDead = true;
        informationWorldZone = _enemy.worldZone;
        enemyObject = enemyObject.gameObject;

        //informationVector = _enemy.transform.position;

        Instantiate(this, informationPos);

        //Destroy(_enemy.gameObject);

        //if (informationIsDead == false)
        // {
        DontDestroyOnLoad(this);
        // }
        //else
        //    Debug.Log("Destroying Information.");
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(informationName + " " + informationHealth + " xPos: " + informationPos.position.x
            + " yPos: " + informationPos.position.y + " " + informationIsDead);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(this.gameObject, informationVector, Quaternion.identity);
            }

        }
    }

    public void SetIsDead(bool isDead)
    {
        informationIsDead = isDead;

    }
}


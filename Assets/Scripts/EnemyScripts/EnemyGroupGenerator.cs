using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupGenerator : MonoBehaviour
{
    public Enemy[] listOfEnemies; // List of enemies available to choose from
    public GameObject[] enemyLocations;
    public EnemyGroup enemyGroup;

    void Start()
    {
        enemyGroup = GetComponent<EnemyGroup>();
    }

    public void GenerateEnemyGroup()
    {
        if (enemyGroup.spawnChance != 0)
        {
            int deSpawn = Random.Range(0, 100);

            if (deSpawn > enemyGroup.spawnChance)
            {
                enemyGroup.DespawnGroup();
            }

            enemyGroup.inWorld = true;
        }
        else
        {
            enemyGroup.DespawnGroup();
        }

        if (enemyGroup.inWorld)
        {
            int numberOfEnemies = Random.Range(1, 5); // Generating number of enemies

            enemyGroup.enemies = new Enemy[numberOfEnemies];
            int enemy1 = Random.Range(0, listOfEnemies.Length); // If the group is "inWorld," there will always be at least one enemy. This picks the first enemy

            GameObject enemy1Obj = Instantiate(listOfEnemies[enemy1].gameObject, enemyLocations[0].transform.position, Quaternion.identity); // Instantiates the GameObject of the first enemy
            enemy1Obj.transform.parent = enemyGroup.transform; // Sets the instantiated enemy1's parent to the enemyGroup, mostly for organization
            enemyGroup.enemies[0] = enemy1Obj.GetComponent<Enemy>(); // Sets the enemyGroup's enemy1 to be associated with the instantiated enemy1, not to be confused with the prefab

            if (numberOfEnemies == 4)
            {
                int enemy4 = Random.Range(0, listOfEnemies.Length);
                int enemy3 = Random.Range(0, listOfEnemies.Length);
                int enemy2 = Random.Range(0, listOfEnemies.Length);

                GameObject enemy4Obj = Instantiate(listOfEnemies[enemy4].gameObject, enemyLocations[3].transform.position, Quaternion.identity);
                enemy4Obj.transform.parent = enemyGroup.transform;
                enemyGroup.enemies[3] = enemy4Obj.GetComponent<Enemy>();

                GameObject enemy3Obj = Instantiate(listOfEnemies[enemy3].gameObject, enemyLocations[2].transform.position, Quaternion.identity);
                enemy3Obj.transform.parent = enemyGroup.transform;
                enemyGroup.enemies[2] = enemy3Obj.GetComponent<Enemy>();

                GameObject enemy2Obj = Instantiate(listOfEnemies[enemy2].gameObject, enemyLocations[1].transform.position, Quaternion.identity);
                enemy2Obj.transform.parent = enemyGroup.transform;
                enemyGroup.enemies[1] = enemy2Obj.GetComponent<Enemy>();

            }

            if (numberOfEnemies == 3)
            {
                int enemy3 = Random.Range(0, listOfEnemies.Length);
                int enemy2 = Random.Range(0, listOfEnemies.Length);

                GameObject enemy3Obj = Instantiate(listOfEnemies[enemy3].gameObject, enemyLocations[2].transform.position, Quaternion.identity);
                enemy3Obj.transform.parent = enemyGroup.transform;
                enemyGroup.enemies[2] = enemy3Obj.GetComponent<Enemy>();

                GameObject enemy2Obj = Instantiate(listOfEnemies[enemy2].gameObject, enemyLocations[1].transform.position, Quaternion.identity);
                enemy2Obj.transform.parent = enemyGroup.transform;
                enemyGroup.enemies[1] = enemy2Obj.GetComponent<Enemy>();
            }

            if (numberOfEnemies == 2)
            {
                int enemy2 = Random.Range(0, listOfEnemies.Length);

                GameObject enemy2Obj = Instantiate(listOfEnemies[enemy2].gameObject, enemyLocations[1].transform.position, Quaternion.identity);
                enemy2Obj.transform.parent = enemyGroup.transform;
                enemyGroup.enemies[1] = enemy2Obj.GetComponent<Enemy>();

            }
        }
    }
}

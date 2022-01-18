using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class AnimationMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    GameObject spawnLoc, targetLoc;

    public void StartMovement(GameObject _spawnLoc, GameObject _targetLoc)
    {
        spawnLoc = _spawnLoc;
        targetLoc = _targetLoc;

        this.gameObject.SetActive(true);

        StartCoroutine(CheckDistance());
    }

    // Start is called before the first frame update
    public IEnumerator CheckDistance()
    {
        //Vector3 targetPos = Vector3.MoveTowards(transform.position, GameManager.gameManager.battleSystem.leaderPos, 4 * Time.deltaTime);
        Vector3 targetPos = Vector3.MoveTowards(transform.position, targetLoc.transform.position, 5f * Time.deltaTime);
        rb.MovePosition(targetPos);

        //if (Vector3.Distance(rb.transform.position, GameManager.gameManager.battleSystem.leaderPos) < 0.1)
        if (Vector3.Distance(rb.transform.position, targetLoc.transform.position) < 0.1)
        {
            GetComponent<Animator>().Play("Start");

            yield return new WaitForSeconds(0.1f);

            this.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(CheckDistance());
    }
}
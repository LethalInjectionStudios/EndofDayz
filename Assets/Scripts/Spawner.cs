using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    private float spawnTmr = 0;
    private float spawnTmrMin = 2f;
    private float spawnTmrMax = 4;
    private float speedMul = 1;
    private float speedMulTmr = 5f;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        spawnTmr -= Time.deltaTime;
        speedMulTmr -= Time.deltaTime;

        if(speedMulTmr <= 0)
        {
            speedMulTmr = 5f;
            speedMul += .2f;
        }

        if(spawnTmr <= 0 && !player.GetComponent<PlayerController>().isDead)
        {
            spawnTmr = Random.Range(spawnTmrMin, spawnTmrMax);
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject zed;

        if (this.tag == "Horizontal")
        {
            zed = (GameObject)Instantiate(enemy, new Vector2(transform.position.x + Random.Range(-30, 30), transform.position.y), Quaternion.identity);
            zed.GetComponent<AIPath>().maxSpeed = 5 * speedMul;
        }

        if (this.tag == "Vertical")
        {
            zed = (GameObject)Instantiate(enemy, new Vector2(transform.position.x, transform.position.y + Random.Range(-20, 20)), Quaternion.identity);
            zed.GetComponent<AIPath>().maxSpeed = 5 * speedMul;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    private float spawnTmr = 0;
    private float spawnTmrMin = 1f;
    private float spawnTmrMax = 3;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTmr -= Time.deltaTime;

        if(spawnTmr <= 0 && !player.GetComponent<PlayerController>().isDead)
        {
            spawnTmr = Random.Range(spawnTmrMin, spawnTmrMax);
            Spawn();
        }
    }

    private void Spawn()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}

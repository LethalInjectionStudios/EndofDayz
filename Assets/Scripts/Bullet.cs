using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float moveSpeed = 50f;
    private Vector3 direction;
    private float ttd = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        direction = Vector3.right;        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * moveSpeed);

        ttd -= Time.deltaTime;
        if (ttd <= 0)
            Destroy(gameObject);        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().Die();
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float moveSpeed = 25f;
    private Vector3 direction;
    private float ttd = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(mousePos.x > player.transform.position.x)
        {
            direction = Vector3.right;
        }
        else
        {
            direction = Vector3.left;
            Vector3 ltemp = transform.localScale;
            ltemp.x *= -1;
            transform.localScale = ltemp;
        }
          
        
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


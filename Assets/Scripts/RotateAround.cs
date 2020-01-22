using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    private bool facingRight = true;

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Debug.Log("X: " + direction.x + " Y: " + direction.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);

        if (direction.x > transform.position.x && !facingRight)
            Flip();
        else if (direction.x < transform.position.x && facingRight)
            Flip();

        transform.position = player.transform.position;
    }

    private void Flip()
    {
        facingRight = !facingRight;;
        GetComponentInChildren<SpriteRenderer>().flipY = !GetComponentInChildren<SpriteRenderer>().flipY;
        //GetComponentInChildren<SpriteRenderer>().flipX = !GetComponentInChildren<SpriteRenderer>().flipX;
    }
}

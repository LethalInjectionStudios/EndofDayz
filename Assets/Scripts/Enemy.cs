using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f;
    private GameObject target;
    private Animator anim;
    private bool facingRight = true;
    private bool isDead = false;
    private float ttd = 2.5f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (isDead)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            ttd -= Time.deltaTime;
            if (ttd <= 0)
                Destroy(gameObject);
            return;
        }

        if (target.GetComponent<PlayerController>().isDead)
        {
            speed = 0f;
            anim.SetBool("isRunning", false);
            return;
        }

        anim.SetBool("isRunning", true);
        float step = speed * Time.deltaTime;

        if (target.transform.position.x > transform.position.x && !facingRight)
            Flip();
        else if (target.transform.position.x < transform.position.x && facingRight)
            Flip();

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);

        transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 ltemp = transform.localScale;
        ltemp.x *= -1;
        transform.localScale = ltemp;
    }

    public void Die()
    {
        speed = 0f;
        isDead = true;
        anim.SetBool("isDead", true);
        anim.SetTrigger("Dead");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Score();
        //Destroy(gameObject);
    }
}
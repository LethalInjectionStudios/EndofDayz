using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    private AIDestinationSetter destinationSetter;
    private AIPath aiPath;
    private GameObject target;
    private Animator anim;
    new private AudioSource audio;
    private bool facingRight = true;
    private bool isDead = false;
    private float ttd = 2.5f;
    private bool deathAudioPlayed = false;
    GameObject[] boundaries;
    //private float baseSpeed = 5f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        audio = GetComponent<AudioSource>();
        aiPath = GetComponent<AIPath>();
        destinationSetter.target = target.transform;
        boundaries = GameObject.FindGameObjectsWithTag("Boundary");
        //aiPath.maxSpeed = baseSpeed;

        foreach(GameObject boundary in boundaries)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), boundary.GetComponent<BoxCollider2D>());
        }
    }

    void Update()
    {

        if (isDead)
        {
            if (!deathAudioPlayed)
            {
                audio.Play();
                deathAudioPlayed = true;
            }

                GetComponent<BoxCollider2D>().enabled = false;
            ttd -= Time.deltaTime;
            if (ttd <= 0)
                Destroy(gameObject);
            return;
        }

        if (target.GetComponent<PlayerController>().isDead)
        {
            aiPath.maxSpeed = 0f;
            anim.SetBool("isRunning", false);
            return;
        }

        anim.SetBool("isRunning", true);

        if (target.transform.position.x > transform.position.x && !facingRight)
            Flip();
        else if (target.transform.position.x < transform.position.x && facingRight)
            Flip();
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
        aiPath.maxSpeed = 0f;
        isDead = true;
        anim.SetBool("isDead", true);
        anim.SetTrigger("Dead");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Score();
    }
}
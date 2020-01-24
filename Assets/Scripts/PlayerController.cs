﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private float moveSpeed = 10f;
    private bool facingRight = true;
    public bool isDead = false;
    private AudioSource audio;

    public int score = 0;
    public Text scoreText;
    public Text gameOver;

    private float time = 5f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        audio.enabled = true;
        scoreText.text = "Score: " + score.ToString();
        gameOver.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            gameOver.enabled = true;

            if(Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene("Menu");
        }
        Vector2 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float horizontal = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        Vector2 movement = new Vector2(horizontal, vertical);
        //movement.Normalize();

        if(horizontal != 0 || vertical != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
            PlayAudio();
        }

        if (mousePositon.x > transform.position.x && !facingRight)
            Flip();
        else if (mousePositon.x < transform.position.x && facingRight)
            Flip();

        transform.Translate(movement);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Die();
        }
    }

    private void PlayAudio()
    {
        audio.Play();
    }

    private void Flip()
    {
        if (!isDead)
        {
            facingRight = !facingRight;
            Vector3 ltemp = transform.localScale;
            ltemp.x *= -1;
            transform.localScale = ltemp;
        }
    }

    private void Die()
    {
        moveSpeed = 0f;
        isDead = true;
        anim.SetBool("isDead", true);
        anim.SetTrigger("Dead");
    }

    public void Score()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private AudioSource audio;
    private float moveSpeed = 10f;
    private bool facingRight = true;
    public bool isDead = false;
    
    private int score = 0;
    public Text scoreText;
    public Image gameOver;
    public AudioClip death;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        audio.enabled = true;
        scoreText.text = "Score: " + score.ToString();
        gameOver.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            gameOver.gameObject.SetActive(true);
        }

        Vector2 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float horizontal = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        Vector2 movement = new Vector2(horizontal, vertical);

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
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        moveSpeed = 0f;
        isDead = true;
        audio.PlayOneShot(death, 1);
        anim.SetBool("isDead", true);
        anim.SetTrigger("Dead");
    }

    public void Score()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }
}

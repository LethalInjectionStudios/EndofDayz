using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject muzzle;
    public GameObject flash;
    public GameObject hand;
    private float flashTmr;
    private Animator anim;
    private AudioSource audio;
    private GameObject player;

    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = hand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = hand.transform.position;

        if (Input.GetMouseButtonDown(0) && !player.GetComponent<PlayerController>().isDead)
        {
            anim.SetTrigger("Fire");
            audio.Play();
            GameObject projectile = (GameObject)Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation);
            flash.SetActive(true);
            flashTmr = .1f;
        }

        flashTmr -= Time.deltaTime;
        if (flashTmr <= 0)
            flash.SetActive(false);
    }
}


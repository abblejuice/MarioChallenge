using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Animator animator;

    private bool facingRight = true;

    public float speed;
    public float jumpforce;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    private AudioSource jumpAudio;
    private AudioSource coinAudio;
    private AudioSource powerupAudio;

    private bool finished = false;


    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        var audio = GetComponents<AudioSource>();
        jumpAudio = audio[0];
        powerupAudio = audio[1];
        coinAudio = audio[2];
    }


    void Awake()
    {
    }

    private void Update()
    {
       
    }

    
    // Update is called once per frame
    void FixedUpdate ()
    {
        if (finished)
            return;


        if (transform.position.x > 192)
        {
            finished = true;
            powerupAudio.Play();
        }

        float moveHorizontal = Input.GetAxis("Horizontal");

        //Vector2 movement = new Vector2(moveHorizontal, 0);

       // rb2d.AddForce(movement * speed);

        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        //Debug.Log(isOnGround);


        animator.SetBool("Airborne", !isOnGround);
        //animator.SetFloat("VelocityX", Mathf.Abs(moveHorizontal));
        animator.SetFloat("VelocityX", Mathf.Abs(rb2d.velocity.x));



        //stuff I added to flip my character
        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }


	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.velocity = Vector2.up * jumpforce;
                jumpAudio.Play();
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            coinAudio.Play();
        }
    }
}

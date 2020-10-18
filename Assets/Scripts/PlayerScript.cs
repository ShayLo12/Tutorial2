using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text scoreText;
    private int scoreValue = 0;
    public Text winText;
    private int livesValue = 3;
    public Text loseText;
    public Text livesText;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    Animator anim;
    private bool facingRight = true;
    private int lives;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreText.text = scoreValue.ToString();
        scoreText.text = "Score: 0";
        winText.text = "";
        livesText.text = "Lives: 3";
        anim = GetComponent<Animator>();
        
        livesValue = 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
            else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
          anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
          anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
          anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
          anim.SetInteger("State", 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
          anim.SetInteger("State", 2);
        }

        if (Input.GetKeyDown("escape"))
            {
               Application.Quit();
            }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            scoreText.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 5)
            {
                transform.position = new Vector3(50f, 0f, 0.0f);
                livesText.text = "Lives: 3";
                livesValue = 3;
            }
            if (scoreValue >= 10)
            {
                winText.text = "You did the win! Created by Shay Czachowski";
                musicSource.clip = musicClipOne;
                musicSource.Play();
            }
        }
    
        if (collision.collider.tag == "Enemy")
            {
                livesValue = livesValue - 1;
                livesText.text = "Lives: " + livesValue.ToString();
                collision.gameObject.SetActive (false);
                Destroy(collision.collider.gameObject);
            }

        if (livesValue == 0)
            {
                loseText.text = "Game Over! Created by Shay Czachowski";
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                Destroy (gameObject);
            }
        
        if (collision.gameObject.CompareTag("Ground"))
            {
                anim.SetInteger("State", 0);
            }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}
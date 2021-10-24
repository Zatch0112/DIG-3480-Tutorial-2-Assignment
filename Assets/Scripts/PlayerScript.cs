using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI livesText;
    private int count;
    private int lives;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        SetCountText();
        winText.text = "";
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            Destroy(collision.collider.gameObject);
            count = count + 1;
            SetCountText();
        }
        else if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            lives = lives - 1;
            SetCountText();
        }
        if (count == 4)
        {
            transform.position = new Vector2(57.0f, 1.0f);
            lives = 3;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count:" + count.ToString();
        livesText.text = "Lives:" + lives.ToString();
        if (count >= 8)
        {
            Destroy(this);
            winText.text = "You Win!\nGame Created By Zach Podaril";
        }
        if (lives <= 0)
        {
            Destroy(this);
            winText.text = "You Lose!\nGame Created By Zach Podaril";
        }
    }
}

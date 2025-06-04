using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    [SerializeField] Sprite spriteup;
    [SerializeField] Sprite spritedown;
    [SerializeField] Sprite spriteleft;
    [SerializeField] Sprite spriteright;

    Rigidbody2D rb;
    SpriteRenderer sR;

    Vector2 input;
    Vector2 velocity;

    [SerializeField] TextMeshProUGUI ScoreText;

    int score = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    // Update is called once per frame
    private void Update()
    {
        input.x = UnityEngine.Input.GetAxisRaw("Horizontal");
        input.y = UnityEngine.Input.GetAxisRaw("Vertical");

        velocity = input.normalized * moveSpeed;

        if (input.sqrMagnitude > .01f)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                if (input.x > 0)
                    sR.sprite = spriteright;
                else if (input.x < 0)
                    sR.sprite = spriteleft;
            }
            else
            {
                if (input.y > 0)
                    sR.sprite = spriteup;
                else
                    sR.sprite = spritedown;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            score += collision.GetComponent<ItemObject>().GetPoint();
            Destroy(collision.gameObject);
            ScoreText.text = "Score : " + score.ToString();
        }
    }
}

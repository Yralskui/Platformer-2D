using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // скорость
    public float speed;
    // сила прыжка
    public float jumpForce;
    // компонент физика
    private Rigidbody2D rb;
    // управление
    private Vector3 input;
    // проверка прыгает ли персонаж
    private bool isJumping=false;
    // счет монет
    public int coins;
    // жизни
    public int hp = 3;
    //Анимации
    Animator animations;
    public bool animationisJump;
    public bool animationisMove;
    // Стрельба
    public Transform shootPosition;
    public GameObject bomb;

    //Счет
    public Text coinsText;
    //Жизни
    public Image[] hearts;
    public Sprite isLife, nonLife;

    // Звуки
    public Audio soundEffector;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animations = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Flip();
        Jump();
        // Если позиция меньше -30 по y, то перезапускаем сцену
        if (transform.position.y < -30)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Shoot();
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hp > i)
            {
                hearts[i].sprite = isLife;
            }
            else
            {
                hearts[i].sprite = nonLife;
            }
        }
    }

    private void Move()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.position += input * speed * Time.deltaTime;
        if (input.x != 0 && !isJumping)
        {
            animations.SetBool("animationisMove", true);
            animations.SetBool("animationisJump", false);
        }
        else
        {
            animations.SetBool("animationisMove", false);
        }
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump")&&!isJumping)
        {
            soundEffector.JumpSound();
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            animations.SetBool("animationisJump", true);
            animations.SetBool("animationisMove", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animations.SetBool("animationisJump", false);
            animations.SetBool("animationisMove", false);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            hp--;
            Debug.Log(hp);
            if (hp == 0)
            {
                GetComponent<CapsuleCollider2D>().enabled = false;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            soundEffector.CoinSound();
            coins++;
            //Debug.Log(coins);
            coinsText.text = coins.ToString();
            Destroy(collision.gameObject);
        }
    }

    void Shoot()
    {
        Instantiate(bomb, shootPosition.position, shootPosition.rotation);
    }
}

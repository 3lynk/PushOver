using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame1Player : MonoBehaviour
{
    public bool playing;
    public float moveSpeed, jumpForce, smoothing;
    public Transform GroundCheck1;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    Animator anim;
    float horizontalMove, verticalMove;
    Vector2 vel = Vector2.zero;
    bool jump, onGround, facingLeft;
    [SerializeField] Minigame1UI ui;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start() {
        StartMinigame();
    }

    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        onGround = Physics2D.OverlapCircle(GroundCheck1.position, 0.15f, groundLayer);
    }

    void StartMinigame() {
        transform.position = new Vector3(0, -3.5f, 0);
        playing = true;
        facingLeft = true;
    }

    private void FixedUpdate() {
        if (!playing)
            return;
        //좌우 움직임
        Vector2 targetVelocity = new Vector2(horizontalMove * Time.deltaTime * moveSpeed * 10, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref vel, smoothing);
        //점프
        if (jump && onGround) {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jump = false;
        }

        //애니메이션
        if (!onGround ||(horizontalMove <= 0.1f && horizontalMove >= -0.1f)) {
            anim.Play("idle");
        } else {
            anim.Play("run");
        }
        if (facingLeft && horizontalMove > 0.1f) {
            facingLeft = false;
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y, transform.localScale.z);
        } else if (!facingLeft && horizontalMove < -0.1f) {
            facingLeft = true;
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("death") && playing) {
            Debug.Log("gameover");
            playing = false;
            ui.GameFail();
        }
        if (col.CompareTag("clear")) {
            Debug.Log("clear");
            GameManager.Instance.M1Clear();
            playing = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stealthController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    //Flip
    public bool isFacingRight = true;

    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask whatisGround;
    [SerializeField] float groundRad;
    [SerializeField] Transform groundPos;
    [SerializeField] bool isJumping;
    [SerializeField] float jumpForce;
    [SerializeField] float horizontalDashSpeed;
    [SerializeField] float verticalDashSpeed;
    [SerializeField] float howLongToDash;
    [SerializeField] bool canHorizontalDash;
    [SerializeField] bool canVerticalDash;
    [SerializeField] float dashCoolDown;
    bool isHorizontalDashing;
    bool isVerticalDashing;
    float horizontal;
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        flip();
        AnimationController();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpMethod();
        }
        if (Input.GetKeyDown(KeyCode.E) && canHorizontalDash){
            StartCoroutine(horizontalDash());
        }
    }

    private void JumpMethod()
    {
        if(isHorizontalDashing || isVerticalDashing)
        {
            return;
        }
        if (isGrounded())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        if (isHorizontalDashing || isVerticalDashing)
        {
            return;
        }
        rb2d.velocity = new Vector2(horizontal * moveSpeed, rb2d.velocity.y);
    }
    bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundPos.position, groundRad, whatisGround);
    }
    
    //Coroutines
       IEnumerator verticalDash()
    {
        canVerticalDash = false;
        isVerticalDashing = true;
        float originalGravityScale = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(rb2d.velocity.x, transform.localScale.y * verticalDashSpeed);
        animator.SetBool("isVerticalDashing", true);
        yield return new WaitForSeconds(howLongToDash);
        animator.SetBool("isVerticalDashing", false);
        rb2d.gravityScale = 1f;
        isVerticalDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canVerticalDash = true;

    }
    IEnumerator horizontalDash()
    {
        canHorizontalDash = false;
        isHorizontalDashing = true;
        float originalGravityScale = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.localScale.x * horizontalDashSpeed, rb2d.velocity.y);
        animator.SetBool("isHorizontalDashing", true);
        yield return new WaitForSeconds(howLongToDash);
        rb2d.gravityScale = 1f;
        animator.SetBool("isHorizontalDashing", false);
        isHorizontalDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canHorizontalDash = true;

    }

    void flip()
    {

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundPos.position, groundRad);
        Gizmos.color = Color.red;
    }

    void AnimationController()
    {
         animator.SetBool("onGround", isGrounded());
         animator.SetFloat("verticalSpeed",rb2d.velocity.y);
    }
}

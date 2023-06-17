using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    #region
    //Common
    Animator animator;
    //Camera 
    private Camera camMain;
    //Movement
    Rigidbody2D rb2d;
    float horizontal;
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed;

    //Mouse inputs & position
    Vector3 target;
    Vector3 direction;
    Vector3 rotation;
    [Header("Mouse & Dash Settings")]
    [SerializeField] float increaseX;
    [SerializeField] GameObject crosshair;
    [SerializeField] float dashSpeed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        camMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb2d = gameObject.GetComponent<Rigidbody2D>(
            );
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        target = camMain.ScreenToWorldPoint(Input.mousePosition);
        direction = target - transform.position;
        //target.z = 0;
        crosshair.transform.position = new Vector2(target.x, target.y);
        //To convert mouse click position to feed it into vector3
        if (Input.GetKeyDown(KeyCode.E))
        {
           rb2d.velocity = new Vector2(direction.x, direction.y) * dashSpeed;
        }
    }
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontal * moveSpeed * Time.deltaTime, rb2d.velocity.y);
    }
}

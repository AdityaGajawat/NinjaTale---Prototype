using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_point : MonoBehaviour
{
    #region
    //Object to Camera Declarations
    private Camera camMain;
    Vector3 target;
    Rigidbody2D rb2d;
    stealthController control;
    [SerializeField] Animator animator;
    //Shoot Mechanics
    [Header ("Shoot Mechanism Settings")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnPos;
    [SerializeField] float timebtwShoot;
    [SerializeField] bool canShoot;

    private float timebtw;
    #endregion
    void Start()
    {
      
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        camMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); //Finding object with tag MainCamera and accessing Camera componenet.
        control = GameObject.FindGameObjectWithTag("Player").GetComponent<stealthController>();
    }

    
    void Update()
    {
        if (control.isFacingRight)
        {
            target = camMain.ScreenToWorldPoint(Input.mousePosition); // Storing Vector3 points of mouse Position
            Vector3 rotation = target - transform.position; // Setting direction
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; //Finding Angle
            transform.rotation = Quaternion.Euler(0, 0, rotZ); //Rotating object towards mouse
        }
        else if(!control.isFacingRight)
        {
            target = camMain.ScreenToWorldPoint(Input.mousePosition); // Storing Vector3 points of mouse Position
            Vector3 rotation = transform.position - target; // Setting direction
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; //Finding Angle
            transform.rotation = Quaternion.Euler(0, 0, rotZ); //Rotating object towards mouse
        }
        if (!canShoot)
        {
            timebtw += Time.deltaTime;
            if(timebtw > timebtwShoot)
            {
                canShoot = true;
                timebtw = 0;
            }
        }
        if (Input.GetMouseButton(0) && canShoot)
        {
            canShoot = false;
            animator.SetTrigger("isShot");
            Instantiate(bulletPrefab, spawnPos.position, Quaternion.identity);
        }
    }
}

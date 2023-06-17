using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 target;
    Camera camMain;
    Rigidbody2D rb2d;
    [Header("Bullet Settings")]
    [SerializeField] float bulletForce;
    [SerializeField] int damage = 100;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        camMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        target = camMain.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = target - transform.position;
        Vector3 rotation = transform.position - target;
        rb2d.velocity = new Vector2(direction.x, direction.y).normalized * bulletForce;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot * 90); 
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, .3f);
    }
    private void OnTriggerEnter2D(Collider2D hitinfo)
    {
        AI enemy = hitinfo.GetComponent<AI>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}


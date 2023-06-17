using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    Rigidbody2D rb2d;
    GameObject playerPos;
    Vector3 target;
    [SerializeField] int damage = 100;
    [SerializeField] float force;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        target = playerPos.transform.position; //As playerPos is a game object!
        Vector3 direction = target-transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        rb2d.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot * 90);
    }
    private void OnTriggerEnter2D(Collider2D hitinfo)
    {
        PlayerHealth player = hitinfo.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        Destroy(gameObject, 1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointTowardsPlayer : MonoBehaviour
{
    EnemyLineOfSight sight;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform playerPos;
    [SerializeField] float timeBTW;
    [SerializeField] Transform spawnPos;

    float timebetweenShots;
    Vector3 target;
    void Start()
    {
        sight = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyLineOfSight>();
        
    }

    // Update is called once per frame
    void Update()
    {  
        if (sight.canSeePlayer)
        {
            target = playerPos.position;
            Vector3 rotation = transform.position - target;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            Debug.Log("Dikh gaya!");
            timebetweenShots  += Time.deltaTime;
            if (timebetweenShots > timeBTW)
            {
                Instantiate(bulletPrefab,spawnPos.position, Quaternion.identity);
                timebetweenShots = 0;
            }
        }
        else if (!sight.canSeePlayer)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}

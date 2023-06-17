using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineOfSight : MonoBehaviour
{
    public float radius = 5f;
    [Range(1, 360)] public float angle = 30f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;
    public GameObject playerRef;
    public bool canSeePlayer;

    [SerializeField] Transform gunObject;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform playerPos;
    [SerializeField] float timeBTW;
    [SerializeField] Transform spawnPos;
    float timebetweenShots;
    Vector3 target;

    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVCheck());
    }
    private void Update()
    {
        if (canSeePlayer)
        {
            shootPlayer();

        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FOV();
        }
    }
    private void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (transform.position - target.position).normalized;
            if (Vector2.Angle(transform.rotation.y == 180 ? -transform.right : transform.right, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);
                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }

        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
    private void OnDrawGizmos()
    {
       /* Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
        Vector3 angle01 = DirectionFromAngle(-angle / 2);
        Vector3 angle02 = DirectionFromAngle(angle / 2);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);*/
        if (canSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerRef.transform.position);
        }
    }
    void shootPlayer()
    {
            target = playerPos.position;
            Vector3 rotation = gunObject.position - target;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            gunObject.rotation = Quaternion.Euler(0f, 0f, rotZ);
            Debug.Log("Dikh gaya!");
            timebetweenShots += Time.deltaTime;
            if (timebetweenShots > timeBTW)
            {
                Instantiate(bulletPrefab, spawnPos.position, Quaternion.identity);
                timebetweenShots = 0;
            }
     }
        
    private Vector2 DirectionFromAngle(float angleInDegrees)
    {
        return (Vector2)(Quaternion.Euler(0, 0, angleInDegrees) * (transform.rotation.y == 180 ? transform.right : -transform.right));
    }
}


using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    [Header ("Stats")]
    public float attackRange;
    public float attackRate;
    float attackCountdown = 0f;
    [Header ("Objects")]
    public GameObject bullet;
    public Transform bulletOrigin;
    public Transform player;
    bool isPlayerInAttackRange;
    [Header ("Other")]
    public LayerMask whatIsPlayer;
    private void Update()
    {
        isPlayerInAttackRange = Physics.CheckSphere(gameObject.transform.position, attackRange, whatIsPlayer);
        attackCountdown -= Time.deltaTime;


        if (isPlayerInAttackRange)
        {
            AttackPlayer();
        }

    }


    void AttackPlayer()
    {
        FaceTarget();
        Shoot();
    }

    void FaceTarget()
    {
        Vector3 direction = (player.transform.position - gameObject.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Shoot()
    {
        if (attackCountdown <= 0f)
        {
            attackCountdown = 1f / attackRate;
            GameObject newBullet = Instantiate(bullet, bulletOrigin.position, Quaternion.identity);
            newBullet.transform.forward = transform.forward.normalized;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

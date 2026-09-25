using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Splines;
using static UnityEngine.UI.Image;

public class EnemyShooting : MonoBehaviour
{
    [Header ("Stats")]
    public float attackRange;
    public float attackRate;
    float attackCountdown = 0f;
    Vector3 origin = Vector3.zero;
    public float radius = 5f;
    [Header("Objects")]
    public SplineContainer spline;
    public CinemachineSplineCart cart;
    public CinemachineSplineCart playerCart;
    public GameObject bullet;
    public Transform bulletOrigin;
    public Transform player;
    bool isPlayerInAttackRange;
    [Header ("Other")]
    public LayerMask whatIsPlayer;    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerCart = GameObject.FindGameObjectWithTag("PlayerCart").GetComponent<CinemachineSplineCart>();
        spline = FindAnyObjectByType<SplineContainer>();
        cart.GetComponentInParent<CinemachineSplineCart>();
        cart.Spline = spline;
        cart.SplinePosition = playerCart.SplinePosition + .0034f;
        Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
        gameObject.transform.localPosition = randomPosition;
    }
    private void Update()
    {
        isPlayerInAttackRange = Physics.CheckSphere(gameObject.transform.position, attackRange, whatIsPlayer);
        attackCountdown -= Time.deltaTime;

        FaceTarget();
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

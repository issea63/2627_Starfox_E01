using UnityEngine;

public class EnemyBulletLogic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //other.GetComponent<HealthManager>().hit(damage);
            Destroy(gameObject);
        }
    }
}

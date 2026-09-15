using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;

    [Header("Stats")]
    public float bulletSpeed = 1.0f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        rb.linearVelocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {




            Destroy(gameObject);
        }
    }
}

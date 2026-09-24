using Unity.Cinemachine;
using UnityEngine;

public class EnemyTrigger2 : MonoBehaviour
{
    public CinemachineSplineCart enemyCart;
    public GameObject enemyObject;
    public float enemySpeed = 10f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Trigger");
            enemyObject.SetActive(true);
            SetSpeed(enemySpeed);
        }
        else return;
    }

    public void SetSpeed(float zSpeed)
    {
        var cartSpeed = enemyCart.AutomaticDolly.Method as SplineAutoDolly.FixedSpeed;
        cartSpeed.Speed = zSpeed;
    }
}

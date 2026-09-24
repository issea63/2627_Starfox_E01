using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [Header ("Objects")]
    public CinemachineSplineCart enemyCart;
    public GameObject enemyObject;
    [Header("Stats")]
    public float enemySpeed = 10f;
    private void OnTriggerEnter(Collider other)
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
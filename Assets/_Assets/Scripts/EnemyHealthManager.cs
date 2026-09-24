using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [Header("Objects")]
    public GameObject explosionEffect;
    public AudioSource explosionSound;
    public GameObject bigPrefab;
    public GameObject smallPrefab;
    public GameObject normalPrefab;
    //public GameObject bossPrefab;
    [Header("Stats")]
    public EnemyType enemyType;
    public float maximunHealth = 100f;
    public float currentHealth;
    [Header ("Scripts")]
    public Bullet bullet;
    public enum EnemyType {BigEnemy, SmallEnemy, BossEnemy, NormalEnemy}
    void Start()
    {
        this.AssignStats();
        currentHealth = maximunHealth;
    }
    public void AssignStats()
    {
        switch (enemyType)
        {
            case EnemyType.BigEnemy:
                this.gameObject.GetComponent<EnemyHealthManager>().maximunHealth = 200;
                bullet.playerDamage = 5f;
                bigPrefab.SetActive(true);
                break;
            case EnemyType.SmallEnemy:
                this.gameObject.GetComponent<EnemyHealthManager>().maximunHealth = 40;
                bullet.playerDamage = 20f;
                smallPrefab.SetActive(true);
                break;
            case EnemyType.BossEnemy:
                this.gameObject.GetComponent<EnemyHealthManager>().maximunHealth = 500;
                bullet.playerDamage = 15f;
                //bossPrefab.SetActive(true);
                break;
            case EnemyType.NormalEnemy:
                this.gameObject.GetComponent<EnemyHealthManager>().maximunHealth = 100;
                bullet.playerDamage = 10f;
                normalPrefab.SetActive(true);
                break;
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosionSound.Play();
        Destroy(gameObject);
    }
}

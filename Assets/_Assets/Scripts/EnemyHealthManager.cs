using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [Header("Objects")]
    public GameObject explosionEffect;
    public AudioSource explosionSound;
    /*public GameObject originalPrefab;
    public GameObject bigPrefab;
    public GameObject smallPrefab;
    public GameObject normalPrefab;
    public GameObject bossPrefab;*/
    [Header("Stats")]
    public float maximunHealth = 100f;
    public float currentHealth;
    public float bigDamage;
    public float smallDamage;
    public float normalDamage;
    public float bossDamage;
    [Header ("Scripts")]
    public Bullet bullet;
    public enum EnemyType
    {
        BigEnemy,
        SmallEnemy,
        BossEnemy,
        NormalEnemy
    }
    private void Start()
    {
        AssignStats(EnemyType.BigEnemy, 200f, bigDamage);
        AssignStats(EnemyType.SmallEnemy, 40f, smallDamage);
        AssignStats(EnemyType.NormalEnemy, 100f, normalDamage);
        AssignStats(EnemyType.BossEnemy, 500f, bossDamage);
        currentHealth = maximunHealth;
    }
    public void AssignStats(EnemyType type, float health, float damage)
    {
        if (type == EnemyType.BigEnemy)
        {
            this.gameObject.GetComponent<HealthManager>().maximunHealth = health;
            bullet.playerDamage = damage;
            //bigPrefab.SetActive(true);
        }
        else if (type == EnemyType.SmallEnemy)
        {
            this.gameObject.GetComponent<HealthManager>().maximunHealth = health;
            bullet.playerDamage = damage;
            //smallPrefab.SetActive(true);
        }
        else if (type == EnemyType.NormalEnemy)
        {
            this.gameObject.GetComponent<HealthManager>().maximunHealth = health;
            bullet.playerDamage = damage;
            //normalPrefab.SetActive(true);
        }
        else if (type ==EnemyType.BossEnemy)
        {
            this.gameObject.GetComponent<HealthManager>().maximunHealth = health;
            bullet.playerDamage = damage;
            //bossPrefab.SetActive(true);
        }

    }
}

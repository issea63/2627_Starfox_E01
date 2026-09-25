using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class SpawnerControl : MonoBehaviour
{
    [Header("Spawner Positions")]
    //public Transform[] spawnerPosition;
    public Transform spawnCenterPosition;
    public Vector3 origin;
    public float radius = 10;
    [Header("Objects")]
    public GameObject enemyPrefab;
    [Header("Stats")]
    public float interval = 1f;
    public float timer;
    private void Update()
    {
        origin = new Vector3 (spawnCenterPosition.position.x, spawnCenterPosition.position.y, spawnCenterPosition.position.z);
        timer += Time.deltaTime;
        if(timer >= interval)
        {
            timer = 0;
            StartCoroutine(SpawnRoutine());
        }
    }
    public IEnumerator SpawnRoutine()
    {
        //int transformRandom = Random.Range(0, spawnerPosition.Length);
        //Instantiate(enemyPrefab, spawnerPosition[transformRandom].position, Quaternion.identity);
        Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        yield break;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, radius);
    }
}

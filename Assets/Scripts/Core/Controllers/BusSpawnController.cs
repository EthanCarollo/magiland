using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Core.Controllers.Quest;

public class BusSpawnController : MonoBehaviour
{
    [Header("Spawn Area")]
    [SerializeField] private float width = 100f;
    [SerializeField] private float height = 100f;
    [SerializeField] private GameObject[] prefabs;

    [Header("NavMesh Settings")]
    [SerializeField] private float sampleMaxDistance = 2f;
    [SerializeField] private int maxAttempts = 20;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float minDistanceFromPlayer = 10f;
    [SerializeField] private Transform player;

    private bool spawning = true;

    void Start()
    {
        if (player == null && GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(SpawnLoop());

        BusQuestController.Instance.OnQuestEnd += StopSpawning;
    }

    void OnDisable()
    {
        if(BusQuestController.Instance != null) BusQuestController.Instance.OnQuestEnd -= StopSpawning;
    }

    private void StopSpawning()
    {
        spawning = false;
    }

    IEnumerator SpawnLoop()
    {
        while (spawning)
        {
            yield return new WaitForSeconds(spawnInterval);

            Vector3? pos = GetRandomSpawnPoint();
            if (pos.HasValue && prefabs.Length > 0 && spawning)
            {
                GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
                Instantiate(prefab, pos.Value, Quaternion.identity);
            }
        }
    }

    Vector3? GetRandomSpawnPoint()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomPoint = transform.position +
                                  new Vector3(Random.Range(-width / 2f, width / 2f), 0f,
                                              Random.Range(-height / 2f, height / 2f));

            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, sampleMaxDistance, NavMesh.AllAreas))
            {
                if (player != null && Vector3.Distance(player.position, hit.position) < minDistanceFromPlayer)
                    continue;

                return hit.position;
            }
        }
        return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 size = new Vector3(width, 0.1f, height);
        Gizmos.DrawWireCube(transform.position, size);

        Gizmos.color = new Color(0f, 1f, 0f, 0.1f);
        Gizmos.DrawCube(transform.position, size);
    }
}

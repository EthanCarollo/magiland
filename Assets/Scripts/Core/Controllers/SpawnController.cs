using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SpawnController : MonoBehaviour
{
    [Header("Spawn Area")]
    [SerializeField] private float width = 100f;
    [SerializeField] private float height = 100f;
    [SerializeField] private GameObject prefab;

    [Header("NavMesh Settings")]
    [SerializeField] private float sampleMaxDistance = 2f;
    [SerializeField] private int maxAttempts = 20;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 5f;   // temps entre 2 spawns
    [SerializeField] private float minDistanceFromPlayer = 10f; // distance mini avec le joueur
    [SerializeField] private Transform player; // référence au joueur

    private bool spawning = true;

    void Start()
    {
        if (player == null && GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (spawning)
        {
            yield return new WaitForSeconds(spawnInterval);

            Vector3? pos = GetRandomSpawnPoint();
            if (pos.HasValue)
            {
                Instantiate(prefab, pos.Value, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("[SpawnController] Aucun point spawnable trouvé sur le NavMesh !");
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

            // Check NavMesh
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, sampleMaxDistance, NavMesh.AllAreas))
            {
                // Vérifier distance minimum joueur
                if (player != null && Vector3.Distance(player.position, hit.position) < minDistanceFromPlayer)
                    continue; // trop proche → on retente

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
using System.ComponentModel;
using Data.Enemy;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EnemyLife : MonoBehaviour
{
    [SerializeField] private EnemyData enemy;
    [SerializeField] private float currentLife;
    private MeshRenderer meshRenderer;
    private Material material;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
    }

    void Start()
    {
        currentLife = enemy.maxLife;
    }

    public void TakeDamage(float damage)
    {
        currentLife -= damage;
        if (currentLife <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        material.SetTexture("_BaseMap", enemy.deadBody.texture);
    }
}

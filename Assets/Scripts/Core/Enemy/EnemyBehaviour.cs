using Core.Controllers;
using Data.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("References & Data")]
    [SerializeField] private EnemyData enemy;
    [SerializeField] private float currentLife;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    private SpriteRenderer _spriteRenderer;
    
    [Header("World UI")]
    [SerializeField] private TextMeshProUGUI enemyNameText;
    [SerializeField] private Slider enemyLifeSlider;

    void Start()
    {
        currentLife = enemy.maxLife;
        UpdateUi();
    }

    public void Update()
    {
        navMeshAgent.SetDestination(PlayerController.Instance.transform.position);
    }

    public void TakeDamage(float damage)
    {
        currentLife -= damage;
        UpdateUi();
        if (currentLife <= 0)
        {
            Death();
        }
    }

    public void UpdateUi()
    {
        enemyLifeSlider.maxValue = enemy.maxLife;
        enemyLifeSlider.value = currentLife;
        enemyNameText.text = enemy.enemyName;
    }

    void Death()
    {
        Debug.Log("Enemy is now dead");
    }
}

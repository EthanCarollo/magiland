using Core.Controllers;
using Core.Quest;
using Data.Enemy;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("References & Data")]
    [SerializeField] private EnemyData enemy;
    [SerializeField] private float currentLife;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isDead;
    
    [Header("World UI")]
    [SerializeField] private Transform transformCanvas;
    [SerializeField] private TextMeshProUGUI enemyNameText;
    [SerializeField] private Slider enemyLifeSlider;

    void Start()
    {
        currentLife = enemy.maxLife;
        navMeshAgent.speed = enemy.enemySpeed;
        navMeshAgent.stoppingDistance = enemy.enemyRange;
        UpdateUi();
    }

    public void Update()
    {
        if (isDead) return;

        navMeshAgent.SetDestination(PlayerController.Instance.transform.position);

        bool isMoving = navMeshAgent.velocity.sqrMagnitude > 0.01f 
                        && navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance;

        animator.SetBool("IsMoving", isMoving);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

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
        enemyNameText.text = enemy.enemyName;
        
        LeanTween.cancel(enemyLifeSlider.gameObject);
        LeanTween.value(enemyLifeSlider.gameObject, enemyLifeSlider.value, currentLife, 0.3f)
            .setOnUpdate((float val) =>
            {
                enemyLifeSlider.value = val;
            })
            .setEaseOutCubic();
    }


    void Death()
    {
        isDead = true;
        navMeshAgent.isStopped = true;
        animator.SetBool("IsMoving", false);
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<SpriteSkin>().enabled = false;
        this.GetComponent<NavMeshAgent>().enabled = false;
        this.GetComponent<Collider>().enabled = false;
        if (enemy.deadBodies.Count > 0)
        {
            spriteRenderer.sprite = enemy.deadBodies.GetRandom();
        }
        transformCanvas.gameObject.SetActive(false);
        LeanTween.delayedCall(4f, () =>
        {
            LeanTween.moveY(gameObject, transform.position.y + 200f, 2f)
                .setEaseInOutSine()
                .setOnComplete((() =>
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 200f, 0);
                }));
        });

        if (BusQuestController.Instance != null)
        {
            BusQuestController.Instance.OnEnemyDied();
        }
    }
}
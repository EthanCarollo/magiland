using Core.Controllers;
using Core.Enemy;
using Core.Controllers.Quest;
using Data.Enemy;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BusEnemyBehaviour : BaseEnemyBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;

    [Header("World UI")]
    [SerializeField] private Transform transformCanvas;
    [SerializeField] private TextMeshProUGUI enemyNameText;
    [SerializeField] private Slider enemyLifeSlider;
    [SerializeField] protected Animator animator;
    private AudioSource audioSource;
    private float lastTimeAttacked = 0;
    private bool isAttacking = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        navMeshAgent.speed = enemy.enemySpeed;
        navMeshAgent.stoppingDistance = enemy.enemyRange;

        if (GameController.Instance != null)
        {
            GameController.Instance.OnGamePaused += HandleGamePause;
            GameController.Instance.OnGameOver += HandleGameOver;
        }

        base.Start();
    }

    public void Update()
    {
        base.Update();
        if (IsDead || (GameController.Instance != null && (GameController.Instance.IsGamePaused || GameController.Instance.IsGameOver))) return;

        navMeshAgent.SetDestination(PlayerController.Instance.transform.position);

        bool isMoving = navMeshAgent.velocity.sqrMagnitude > 0.01f
                        && navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance;

        animator.SetBool("IsMoving", isMoving);

        if (navMeshAgent.hasPath &&
            navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Attack();
        }

        // Playing basic enemy sound
        if (!audioSource.isPlaying) audioSource.PlayOneShot(enemy.enemySounds.GetRandom());
    }

    void Attack()
    {
        if (Time.time - lastTimeAttacked >= enemy.enemyAttackCooldown) isAttacking = false;
        if (isAttacking) return;

        isAttacking = true;
        lastTimeAttacked = Time.time;

        if (enemy.attackSound != null)
        {
            if (audioSource.isPlaying) audioSource.Stop();
            audioSource.PlayOneShot(enemy.attackSound);
        }

        PlayerController.Instance.TakeDamage();
    }


    protected override void OnBeforeTakeDamage()
    {

    }

    protected override void OnAfterTakeDamage()
    {

    }

    protected override void OnUpdateUi()
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

    protected override void OnDeath()
    {
        navMeshAgent.isStopped = true;

        animator.SetBool("IsMoving", false);
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<SpriteSkin>().enabled = false;
        this.GetComponent<NavMeshAgent>().enabled = false;
        transformCanvas.gameObject.SetActive(false);
        if (BusQuestController.Instance != null)
            BusQuestController.Instance.AdvanceQuest();
    }

    void HandleGamePause(bool isPaused)
    {
        navMeshAgent.enabled = !isPaused;
    }

    void HandleGameOver()
    {
        HandleGamePause(true);
    }

}
using Core.Controllers;
using Core.Enemy;
using Core.Quest;
using Data.Enemy;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class BusEnemyBehaviour : BaseEnemyBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    
    [Header("World UI")]
    [SerializeField] private Transform transformCanvas;
    [SerializeField] private TextMeshProUGUI enemyNameText;
    [SerializeField] private Slider enemyLifeSlider;

    void Start()
    {
        currentLife = enemy.maxLife;
        navMeshAgent.speed = enemy.enemySpeed;
        navMeshAgent.stoppingDistance = enemy.enemyRange;
        base.Start();
    }

    public void Update()
    {
        base.Update();
        if (IsDead) return;

        navMeshAgent.SetDestination(PlayerController.Instance.transform.position);

        bool isMoving = navMeshAgent.velocity.sqrMagnitude > 0.01f 
                        && navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance;

        animator.SetBool("IsMoving", isMoving);
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
        {
            BusQuestController.Instance.OnEnemyDied();
        }
    }

}
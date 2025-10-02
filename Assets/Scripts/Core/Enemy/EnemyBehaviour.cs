using System.ComponentModel;
using Data.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyBehaviour : MonoBehaviour
{
    [Header("References & Data")]
    [SerializeField] private EnemyData enemy;
    [SerializeField] private float currentLife;
    private SpriteRenderer _spriteRenderer;
    private Material material;
    
    [Header("World UI")]
    [SerializeField] private TextMeshProUGUI enemyNameText;
    [SerializeField] private Slider enemyLifeSlider;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        material = _spriteRenderer.material;
    }

    void Start()
    {
        currentLife = enemy.maxLife;
        UpdateUi();
    }

    public void TakeDamage(float damage)
    {
        currentLife -= damage;
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
        UpdateUi();
    }

    void Death()
    {
        Debug.Log("Enemy is now dead");
        // material.SetTexture("_BaseMap", enemy.deadBody.texture);
    }
}

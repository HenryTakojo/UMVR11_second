using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using test;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Health : MonoBehaviour
{
    private readonly int DeadHash = Animator.StringToHash("Dead");
    private Animator anim;

    public int maxHealth = 100;
    public int health;

    [Header("HP ��������")]
    [Tooltip("���o Canvas �̩��h��q��Image")]
    public RectTransform hpImage;
    [Tooltip("���o������� �A�p�ǻP���b�����l���� Canvas �APlayer�b���� �̫BHp")]
    public GameObject hpBarUI;
    [Tooltip("�����q�������A���a460�A�p�� Hp number �]�w272�A Boss Hp number �]�w272")]
    public int hpLocalNum;
    

    [Header("�ˮ`���Ʀr")]
    [Tooltip("���X�ˮ`�Ʀr�����O�A���� TMP_Text ������ܶˮ`�Ʀr")]
    [SerializeField] GameObject hitNumberPrefab;
    private int mobIndex;

    public event Action OnTakeDamage;
    public event Action OnDie;

    private GameObject bossHP;
    private bool isDamaged = false;
    public float cannotHurtTime = 2.0f;
    public float original_ctTime = 2.0f;
    public ParticleSystem shield;


    void Start()
    {
        //bossHP = GameObject.Find("BossHp");
        health = maxHealth;

        anim = GetComponent<Animator>();
        if(this.TryGetComponent<EnemyStateMachine>(out EnemyStateMachine esm))
        {
            mobIndex = esm.MobEnumIndex;
        }
    }

    private void Update()
    {
        float currentHealth = (float)health;
        float hpPercentage = (float)currentHealth / maxHealth;
        float newXPosition = -hpLocalNum + hpLocalNum * hpPercentage;
        hpImage.localPosition = new Vector3(newXPosition, hpImage.localPosition.y, hpImage.localPosition.z);
        if(isDamaged == true)
        {
            if(shield != null)
            {
                shield.gameObject.transform.position = this.transform.position + this.transform.up;
                shield.gameObject.SetActive(true);
            }
            
            cannotHurtTime -= Time.deltaTime;
            if (cannotHurtTime < 0)
            {
                if (shield != null)
                {
                    shield.gameObject.SetActive(false);
                }
                cannotHurtTime = original_ctTime;
                isDamaged = false;
            }
        }
    }

    public void DealDamage(int damage)
    {
        if (health <= 0) { return; }
        health = Mathf.Max(health - damage, 0);
        float healthRatio = (float)health / maxHealth;

        if (isDamaged == false)
        {
            OnTakeDamage?.Invoke();
            isDamaged = true;
        }

        GenerateHitNumber(damage, this.transform.position + Vector3.up * 0.5f);

        if (health <= 0)
        {
            OnDie?.Invoke();
            anim.Play(DeadHash);
            
            if (!this.CompareTag("Player"))
            {
                StartCoroutine(DisableObject());
                hpBarUI.SetActive(false);
            }
            //if (bossHP.activeSelf)
            //{
            //    bossHP.SetActive(false);
            //}

        }
    }

    private void GenerateHitNumber(int damage, Vector3 pos)
    {
        var numberObject = Instantiate(hitNumberPrefab, pos, Quaternion.identity);
        var textComponent = numberObject.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = damage.ToString();
        }
    }

    public IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using test;
using TMPro;
using UnityEngine;

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

    [SerializeField] GameObject Deadmenu = null;

    public event Action OnTakeDamage;
    public event Action OnDie;



    void Start()
    {
        health = maxHealth;
        Deadmenu.SetActive(false);

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float currentHealth = (float)health;
        float hpPercentage = (float)currentHealth / maxHealth;
        float newXPosition = -hpLocalNum + hpLocalNum * hpPercentage;
        hpImage.localPosition = new Vector3(newXPosition, hpImage.localPosition.y, hpImage.localPosition.z);
    }

    public void DealDamage(int damage)
    {
        if (health <= 0) { return; }
        health = Mathf.Max(health - damage, 0);
        Debug.Log(this.name + ": " + health);
        OnTakeDamage?.Invoke();
        GenerateHitNumber(damage, this.transform.position + Vector3.up * 0.5f);

        if (health <= 0)
        {
            OnDie?.Invoke();
            anim.Play(DeadHash);
            StartCoroutine(DisableObject());
            Deadmenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            hpBarUI.SetActive(false);
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

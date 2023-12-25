using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent1 : MonoBehaviour
{
    [SerializeField] private GameObject weaponLogic;
    public GameObject iceArrow;
    private Transform arrowStart;
    public ParticleSystem GenyuSkill;
    public ParticleSystem GenyuSkill_01;
    public ParticleSystem GenyuSkill_02;
    public ParticleSystem GenyuSkill_03;
    public GameObject skillBall;
    public GameObject skillLotus;


    private void Start()
    {
        arrowStart = GameObject.Find("bowStart").transform;
        skillBall.SetActive(false);
        skillLotus.SetActive(false);
    }
    void Shoot()
    {
        //iceArrow.transform.position = arrowStart.position;
        iceArrow.SetActive(true);
        GameObject arow =  Instantiate(iceArrow, arrowStart.position, transform.rotation);
        arow.transform.forward = transform.forward;
    }

    public void EnableWeapon()
    {
        weaponLogic.SetActive(true);
    }

    public void DisableWeapon()
    {
        weaponLogic.SetActive(false);
    }

    public void UltimateSkill()
    {
        skillBall.transform.position = transform.position;
        skillBall.SetActive(true);
        GenyuSkill.Play();
        GenyuSkill_01.Play();
        GenyuSkill_02.Play();
        GenyuSkill_03.Play();
    }

    public void ActivateEffect()
    {

    }

    public void DodgeSkill()
    {
        skillLotus.transform.position = transform.position;
        skillLotus.transform.forward = transform.forward;
        skillLotus.SetActive(true);
    }
}

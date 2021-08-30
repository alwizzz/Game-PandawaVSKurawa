using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameUnit : MonoBehaviour
{
    public string unitName;
    public GameObject unitObject;
    Animator unitAnimator;
    public GameObject unitArrow;
    Animator arrowAnimator;
    Sprite unitSprite;
    public int maxHP;
    public int currentHP;
    public int defense;
    public int damageOutput;

    public AttackOption[] option = new AttackOption[3];

    void Start(){
        arrowAnimator = unitArrow.GetComponent<Animator>();
        unitAnimator = unitObject.GetComponent<Animator>();
        unitAnimator.SetBool("isDead", false);
        unitSprite = unitObject.GetComponent<Sprite>();
    }

    public void SetCurrentHP_As_MaxHP(){
        currentHP = maxHP;
    }


    public int TakeDamage( int damage, int defense ){
        int realDamage = (damage*(100-defense)/100);
        currentHP = currentHP - realDamage;
        if( currentHP < 0 ){
            currentHP = 0;
        }
        return realDamage;
    }
    public bool isHit(int accuracy){
        if( Random.Range(1,101) <= accuracy)
            return true;
        else   
            return false;
    }

    public bool isDead(){
        if( currentHP <= 0){
            return true;
        }
        else{
            return false;
        }
    }

    public int RandomAttackType(){
        return Random.Range(0, 3);
    }

    public void AttackAnimation(){
        unitAnimator.SetTrigger("AttackTrigger");
    }

    public void AvoidAnimation(){
        unitAnimator.SetTrigger("AvoidTrigger");
    }

    public void ArrowAnimation(){
        Debug.Log("Arrow Animation played");
        arrowAnimator.SetTrigger("LaunchArrow");
    }

    public void DeathAnimation(){
        unitAnimator.SetBool("isDead", true);
    }

    public void ShowGameObject(){
        unitObject.SetActive(true);
    }






}

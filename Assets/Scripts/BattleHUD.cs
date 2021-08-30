using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text HUDName;
    public Text HUDHP;
    public Slider HPSlider;

    public void SetHUD(GameUnit unit){
        HUDName.text = unit.name;
        HUDHP.text = unit.currentHP + "/" + unit.maxHP;
        HPSlider.maxValue = unit.maxHP;
        HPSlider.value = unit.currentHP;
    }

    public void SetHP(GameUnit unit){
        HUDHP.text = unit.currentHP + "/" + unit.maxHP;
        HPSlider.value = unit.currentHP;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour
{
    public GameUnit unitStatus;
    public Text correspondingName;
    public Text attackValue;
    public Text accuracyValue;
    public Text healthValue;
    public Text defenseValue;

    void Start()
    {
        correspondingName.text = unitStatus.unitName;

        attackValue.text = ( (unitStatus.option[0].attackDamage + unitStatus.option[1].attackDamage
         + unitStatus.option[2].attackDamage)/3 ).ToString();

        accuracyValue.text = ( (unitStatus.option[0].attackAccuracy + unitStatus.option[1].attackAccuracy
         + unitStatus.option[2].attackAccuracy)/3 ).ToString();

        healthValue.text = unitStatus.maxHP.ToString();

        defenseValue.text = unitStatus.defense.ToString();
    }

}

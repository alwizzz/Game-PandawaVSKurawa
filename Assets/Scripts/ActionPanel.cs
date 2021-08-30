using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour
{
    public GameObject panel;
    public Button attack1;
    Text attack1Text;
    public Button attack2;
    Text attack2Text;
    public Button attack3;
    Text attack3Text;
    public BattleSystem system;

    void Start(){
        attack1Text = attack1.GetComponentInChildren<Text>();
        attack1Text.text = system.AttackOptionName(0);

        attack2Text = attack2.GetComponentInChildren<Text>();
        attack2Text.text = system.AttackOptionName(1);

        attack3Text = attack3.GetComponentInChildren<Text>();
        attack3Text.text = system.AttackOptionName(2);
    }
}

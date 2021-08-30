using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }



public class BattleSystem : MonoBehaviour
{

    public GameObject classPandawa;
    public GameObject classKurawa;
    GameUnit playerUnit;
    GameUnit enemyUnit;
	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;
	public BattleState state;

    public GameObject actionPanel;
    Text battleText;
    public GameObject eventPanel;
    Text eventText;
    public GameObject endPanel;
    Text endText;

    public MusicController battleMusic;

    public void SwitchToActionPanel(){
        actionPanel.SetActive(true);
        eventPanel.SetActive(false);
    }
    public void SwitchToEventPanel(){
        actionPanel.SetActive(false);
        eventPanel.SetActive(true);
    }
    public void SetEndPanel(bool mode){
        if(mode){
            endPanel.SetActive(true);
        }
        else{
            endPanel.SetActive(false);
        }
    }

    void Start()
    {
        Debug.Log("playerChoice at BattleScene: " + SceneLoader.playerChoice);
        if(SceneLoader.playerChoice == 0){
            Debug.Log("playerChoice changed to default (1)");
            SceneLoader.playerChoice = 1;
        }
        if(SceneLoader.playerChoice == 1){
            playerUnit = classPandawa.transform.GetChild(0).GetComponent<GameUnit>();
        }
        else if(SceneLoader.playerChoice == 2){
            playerUnit = classPandawa.transform.GetChild(1).GetComponent<GameUnit>();
        }
        playerUnit.ShowGameObject();
        

        Debug.Log("enemyChoice at BattleScene: " + SceneLoader.enemyChoice);
        if(SceneLoader.enemyChoice == 0){
            Debug.Log("enemyChoice changed to default (1)");
            SceneLoader.enemyChoice = 1;
        }
        if(SceneLoader.enemyChoice == 1){
            enemyUnit = classKurawa.transform.GetChild(0).GetComponent<GameUnit>();
        }
        else if(SceneLoader.enemyChoice == 2){
            enemyUnit = classKurawa.transform.GetChild(1).GetComponent<GameUnit>();
        }
        enemyUnit.ShowGameObject();
        

        battleText = actionPanel.GetComponentInChildren<Text>();
        eventText = eventPanel.GetComponentInChildren<Text>();
        endText = endPanel.GetComponentInChildren<Text>();

        battleText.text = "Your Action?";
        eventText.text = "Battle Start!";

        SetEndPanel(false);
        SwitchToEventPanel();

        state = BattleState.START;
        StartCoroutine(BattleSetup());
    }

    

    IEnumerator BattleSetup(){

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        playerUnit.SetCurrentHP_As_MaxHP();
        enemyUnit.SetCurrentHP_As_MaxHP();

        yield return new WaitForSeconds(2f);

        SwitchToActionPanel();

        state = BattleState.PLAYERTURN;
    }

    public void OnAttack1Button(){
        if( state == BattleState.PLAYERTURN ){
            battleMusic.PlayClickButtonSFX();
            Debug.Log(SceneManager.GetActiveScene().name + ": " + "Attack1Button clicked");
            StartCoroutine(PlayerAttack(0));
        }
    }
    public void OnAttack2Button(){
        if( state == BattleState.PLAYERTURN ){
            battleMusic.PlayClickButtonSFX();
            Debug.Log(SceneManager.GetActiveScene().name + ": " + "Attack2Button clicked");
            StartCoroutine(PlayerAttack(1));
        }
    }
    public void OnAttack3Button(){
        if( state == BattleState.PLAYERTURN ){
            battleMusic.PlayClickButtonSFX();
            Debug.Log(SceneManager.GetActiveScene().name + ": " + "Attack3Button clicked");
            StartCoroutine(PlayerAttack(2));
        }
    }

    IEnumerator PlayerAttack(int attackType){
        SwitchToEventPanel();
        eventText.text = playerUnit.unitName + " uses " + playerUnit.option[attackType].attackName;
        yield return new WaitForSeconds(1f);

        eventText.text = playerUnit.option[attackType].attackDeclare;
        
        if( playerUnit.isHit( playerUnit.option[attackType].attackAccuracy ) ){
            playerUnit.AttackAnimation();
            battleMusic.PlayCharacterHit();
            if(SceneLoader.playerChoice == 2){
                playerUnit.ArrowAnimation();
            }
            yield return new WaitForSeconds(1.5f);
            playerUnit.damageOutput = enemyUnit.TakeDamage( playerUnit.option[attackType].attackDamage, enemyUnit.defense );
            enemyHUD.SetHP(enemyUnit);
            eventText.text = playerUnit.option[attackType].attackSuccess
            + "\n" + playerUnit.unitName + " deal " + playerUnit.damageOutput + " damage!";
        }
        else{
            playerUnit.AttackAnimation();
            battleMusic.PlayCharacterMiss();
            if(SceneLoader.playerChoice == 2){
                playerUnit.ArrowAnimation();
            }
            enemyUnit.AvoidAnimation();
            yield return new WaitForSeconds(1.5f);
            eventText.text = playerUnit.option[attackType].attackFail;  
        }
        yield return new WaitForSeconds(2f);

        if(enemyUnit.isDead()){
            state = BattleState.WON;
            StartCoroutine( EndBattle() );
        }
        else{
            state = BattleState.ENEMYTURN;
            StartCoroutine( EnemyAttack( enemyUnit.RandomAttackType() ) );
        }
    }

    IEnumerator EnemyAttack(int attackType){
        eventText.text = "Enemy Turn";
        yield return new WaitForSeconds(2f);

        eventText.text = enemyUnit.unitName + " uses " + enemyUnit.option[attackType].attackName;
        yield return new WaitForSeconds(1f);

        eventText.text = enemyUnit.option[attackType].attackDeclare;

        if( enemyUnit.isHit( enemyUnit.option[attackType].attackAccuracy ) ){
            enemyUnit.AttackAnimation();
            battleMusic.PlayCharacterHit();
            if(SceneLoader.enemyChoice == 2){
                enemyUnit.ArrowAnimation();
            }
            yield return new WaitForSeconds(1.5f);
            enemyUnit.damageOutput = playerUnit.TakeDamage( enemyUnit.option[attackType].attackDamage, playerUnit.defense );
            playerHUD.SetHP(playerUnit);
            eventText.text = enemyUnit.option[attackType].attackSuccess 
            + "\n" + enemyUnit.unitName + " deal " + enemyUnit.damageOutput + " damage!";
        }
        else{
            enemyUnit.AttackAnimation();
            battleMusic.PlayCharacterMiss();
            if(SceneLoader.enemyChoice == 2){
                enemyUnit.ArrowAnimation();
            }
            playerUnit.AvoidAnimation();
            yield return new WaitForSeconds(1.5f);
            
            eventText.text = enemyUnit.option[attackType].attackFail;
        }
        yield return new WaitForSeconds(2f);

        if(playerUnit.isDead()){
            state = BattleState.LOST;
            StartCoroutine( EndBattle() );
        }
        else{
            state = BattleState.PLAYERTURN;
            SwitchToActionPanel();
        }
    }

    IEnumerator EndBattle(){
        if( state == BattleState.WON ){
            endText.text = "You Won!";
        }
        else if( state == BattleState.LOST ){
            endText.text = "You Lost!";
        }
        battleMusic.PlayDeathScream();
        yield return new WaitForSeconds(MusicController.deathScreamDelay);
        if( state == BattleState.WON ){
            enemyUnit.DeathAnimation();
        }
        else if( state == BattleState.LOST ){
            playerUnit.DeathAnimation();
        }
        SetEndPanel(true);
    }


    public string AttackOptionName(int index){
        return playerUnit.option[index].attackName;
    }
}

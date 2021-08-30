using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static int playerChoice;
    public static int enemyChoice;
    public MusicController pickMusic;

    public Animator sceneTransition;
    public float transitionDuration = 1f;


    public void OnChoice1Button(){
        Debug.Log(SceneManager.GetActiveScene().name + ": " + "Choice1Button clicked");
        if( SceneManager.GetActiveScene().name == "PickPlayerScene" ){
            playerChoice = 1;
            Debug.Log("playerChoice: " + playerChoice);
            pickMusic.PlayClickButtonSFX();
            StartCoroutine( LoadTheScene("PickEnemyScene", sceneTransition, transitionDuration) );
        }
        else if( SceneManager.GetActiveScene().name == "PickEnemyScene" ){
            enemyChoice = 1;
            Debug.Log("enemyChoice: " + enemyChoice);
            pickMusic.PlayClickButtonSFX();
            StartCoroutine( LoadTheScene("BattleScene", sceneTransition, transitionDuration) );
        }
        
    }

    public void OnChoice2Button(){
        Debug.Log(SceneManager.GetActiveScene().name + ": " + "Choice2Button clicked");
        if( SceneManager.GetActiveScene().name == "PickPlayerScene" ){
            playerChoice = 2;
            Debug.Log("playerChoice: " + playerChoice);
            pickMusic.PlayClickButtonSFX();
            StartCoroutine( LoadTheScene("PickEnemyScene", sceneTransition, transitionDuration) );
        }
        else if( SceneManager.GetActiveScene().name == "PickEnemyScene" ){
            enemyChoice = 2;
            Debug.Log("enemyChoice: " + enemyChoice);
            pickMusic.PlayClickButtonSFX();
            StartCoroutine( LoadTheScene("BattleScene", sceneTransition, transitionDuration) );
        }
    }

    public void OnStartButton(){
        Debug.Log(SceneManager.GetActiveScene().name + ": " + "StartButton clicked");
        pickMusic.PlayClickButtonSFX();
        StartCoroutine( LoadTheScene("PickPlayerScene", sceneTransition, transitionDuration) );
    }

    public void OnExitButton(){
        Debug.Log(SceneManager.GetActiveScene().name + ": " + "ExitButton clicked");
        pickMusic.PlayClickButtonSFX();
        Application.Quit();
    }

    public void OnBackToMenuButton(){
        Debug.Log(SceneManager.GetActiveScene().name + ": " + "BackToMenuButton clicked");
        pickMusic.PlayClickButtonSFX();
        StartCoroutine( LoadTheScene("MainMenuScene", sceneTransition, transitionDuration) );
    }

    IEnumerator LoadTheScene(string scene, Animator transition, float duration){
        transition.SetTrigger("TransitionStart");
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(scene);
    }

}

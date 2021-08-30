using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MusicController : MonoBehaviour
{
    public static float volumeValue = 1;
    public AudioSource clickButtonSFX;
    public static float clickButtonDelay = 0.8f;

    public AudioSource sceneSoundtrack;

    public AudioSource deathScreamSFX;
    public static float deathScreamDelay = 1f;

    public AudioSource characterHitSFX;

    public AudioSource characterMissSFX;

    void Start(){

        updateVolume();
        sceneSoundtrack.Play(0);
    }

    public void PlayClickButtonSFX(){
        Debug.Log("clickButton played at " + SceneManager.GetActiveScene().name);   
        clickButtonSFX.Play(0);
    }

    public void PlayDeathScream(){
        deathScreamSFX.Play(0);
    }

    public void PlayCharacterHit(){
        characterHitSFX.Play(0);
    }

    public void PlayCharacterMiss(){
        characterMissSFX.Play(0);
    }

    public void updateVolume(){
        sceneSoundtrack.volume = volumeValue;
        clickButtonSFX.volume = volumeValue;
        deathScreamSFX.volume = volumeValue;
        characterHitSFX.volume = volumeValue;
        characterMissSFX.volume = volumeValue;
    }

}

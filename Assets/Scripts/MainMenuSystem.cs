using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSystem : MonoBehaviour
{
    public GameObject optionPanel;
    public Slider volumeSlider;
    public MusicController volumeController;
    public AudioSource clickSFX;

    bool controller;

    void Start(){
        optionPanel.SetActive(false);
        volumeSlider.value = MusicController.volumeValue;

        controller = false;
    }

    public void OnOptionButton(){
        clickSFX.Play(0);
        Debug.Log("clickButton played from MainMenuSystem"); 
        optionPanel.SetActive(true);
        controller = true;
    }

    public void OnCloseButton(){
        clickSFX.Play(0);
        Debug.Log("clickButton played from MainMenuSystem");
        optionPanel.SetActive(false);
        controller = false;
    }

    public void OnVolumeSlider(){
        if(controller){
            MusicController.volumeValue = volumeSlider.value;
            volumeController.updateVolume();
            volumeController.PlayClickButtonSFX();
            Debug.Log("because this volumeslider");
        }
        
    }
}

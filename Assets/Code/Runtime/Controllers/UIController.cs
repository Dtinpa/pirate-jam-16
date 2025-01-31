using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [SerializeField] private GameObject      endScreen; // Appears on game completion
    [SerializeField] private TextMeshProUGUI bulletCount; // ammo for the gun
    [SerializeField] private GameObject      pauseMenuUI; // Pause menu to resume or quit
    [SerializeField] private Button          resumeGameButton; // allows the game to continue
    [SerializeField] private Button          quitGameButton; // allows player to exit app
    [SerializeField] private GameObject      HUD; // the player character interface
    [SerializeField] private Slider          detectionMeter; // meter that shows how much the player has been detected
    [SerializeField] private TextMeshProUGUI banner; // end screen text for win or loss

    private void Start() {
        EventManager.current.InitializeEndUI += InitializeEndUI;
        EventManager.current.TogglePauseUI   += TogglePauseUI;
        EventManager.current.UpdateBulletUI += UpdateBulletUI;
        EventManager.current.IncrementDetectionMeter += IncrementDetectionMeter;
    }

    private void OnDestroy() {
        EventManager.current.InitializeEndUI -= InitializeEndUI;
        EventManager.current.TogglePauseUI   -= TogglePauseUI;
        EventManager.current.UpdateBulletUI  -= UpdateBulletUI;
        EventManager.current.IncrementDetectionMeter -= IncrementDetectionMeter;
    }


    private void UpdateBulletUI(int bullets)
    {
        bulletCount.text = bullets.ToString();
    }
    
    //initializes the UI on start up and whenever a wave is cleared
    private void InitializeEndUI(string message) {
        banner.text = message;
        endScreen.SetActive(true);
        ToggleHUD(false);
    }

    private void ToggleHUD(bool setActive)
    {
        HUD.SetActive(setActive);
    }

    private void TogglePauseUI(bool setActive)
    {
        ToggleHUD(!setActive);
        pauseMenuUI.SetActive(setActive);
    }

    private void IncrementDetectionMeter(float value)
    {
        // if we've reached the max value, then the game is lost
        if (detectionMeter.value >= detectionMeter.maxValue)
        {
            EventManager.current.OnGameFinished("You lost and got caught by the humans.  You'll never be SAFE again!!!");
        }
        else
        {
            detectionMeter.value += value;
        }
    }

}
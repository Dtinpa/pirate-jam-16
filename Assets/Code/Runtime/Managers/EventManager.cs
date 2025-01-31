using System;

// using Code.Runtime.Skeletons;

using UnityEngine;

public class EventManager : MonoBehaviour {
    public static EventManager current;

    private void Awake() {
        current = this;
    }

    #region Game-Start-End-Overhead

    public event Action GameStarted;
    public void         OnGameStarted() => GameStarted?.Invoke();

    public event Action GameEnded;
    public void         OnGameEnded() => GameEnded?.Invoke();

    public event Action<string> GameFinished;
    public void         OnGameFinished(string message) => GameFinished?.Invoke(message);

    public event Action GameRestart;
    public void         OnGameRestart() => GameRestart?.Invoke();

    public event Action GamePause;
    public void         OnGamePause() => GamePause?.Invoke();

    public event Action GameUnPause;
    public void         OnGameUnPause() => GameUnPause?.Invoke();

    public event Action GameIncrementScore;
    public void OnGameIncrementScore() => GameIncrementScore?.Invoke();

    #endregion

    //UIController Events

    #region Initialize UI

    public event Action<int, int> InitializeUI;
    public void                   OnInitializeUI(int waveCount, int killCount) => InitializeUI?.Invoke(waveCount, killCount);

    #endregion

    #region Initialize End UI

    public event Action<string> InitializeEndUI;
    public void         OnInitializeEndUI(string message) => InitializeEndUI?.Invoke(message);

    #endregion

    #region Toggle Pause Menu UI

    public event Action<bool> TogglePauseUI;
    public void               OnTogglePauseUI(bool setActive) => TogglePauseUI?.Invoke(setActive);

    #endregion

    #region Update Bullet UI

    public event Action<int> UpdateBulletUI;
    public void OnUpdateBulletUI(int bullets) => UpdateBulletUI?.Invoke(bullets);

    #endregion

    // Player Controller

    #region Fire Gun

    public event Action PlayerAimGunToggle;
    public void OnPlayerAimGunToggle() => PlayerAimGunToggle?.Invoke();

    #endregion

    #region Fire Gun

    public event Action PlayerFireBullet;
    public void OnPlayerFireBullet() => PlayerFireBullet?.Invoke();

    #endregion

    #region Disable Current Gun

    public event Action DisableCurrentGun;
    public void OnDisableCurrentGun() => DisableCurrentGun?.Invoke();

    #endregion

    // Enemy Controller

    public event Action<float> IncrementDetectionMeter;
    public void OnIncrementDetectionMeter(float value) => IncrementDetectionMeter?.Invoke(value);

}
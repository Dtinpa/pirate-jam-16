using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager current;
    public        bool        gamePaused { get; private set; } = false;
    public        bool        gameEnd    { get; private set; } = false;

    [SerializeField] private int sceneToLoadOnRestart = 1;
    [SerializeField] private int maxScore = 3;
    private int gunsInSafe { get; set; } = 0;

    private void Awake() {
        current = this;
    }

    private void Start() {
        Debug.developerConsoleVisible = true;
        EventManager.current.GameStarted += StartGame;
        EventManager.current.GameEnded   += EndGame;

        EventManager.current.GameFinished+= GameFinished;
        EventManager.current.GameRestart += GameRestart;

        EventManager.current.GamePause   += PauseGame;
        EventManager.current.GameUnPause += UnPauseGame;

        EventManager.current.GameIncrementScore += IncrementScore;

        //ensures the Puase menu will always be deactivated on game start up
        // EventManager.current.OnGameUnPause();
    }

    private void OnDestroy() {
        EventManager.current.GameStarted -= StartGame;
        EventManager.current.GameEnded   -= EndGame;

        EventManager.current.GameFinished -= GameFinished;
        EventManager.current.GameRestart -= GameRestart;

        EventManager.current.GamePause   -= PauseGame;
        EventManager.current.GameUnPause -= UnPauseGame;

        EventManager.current.GameIncrementScore -= IncrementScore;
    }

    private void StartGame() {
        Debug.Log("New Game has started");
    }

    private void EndGame() {
        SceneManager.LoadScene(0);
    }

    private void GameRestart() {
        SceneManager.LoadScene(sceneToLoadOnRestart);
    }

    private void GameFinished(string message) {
        gameEnd = true;
        EventManager.current.OnInitializeEndUI(message);
    }

    private void PauseGame() {
        if (!gamePaused) {
            Time.timeScale = 0;
            gamePaused     = true;
            EventManager.current.OnTogglePauseUI(true);
        }
    }

    private void UnPauseGame() {
        Time.timeScale = 1;
        gamePaused     = false;
        EventManager.current.OnTogglePauseUI(false);
    }

    private void IncrementScore()
    {
        gunsInSafe += 1;

        if(gunsInSafe >= maxScore)
        {
            GameFinished("Congratz, the guns are SAFEly stored!  Better SAFE than sorry!!!");
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (!gamePaused)
            {
                EventManager.current.OnGamePause();
            } else
            {
                EventManager.current.OnGameUnPause();
            }
        }
    }

}
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int gameScore;
    private int biggestLevel;
    private int currentLevel;
    public bool gameOver;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PoleController[] poleController;
    [SerializeField] private TextMeshProUGUI scoreUI;
    private void Awake()
    {
        Time.timeScale = 0;
        biggestLevel = SceneManager.sceneCount-1;
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }
    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform= GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (playerController == null)
        {
            playerController = playerTransform.GetComponent<PlayerController>();
        }
        if (poleController == null)
        {
            poleController = playerTransform.GetComponentsInChildren<PoleController>();
        }
    }

    private void Update()
    {
        if (gameOver)
        {
            GameFailed();
        }
        scoreUI.text = gameScore.ToString("0");
    }
    
    //this trigger is the trigger of the finishline at the game end.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            gameScore++;
        }
        if (other.CompareTag("PlayerChild"))
        {
            playerController.gameCompleted = true;
            //both poles' magnet effect is neutralized:
            for(int i = 0; i<poleController.Length; i++)
            {
                poleController[i].gameCompleted = true;
            }
            Invoke("GameWon", 5f);
        }
    }
    private void GameFailed()
    {
        SceneManager.LoadScene(0);
    }
    private void GameWon()
    {
        //if there is no new level the first scene loads
        if (currentLevel == biggestLevel)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(currentLevel + 1);
    }
    //Debug menu method for play button:
    public void GameStarted()
    {
        Time.timeScale = 1;
    }
}

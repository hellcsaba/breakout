using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public Brick[] bricks {get; private set; }

    public int level = 1;
    public int score = 0;
    public int lives = 3;
    private int selectedLevel;

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI livesText;

    private void Awake(){
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded; //subscribe to load event
    }

    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }

    public void NewGame(int selectedLevel)
    {
        this.selectedLevel = selectedLevel;
        LoadLevel(this.selectedLevel);
    }

    private void LoadLevel(int level)
    {
        this.level = level;
        SceneManager.LoadScene("Level" + level);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();
        this.score = 0;
        this.lives = 3;
        scoreText = GameObject.Find("Score")?.GetComponent<TextMeshProUGUI>();
        livesText = GameObject.Find("Lives")?.GetComponent<TextMeshProUGUI>();
    }

    public void Hit(Brick brick)
    {
        this.score += brick.points;
        if(scoreText != null)
            scoreText.text = "Score: " + this.score;
        if(Cleared()){
            SceneManager.LoadScene("WinScreen");
        }
    }

    private void ResetLevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOverScreen");
    }

    public void Miss()
    {
        this.lives--;
        if(livesText != null){
            livesText.text = "Lives: " + this.lives;
        }
        if(this.lives > 0) {
            ResetLevel();
        } else {
            GameOver();
        }
    }

    private bool Cleared()
    {
        for(int i = 0; i < this.bricks.Length; i++){
            if(this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable) {
                return false;
            }
        }

        return true;
    }
}

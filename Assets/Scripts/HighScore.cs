using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    int highScore = 0;
    [SerializeField]
    TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    public static HighScore Instance;
    
    [SerializeField]
    TextMeshProUGUI scoreText;
    float time = 0f;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
    }

    
    void Update()
    {
        time += Time.deltaTime;
        scoreText.text = ((int)time).ToString();

        if(Input.GetKeyDown(KeyCode.R))
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (((int)time) > highScore)
            highScore = ((int)time);
        highScoreText.text = highScore.ToString();

        SceneManager.LoadScene(0);
        time = 0f;
    }

    public void AddScore(int val)
    {
        time += val;
    }
}

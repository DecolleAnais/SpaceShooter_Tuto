using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] m_hazards;
    public Vector3 m_spawnPosition;
    public int m_hazardCount;

    public float m_spawnWait;
    public float m_startWait;
    public float m_waveWait;

    public Text m_scoreText;
    //public Text m_restartText;
    public Text m_gameOverText;
    public GameObject m_restartButton;

    private int m_score;
    //private bool m_restart;
    private bool m_gameOver;

    // Use this for initialization
    void Start () {
        m_score = 0;
        UpdateScore();

        m_gameOver = false;
        m_gameOverText.text = string.Empty;
        //m_restart = false;
        //m_restartText.text = string.Empty;
        m_restartButton.SetActive(false);

        // Coroutine and Actions
        StartCoroutine(SpawnWaves());
        //FindObjectOfType<DestroyByContact>().OnModifyScore += AddScore;
        DestroyByContact.OnModifyScore += AddScore;
        DestroyByContact.OnGameOver += GameOver;
	}

    // Update is called once per frame
    void Update () {
		/*if(m_restart && Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }*/
	}

    IEnumerator SpawnWaves()
    {
        // wait for start
        yield return new WaitForSeconds(m_startWait);

        // game loop
        while(!m_gameOver)
        {
            for (int i = 0; i < m_hazardCount; i++)
            {
                Vector3 spawnPosition =
                    new Vector3(
                    Random.Range(-m_spawnPosition.x, m_spawnPosition.x),
                    m_spawnPosition.y,
                    m_spawnPosition.z);

                Quaternion spawnRotation = Quaternion.identity;

                GameObject hazard = m_hazards[ Random.Range(0, m_hazards.Length) ];

                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(m_spawnWait);
            }
            yield return new WaitForSeconds(m_waveWait);
        }

    }

    public void AddScore(int scoreValue)
    {
        m_score += scoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        m_scoreText.text = "Score : " + m_score;
    }

    public void GameOver()
    {
        m_gameOverText.text = "GAME OVER";
        m_gameOver = true;

        // restart ?
        //m_restartText.text = "Press R to restart";
        m_restartButton.SetActive(true);
        //m_restart = true;
    }

    public void ReloadScene()
    {
        DestroyByContact.OnModifyScore -= AddScore;
        DestroyByContact.OnGameOver -= GameOver;
        SceneManager.LoadScene(0);
    }
    
}

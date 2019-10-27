using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenControl : MonoBehaviour
{
    public GameObject wallGroup;
    public Text continueText;
    public Text secoundsText;
    public Text scoreText;
    public Transform rotateObject;
    public Transform tapToGo;
    public Transform swapGo;
    public Transform skins;
    public Image settings;
    public Transform score;
    public bool gameRun;
    private bool rotate;
    private bool hide;
    public bool gameOver;
    public bool restart;
    public bool canRotate;
    private float rotateSpeed;
    private float currentRot, maxRot;
    private float hideSpeed;
    public float gameOverDelayTargetTime;
    public float targetSpawnTime,targetSpawnMax;
    public float targetUpLevelTime, targetUpLevelMax;
    public float spawnY;
    public float wallSpeed;
    public int gameStep;
    public int scorePoints;

    // Start is called before the first frame update
    void Start()
    {
        gameRun = false;
        rotate = false;
        hide = false;
        gameOver = false;
        restart = false;
        canRotate = false;
        maxRot = 180;
        currentRot = 0;
        rotateSpeed = 30f;
        hideSpeed = 20;
        continueText.gameObject.SetActive(false);
        secoundsText.gameObject.SetActive(false);
        gameStep = 1;
        spawnY = -14;
        targetSpawnMax = 6;
        targetSpawnTime = targetSpawnMax;
        targetUpLevelMax = targetSpawnMax * 3;
        targetUpLevelTime = targetUpLevelMax;
        scorePoints = 0;
        wallSpeed = 4.0f;
        scoreText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
        {
            if (!scoreText.gameObject.activeSelf)
            {
                scoreText.gameObject.SetActive(true);
            }
            else
            {
                scoreText.text = "Score: " + scorePoints.ToString();
            }
        }
        targetSpawnTime -= Time.deltaTime;
        if (targetSpawnTime <= 0.0f)
        {
            timerSpawnEnded();
        }

        targetUpLevelTime -= Time.deltaTime;
        if(targetUpLevelTime < 0.0f)
        {
            timerUpLevelEnded();
        }

        if (rotate && gameRun)
        {
            Rotate();
        }

        if (hide)
        {
            Hide();
        }

        if (gameOver)
        {
            continueText.gameObject.SetActive(true);
            secoundsText.gameObject.SetActive(true);

            //Debug.Log(gameOverDelayTargetTime);
            if (gameOverDelayTargetTime <= 0.0f)
            {
                GameOverDelayTimerEnd();
            }
            else
            {
                secoundsText.text = Mathf.RoundToInt(gameOverDelayTargetTime).ToString();
                gameOverDelayTargetTime -= Time.deltaTime;
            }
        }
    }

    void OnMouseDown()
    {
        if (!gameRun)
        {
            Destroy(tapToGo.gameObject);
            settings.gameObject.SetActive(false);
            hide = true;
            gameRun = true;
        }
        else
        {
            if (!rotate)
            {
                rotate = true;
            }
        }
    }


    void Rotate()
    {
        if (canRotate)
        {

            if (currentRot < maxRot)
            {
                rotateObject.eulerAngles = new Vector3(rotateObject.eulerAngles.x, rotateObject.eulerAngles.y, rotateObject.eulerAngles.z - rotateSpeed);
                currentRot += rotateSpeed;

            }
            else
            {
                currentRot = 0;

                rotate = false;
            }
            Mathf.RoundToInt(rotateObject.rotation.eulerAngles.z);
            //Debug.Log(rotateObject.rotation.eulerAngles.z);
        }
    }

    void Hide()
    {
        if (rotateObject.transform.position.y > -4.355)
        {
            rotateObject.Translate(Vector2.down * hideSpeed * Time.deltaTime);
        }
        else
        {
            canRotate = true;
        }


        if (rotateObject.transform.position.y > -6)
        {
            swapGo.Translate(Vector2.up * hideSpeed * Time.deltaTime);
            skins.Translate(Vector2.down * hideSpeed * Time.deltaTime);
            score.Translate(Vector2.up * hideSpeed * Time.deltaTime);
        }
        else
        {
            hide = false;  // Ending hiding
        }
    }

    void GameOverDelayTimerEnd()
    {
        restart = false;
        SceneManager.LoadScene("SampleScene");
        gameOverDelayTargetTime = 0;
    }

    void timerSpawnEnded()
    {
        if (!gameOver && gameRun)
        {
            wallSpeed += 0.1f;
            Vector3 pos = this.transform.position;

            Instantiate(wallGroup, new Vector2(pos.x, pos.y - spawnY), Quaternion.identity);
            targetSpawnTime = targetSpawnMax;
        }
    }


    void timerUpLevelEnded()
    {
        if (gameStep < 4 && !gameOver && gameRun)
        {
            gameStep++;
        }
        targetUpLevelTime = targetUpLevelMax;
    }
}

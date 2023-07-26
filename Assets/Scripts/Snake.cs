using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    private Vector2 dir = Vector2.right;
    private List<Transform> tail = new List<Transform>();

    private bool ate = false;
    private int score;
    static private int highestScore;

    private float moveStartTime = 0f;
    private float speedUpStartTime = 0f;
    private float movePeriod = 0.2f; // time between coroutine
    private float speedUpPeriod = 5f; // after 5 seconds increase the move speed

    [SerializeField] private GameObject tailPrefab;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highestScoreText;

    // Start is called before the first frame update
    private void Start()
    {
        //InvokeRepeating("Move", 0.3f, 0.3f);
        //StartCoroutine(Move());
        gameOverPanel.SetActive(false);
        score = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        moveStartTime += Time.deltaTime;
        speedUpStartTime += Time.deltaTime;
        highestScoreText.text = $"Highest score: {highestScore}";
        
        if (speedUpStartTime > speedUpPeriod && movePeriod >= 0f)
        {
            speedUpStartTime = 0;
            movePeriod -= 0.01f;
        }

        if (moveStartTime >= movePeriod)
        {
            moveStartTime = 0;
            StartCoroutine(Move());
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && dir != Vector2.up)
        {
            dir = Vector2.down;
        } else if (Input.GetKeyDown(KeyCode.UpArrow) && dir != Vector2.down)
        {
            dir = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && dir != Vector2.right)
        {
            dir = Vector2.left;
        } else if (Input .GetKeyDown(KeyCode.RightArrow) && dir != Vector2.left)
        {
            dir = Vector2.right;
        }
    }

    IEnumerator Move()
    {
        Vector2 v = transform.position;
        transform.Translate(dir);

        if (ate)
        {
            GameObject g = (GameObject) Instantiate(tailPrefab, v, Quaternion.identity);
            tail.Insert(0, g.transform);
            ate = false;
            mainCamera.GetComponent<SpawnFood>().Spawn();
        }
        else if (tail.Count > 0)
        {
            tail.Last().position = v;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.StartsWith("FoodPrefab"))
        {
            ate = true;
            Destroy(collision.gameObject);
            ++score;
            highestScore = Mathf.Max(score, highestScore);
            Debug.Log(highestScore);
            scoreText.text = $"Score: {score}";
            highestScoreText.text = $"Highest score: {highestScore}";
        } else
        {
            // TODO: Lose screen because the snake collides with walls
            gameOverPanel.SetActive(true);
            //CancelInvoke();
            enabled = false;
            score = 0;
        }
    }
}

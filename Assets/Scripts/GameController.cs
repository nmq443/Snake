using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum GameState {
    None,
    Idle,
    Ending
}

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public GameState gameState;
    [SerializeField] PlayerController playerController;
    [SerializeField] Food foodPrefab;
    [SerializeField] BoxCollider2D gridArea;
    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }

    private void OnDestroy() {
        if (Instance == this)
            Instance = null;
    }

    private void Start() {
        NewGame();
    }

    private void NewGame() {
        gameState = GameState.Idle;
        SpawnFood();
    }

    private void Update() {
        if (gameState == GameState.Idle) {
            
        } else if (gameState == GameState.Ending) {
            playerController.gameObject.SetActive(false);
        }
    }

    public void SpawnFood() {
        Bounds bounds = gridArea.bounds;
        int randX = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
        int randY = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));
        Vector3 spawnPos = new Vector3(randX, randY, 0f);
        Instantiate(foodPrefab, spawnPos, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    [SerializeField] Transform segmentTransform;
    private List<Transform> segments;

    private void Start() {
        segments = new List<Transform>();
        segments.Add(transform);
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            direction = Vector2.right;
    }

    private void FixedUpdate() {
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }
        transform.position = new Vector3(Mathf.Round(transform.position.x) + direction.x,
                                         Mathf.Round(transform.position.y) + direction.y, 
                                         0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Food") {
            Destroy(other.gameObject);
            GameController.Instance.SpawnFood();
            Grow();
        } else if (other.tag == "Wall") {
            Debug.Log("You lose");
            GameController.Instance.gameState = GameState.Ending;
        }
    }

    private void Grow() {
        Transform segment = Instantiate(this.segmentTransform);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }
}

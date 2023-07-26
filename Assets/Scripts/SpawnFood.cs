using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    [SerializeField] GameObject food;

    [SerializeField] Transform BorderTop;
    [SerializeField] Transform BorderBottom;
    [SerializeField] Transform BorderLeft;
    [SerializeField] Transform BorderRight;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        int x = (int) Random.Range(BorderLeft.position.x, BorderRight.position.x);
        int y = (int) Random.Range(BorderBottom.position.y, BorderTop.position.y);

        Instantiate(food, new Vector2(x, y), Quaternion.identity);
    }
}

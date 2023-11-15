//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class spring : MonoBehaviour
{
    public BoxCollider2D gridArea;

    public float minTime = 5f;
    public float maxTime = 10f;//生成道具间隔

    private float timer;

    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void Update()
    {
        if (timer <= 0)
        {
            RandomizePosition();
            float randomTime = Random.Range(minTime, maxTime);
            timer = randomTime;
        }
        timer -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomizePosition();
        }
    }

}

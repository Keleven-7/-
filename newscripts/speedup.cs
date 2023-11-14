//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class speedup:MonoBehaviour
{
    public BoxCollider2D gridArea;

    public float minTime = 5f;
    public float maxTime = 10f;//���ɵ��߼��

    private float timer;
    private void DestroyItem()//���ٵ���
    {
            Destroy(GameObject.FindGameObjectWithTag("Item"));
    }

    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void Update()
    {
<<<<<<< HEAD
        if (Time.timeScale == 1)
        {
=======
>>>>>>> 683c966616510ec9ef5353fc0c816e126a1c54d7
            if (timer <= 0)
            {
                RandomizePosition();
                float randomTime = Random.Range(minTime, maxTime);
                Invoke("DestroyItem", randomTime);
                timer = randomTime;
            }
            timer -= Time.deltaTime;
<<<<<<< HEAD
        }

=======
>>>>>>> 683c966616510ec9ef5353fc0c816e126a1c54d7
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomizePosition();
        }
    }

}

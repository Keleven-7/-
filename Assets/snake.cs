//using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 3;
    public float speed = 0.15f;//通过修改fixupdate频率模拟速度
    public bool reverse_value = false; //通过在输入时判断实现反转
    private void Start()
    {
        //_segments = new List<Transform>();
        //_segments.Add(this.transform);
        ResetState();
    }

    private void Update()
    {
        if (reverse_value)//如果反转
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_direction != Vector2.up)
                    _direction = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (_direction != Vector2.down)
                    _direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (_direction != Vector2.left)
                    _direction = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (_direction != Vector2.right)
                    _direction = Vector2.left;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_direction != Vector2.down)
                    _direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (_direction != Vector2.up)
                    _direction = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (_direction != Vector2.right)
                    _direction = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (_direction != Vector2.left)
                    _direction = Vector2.right;
            }
        }
        
    }
    private void FixedUpdate()
    {
        for(int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i-1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
        Time.fixedDeltaTime = speed;//速度模拟
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    private void ResetState()
    {
        for (int i=1;i< _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);

        }
        _segments.Clear();
        _segments.Add(this.transform);

        for(int i=1;i<this.initialSize;i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab)); 
        }

        this.transform.position= Vector3.zero;
    }

    private void end_up()
    {
        speed = 0.15f;
    }

    private void end_reverse()
    {
        reverse_value = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if(other.tag == "Obstacle")
        {
            ResetState();
        }
        else if(other.tag=="speedup")
        {
            speed = speed*2/3;
            CancelInvoke("end_up");//重置道具时间
            Invoke("end_up", 5);//在5s后结束道具效果
        }
        else if(other.tag=="reverse")
        {
            reverse_value = true;
            Invoke("end_reverse", 5);//在5s后结束道具效果
        }
    }
}

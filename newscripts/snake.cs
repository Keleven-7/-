using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    //static Snake instance;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI ScoreText;
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 3;

    public GameObject gameOverUI;

    private void Start()
    {
        //_segments = new List<Transform>();
        //_segments.Add(this.transform);
        ResetState();
    }

    private void Update()
    {
        if(Time.timeScale == 1)
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
        if(Time.timeScale ==1)
        {
            for (int i = _segments.Count - 1; i > 0; i--)
            {
                _segments[i].position = _segments[i - 1].position;
            }

            this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + _direction.x,
                Mathf.Round(this.transform.position.y) + _direction.y,
                0.0f
            );
        }
    }

    private void Grow()
    {
        score++;
        ScoreText.text = "SCORE:" + score;
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);

    }

    public void ResetState()
    {
        //Time.timeScale = 1;
        score = 0;
        ScoreText.text = "SCORE:" + score;
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

    private void s_up()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if(other.tag == "Obstacle")
        {
            GameManager.GameOver(true);
        }
        else if(other.tag=="speedup")
        {
            s_up();
        }
        else if(other.tag=="speedup")
        {
            s_up();
        }
    }
    /*public void Quit()
    {
        //Application.Quit();
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/
}

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
    public float speed = 0.05f;//ͨ���޸�fixupdateƵ��ģ���ٶ�
    public bool reverse_value = false; //ͨ��������ʱ�ж�ʵ�ַ�ת
    public bool collider_value=true;//�ж��Ƿ������ײ���
    public int lenth=1;//�洢���峤��
    private void Start()
    {
        //_segments = new List<Transform>();
        //_segments.Add(this.transform);
        ResetState();
    }

    private void Update()
    {
        if (reverse_value)//�����ת
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
        Time.fixedDeltaTime = speed;//�ٶ�ģ��
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

    private void end_ignore()
    {
        collider_value = true;
        // if() Ԥ���������ж�
    }
    private void end_spring()
    {
        for (int i = 1; i < lenth; i++)
        {
           Grow();//��growʵ�ָֻ�����
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            if(_segments.Count != 1)
            {
                Grow();//�峤Ϊ1ʱ�����жϸõ��ߣ�
            }
                
        }
        else if(other.tag=="other_player")
        {
            //Ԥ��������ɫ��ײ
            /*
            if(collider_value && other_player.collider_value)//��������ײ���ʱ�ж�
            {
                ResetState();
            }
            */

        }
        else if(other.tag == "Obstacle")
        {
            ResetState();
        }
        else if(other.tag=="speedup")
        {
            speed = speed*2/3;
            CancelInvoke("end_up");//���õ���ʱ��
            Invoke("end_up", 5);//��5s���������Ч��
        }
        else if(other.tag=="reverse")
        {
            reverse_value = true;
            Invoke("end_reverse", 5);//��5s���������Ч��
        }
        else if(other.tag== "ignore")
        {
            collider_value = false;//��������������ɫ��ײ
            Invoke("end_ignore", 5);
        }
        else if(other.tag=="spring")
        {
            if(_segments.Count!=1)//����Ϊ1���ǵ���״̬�����ж�
            {
                lenth=_segments.Count;//�洢���峤��

                for (int i = 1; i < _segments.Count; i++)
                {
                    Destroy(_segments[i].gameObject);
                }//��������
                List<Transform> temp = new List<Transform>();//����ʱ�ڵ��ͷ�ڵ�
                temp.Add(_segments[0]);
                _segments.Clear();//����ڵ�
                _segments.Add(temp[0]);//����ͷ���
                temp.Clear(); 

                CancelInvoke("end_spring");//���õ���ʱ��
                Invoke("end_spring", 5);
            }
            
        }
    }
}
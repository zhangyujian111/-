using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brid : MonoBehaviour
{
    private bool isClick = false;  //�Ƿ���
    public float MaxDis = 3;   //����������
    [HideInInspector]
    public SpringJoint2D sj;
    protected Rigidbody2D rb;

    public LineRenderer rightLine;
    public Transform rightPos;
    public LineRenderer leftLine;
    public Transform leftPos;

    public GameObject Boom;


    private bool canMove = true;  //��ֹС���ڷɳ��󻹿��Ա����
    public float smooth = 3;

    private bool isFly = false;

    //��Ч
    public AudioClip birdSelect;  //���С��ʱ����Ч
    public AudioClip birdFly;   //С�����ʱ����Ч

    public Sprite hurt;
    private SpriteRenderer spriteRenderer;



    private void Awake()
    { //��������ʱ������
        sj = GetComponent<SpringJoint2D>(); //��SpringJoint����������俪����رգ�ʵ��С��ĵ���
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    { //��갴��ʱ
        if(canMove)
        {
            AudioPlay(birdSelect);  //���ѡ��С��ʱ��������Ч
            isClick = true;
            rb.isKinematic = true;  //��������ѧ����������ϵͳ����
        }
    }

    private void OnMouseUp()
    { //���̧��ʱ
        if(canMove) 
        {
            isClick = false;
            rb.isKinematic = false;  //�ر�
            Invoke("Fly", 0.1f);  //�����ӳٵ��ã�����1�Ǻ�����������2���ӳٵ��õ�ʱ�䡣
                                  //���û���
            rightLine.enabled = false;
            leftLine.enabled = false;
            canMove = false;
        }

    }

    private void Update()
    {
        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, 10);
            //transform.position += new Vector3(0,0,Camera.main.transform.position.z);

            if(Vector3.Distance(transform.position,rightPos.position) > MaxDis)
            {   //��ȡС�񵽶���ľ��룬��������������õ��������޶��Ļ�
                Vector3 pos = (transform.position - rightPos.position).normalized;  //��ȡ����֮���λ����������һ����ά����,Ȼ��λ��
                pos *= MaxDis; //��󳤶ȵ�����
                transform.position = pos + rightPos.position;  //��С��ľ����޶�����������

            }
            Line();
        }


        //�������
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 2, 15), Camera.main.transform.position.y, Camera.main.transform.position.z), smooth * Time.deltaTime);

        if (isFly)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }

    void Fly()
    {
        isFly = true;

        AudioPlay(birdFly);
        sj.enabled = false;  //��������ã�ʹС����Ե����ȥ
        Invoke("NextBird", 4f);  //�л�С���ʱ�䶨Ϊ����
    }

    void Line()
    {  //���ߺ���
        rightLine.enabled = true;
        leftLine.enabled = true;

        rightLine.SetPosition(0, rightPos.position);
        rightLine.SetPosition(1, transform.position);

        leftLine.SetPosition(0, leftPos.position);
        leftLine.SetPosition(1, transform.position);
    }

    void NextBird()
    {
        //��һֻС����л�
        GameManager.Instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(Boom,transform.position, Quaternion.identity);
        GameManager.Instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip,transform.position);  //�ڵ�ǰλ�ò�����Ч
    }

    //��С����ٲ���
    public virtual void ShowSkill()
    {
        isFly = false;
    }

    public void BirdHurt()
    {
        spriteRenderer.sprite = hurt;
    }
}

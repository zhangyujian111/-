using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brid : MonoBehaviour
{
    private bool isClick = false;  //是否点击
    public float MaxDis = 3;   //限制最大距离
    [HideInInspector]
    public SpringJoint2D sj;
    protected Rigidbody2D rb;

    public LineRenderer rightLine;
    public Transform rightPos;
    public LineRenderer leftLine;
    public Transform leftPos;

    public GameObject Boom;


    private bool canMove = true;  //防止小鸟在飞出后还可以被点击
    public float smooth = 3;

    private bool isFly = false;

    //音效
    public AudioClip birdSelect;  //点击小鸟时的音效
    public AudioClip birdFly;   //小鸟飞行时的音效

    public Sprite hurt;
    private SpriteRenderer spriteRenderer;



    private void Awake()
    { //函数启用时被调用
        sj = GetComponent<SpringJoint2D>(); //绑定SpringJoint组件，控制其开启与关闭，实现小鸟的弹射
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    { //鼠标按下时
        if(canMove)
        {
            AudioPlay(birdSelect);  //鼠标选择小鸟时，播放音效
            isClick = true;
            rb.isKinematic = true;  //开启动力学，不受物理系统控制
        }
    }

    private void OnMouseUp()
    { //鼠标抬起时
        if(canMove) 
        {
            isClick = false;
            rb.isKinematic = false;  //关闭
            Invoke("Fly", 0.1f);  //函数延迟调用，参数1是函数名，参数2是延迟调用的时间。
                                  //禁用画线
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
            {   //获取小鸟到定点的距离，如过超过我们设置的最大距离限定的话
                Vector3 pos = (transform.position - rightPos.position).normalized;  //获取两者之间的位置向量，是一个三维向量,然后单位化
                pos *= MaxDis; //最大长度的向量
                transform.position = pos + rightPos.position;  //将小鸟的距离限定在最大距离内

            }
            Line();
        }


        //相机跟随
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
        sj.enabled = false;  //将组件禁用，使小鸟可以弹射出去
        Invoke("NextBird", 4f);  //切换小鸟的时间定为四秒
    }

    void Line()
    {  //划线函数
        rightLine.enabled = true;
        leftLine.enabled = true;

        rightLine.SetPosition(0, rightPos.position);
        rightLine.SetPosition(1, transform.position);

        leftLine.SetPosition(0, leftPos.position);
        leftLine.SetPosition(1, transform.position);
    }

    void NextBird()
    {
        //下一只小鸟的切换
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
        AudioSource.PlayClipAtPoint(clip,transform.position);  //在当前位置播放音效
    }

    //黄小鸟加速操作
    public virtual void ShowSkill()
    {
        isFly = false;
    }

    public void BirdHurt()
    {
        spriteRenderer.sprite = hurt;
    }
}

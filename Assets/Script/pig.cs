using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig : MonoBehaviour
{
    public float maxSpeed = 10;  //定义最大最小速度
    public float minSpeed = 5;
    private SpriteRenderer sr;
    public Sprite sp;
    public GameObject Boom;
    public GameObject Score;

    public AudioClip pigCollision;
    public AudioClip pigDead;
    public AudioClip birdCollision;

    public bool isPig = false;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {  //碰撞器检测，当两个物体同时挂载碰撞器和刚体时，可检测碰撞

        if(collision.gameObject.tag == "Player")
        {
            AudioPlay(birdCollision);
            collision.transform.GetComponent<Brid>().BirdHurt();
        }

        if(collision.relativeVelocity.magnitude > maxSpeed)
        {  //两者相对速度大于最大速度，猪直接死亡
            Dead();

        }
        else if(collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed)
        {
            sr.sprite = sp;
            AudioPlay(pigCollision);  //播放猪碰撞音效
        }
        
    }

    void Dead()
    {
        if (isPig)
        {
            GameManager.Instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(Boom, transform.position, Quaternion.identity);  //触发爆炸特效

        AudioPlay(pigDead);//播放猪死亡音效

        GameObject pigScore = Instantiate(Score, transform.position + new Vector3(0,0.5f,0),Quaternion.identity);
        Destroy(pigScore, 2f);

    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);  //在当前位置播放音效
    }
}

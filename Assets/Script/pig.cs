using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig : MonoBehaviour
{
    public float maxSpeed = 10;  //���������С�ٶ�
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
    {  //��ײ����⣬����������ͬʱ������ײ���͸���ʱ���ɼ����ײ

        if(collision.gameObject.tag == "Player")
        {
            AudioPlay(birdCollision);
            collision.transform.GetComponent<Brid>().BirdHurt();
        }

        if(collision.relativeVelocity.magnitude > maxSpeed)
        {  //��������ٶȴ�������ٶȣ���ֱ������
            Dead();

        }
        else if(collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed)
        {
            sr.sprite = sp;
            AudioPlay(pigCollision);  //��������ײ��Ч
        }
        
    }

    void Dead()
    {
        if (isPig)
        {
            GameManager.Instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(Boom, transform.position, Quaternion.identity);  //������ը��Ч

        AudioPlay(pigDead);//������������Ч

        GameObject pigScore = Instantiate(Score, transform.position + new Vector3(0,0.5f,0),Quaternion.identity);
        Destroy(pigScore, 2f);

    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);  //�ڵ�ǰλ�ò�����Ч
    }
}

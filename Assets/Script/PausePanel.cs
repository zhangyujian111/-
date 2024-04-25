using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    private Animator anim;
    public GameObject pauseButton;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Pause()
    {    //���pause���󣬲��Ŷ���Ȼ����ͣ��Ϸ
        pauseButton.SetActive(false);  //ʹ��ͣ��ť����
        anim.SetBool("isPause", true);  //������ͣ����
    }

    public void Resume()
    {
        //����������Ժ��Ƚ����Ϸʱ��ֹͣ���ٲ��Ŷ���
        Time.timeScale = 1;  //ʹʱ����Ϸ��ʼ
        anim.SetBool("isPause", false);  //���Ŷ���

    }

    public void Retry()
    {
        Time.timeScale = 1;  //ʹʱ����Ϸ��ʼ
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        Time.timeScale = 1;  //ʹʱ����Ϸ��ʼ
        SceneManager.LoadScene(1);
    }

    public void afterPauseAnim()
    {
        Time.timeScale = 0;  //ʹʱ����Ϸ��ͣ
    }

    public void afterResumeAnim()
    {
        pauseButton.SetActive(true); //ʹ��ͣ��ť����
    }

}

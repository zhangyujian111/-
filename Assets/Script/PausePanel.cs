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
    {    //点击pause键后，播放动画然后暂停游戏
        pauseButton.SetActive(false);  //使暂停按钮隐藏
        anim.SetBool("isPause", true);  //播放暂停动画
    }

    public void Resume()
    {
        //点击继续键以后，先解除游戏时间停止，再播放动画
        Time.timeScale = 1;  //使时间游戏开始
        anim.SetBool("isPause", false);  //播放动画

    }

    public void Retry()
    {
        Time.timeScale = 1;  //使时间游戏开始
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        Time.timeScale = 1;  //使时间游戏开始
        SceneManager.LoadScene(1);
    }

    public void afterPauseAnim()
    {
        Time.timeScale = 0;  //使时间游戏暂停
    }

    public void afterResumeAnim()
    {
        pauseButton.SetActive(true); //使暂停按钮显现
    }

}

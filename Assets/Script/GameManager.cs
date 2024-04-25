using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Brid> birds;
    public List<pig> pigs;
    public static GameManager Instance;
    private Vector3 originPos;

    public GameObject win;
    public GameObject lose;

    public GameObject[] starts;

    private void Awake()
    {
        Instance = this;
        if(birds.Count > 0)
        {
            originPos = birds[0].transform.position;
        }
    }

    private void Start()
    {
        Init();
    }

    //声明初始化
    private void Init()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            if(i == 0)
            {
                birds[i].transform.position = originPos;   
                birds[i].enabled = true;
                birds[i].sj.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sj.enabled = false;
            }
        }
    }

    public void NextBird()
    {
        if(pigs.Count > 0)
        {
            if(birds.Count > 0)
            {
                //切换下一只小鸟
                Init();
            }
            else
            {
                //游戏失败
                lose.SetActive(true);
            }
        }
        else
        {
            //游戏胜利
            win.SetActive(true);
        }
    }

    public void ShowStarts()
    {
        StartCoroutine("show");
    }

    IEnumerator show()
    {
        for (int i = 0; i < birds.Count + 1; i++)
        {
            if(i >= 3)
            {
                break;
            }
            yield return new WaitForSeconds(0.3f);
            starts[i].SetActive(true);
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SceneManager.LoadScene(1);
    }
}

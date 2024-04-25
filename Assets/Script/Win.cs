using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Win : MonoBehaviour
{
    void Show()
    {
        //动画播放完以后显示星星的效果
        GameManager.Instance.ShowStarts();
    }
}

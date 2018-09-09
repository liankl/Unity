
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;  
using UnityEngine.UI;  
  
public class MyMenuUI : MonoBehaviour
{
    //加载界面  
    public GameObject loadPanel;
    //滑动进度条  
    public Image progressImg;
    //进度显示百分比  
    public Text proText;


    //异步加载进程  
    private AsyncOperation async;


    //不断更新的进度值  
    private int curProgressValue = 0;


    void Start()
    {
        StartCoroutine(LoadScene());//调用加载场景的协程  
        loadPanel.SetActive(true);//启用加载界面  

    }

    /// <summary>  
    /// 加载场景协程  
    /// </summary>  
    /// <returns></returns>  
    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync("XinShou");


        async.allowSceneActivation = false;//此处将允许自动场景加载禁用，防止到90%时自动跳转到新场景。  


        yield return async;
    }

    void Update()
    {
        if (async == null)
        {
            return;//进程为空，跳出该函数  
        }


        //总的进度值  
        int progressValue = 100;


        if (curProgressValue < progressValue)
        {
            curProgressValue++;
        }

        proText.text = "Loading " + curProgressValue + "%";//实时更新进度百分比的文本显示  

        progressImg.fillAmount = curProgressValue / 100f;//实时更新滑动进度图片的fillAmount值  

        if (curProgressValue == 100)
        {
            async.allowSceneActivation = true;//启用自动加载场景  

            proText.text = "OK";//文本显示完成OK  

        }
    }

}

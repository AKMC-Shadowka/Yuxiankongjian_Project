using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Main_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start_Button_Click()
    {
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        // 异步加载新场景
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("Level_1", LoadSceneMode.Additive);

        // 等待加载完成
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        // 设置新加载的场景为活动场景
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level_1"));

        // 现在可以安全地卸载主菜单场景
        SceneManager.UnloadSceneAsync("Main_Menu");
    }

}

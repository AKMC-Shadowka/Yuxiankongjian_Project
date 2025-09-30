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
        // �첽�����³���
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("Level_1", LoadSceneMode.Additive);

        // �ȴ��������
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        // �����¼��صĳ���Ϊ�����
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level_1"));

        // ���ڿ��԰�ȫ��ж�����˵�����
        SceneManager.UnloadSceneAsync("Main_Menu");
    }

}

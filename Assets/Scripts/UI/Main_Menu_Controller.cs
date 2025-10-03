using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Main_Menu_Controller : MonoBehaviour
{

    //���˵�UI������

    public Global_Controller Global_Controller_Component;
    public UI_Controller UI_Controller_Component;


    public void Start_Button_Click()
    {
        Global_Controller_Component.Current_Level_Num = 1;
        UI_Controller_Component.UI_Refresh();

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

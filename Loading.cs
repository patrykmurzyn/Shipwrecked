using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Animator transition;

    private readonly float transitionTime = 1f;

    private static bool isDone = false;

    private static float progress = 0f;

    private static int state = 0;

    private static int sceneIndex = 1;

    public void LoadLevel(int index)
    {

        StartCoroutine(LoadAsynchronously(index));

    }

    IEnumerator LoadAsynchronously(int index)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while(!operation.isDone)
        {
            progress = Mathf.Clamp01(operation.progress /.9f);
            slider.value = progress;
            Debug.Log(progress);

            yield return null;
        }
    }

    private void Start()
    {
        slider.value = progress;

        if(state == 0 && !isDone)
        {
            LoadLevel(sceneIndex);
            isDone = true;
            state = 1;
            sceneIndex = 2;
        }
        //else if(state == 1 && !isDone)
        //{
        //    LoadLevel(sceneIndex);
        //    isDone = true;
        //    state = 0;
        //    sceneIndex = 1;
        //}
    }

    public void LoadingGame()
    {
        Box.ClearLists();
        Coin.ClearList();
        Enemy.ClearList();
        Turtle.ClearLists();
        PlayerManage.Clear();

        state = 1;
        sceneIndex = 2;
        progress = 0f;
        slider.value = 0f;
        isDone = false;
        LoadLevel(sceneIndex);
        isDone = true;
        
    }

    public void LoadingMainMenu()
    {
        state = 0;
        sceneIndex = 1;
        progress = 0f;
        slider.value = 0f;
        isDone = false;
        LoadLevel(sceneIndex);
        isDone = true;
    }

    public void LoadPlayAgain()
    {
        Box.ClearLists();
        Coin.ClearList();
        Enemy.ClearList();
        Turtle.ClearLists();
        PlayerManage.Clear();

        state = 1;
        sceneIndex = 2;
        progress = 0f;
        slider.value = 0f;
        isDone = false;
        LoadLevel(sceneIndex);
        isDone = true;
    }

    public void SetIsDoneFalse()
    {
        progress = 0f;
        slider.value = 0f;
        isDone = false;
    }
}

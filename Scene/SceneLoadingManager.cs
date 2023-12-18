using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneLoadingManager : MonoBehaviour
{
    public TextMeshProUGUI loadingText;

    private void Start()
    {
        LoadScene(1); //demo level
    }

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneId);

        //loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            Debug.Log("PROGRESS: " + progressValue);

            loadingText.text = "Loading: " + progressValue * 100 + "%";

            yield return null;
        }
    }
}

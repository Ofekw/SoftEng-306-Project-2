using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadSceneAsync : MonoBehaviour {

    public Slider loadingBar;
    public GameObject loadingImage;

    private AsyncOperation async;

    // scene_id is given by the build settings on the project
    public void ClickAsync(int scene_id)
    {
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(scene_id));
    }

    IEnumerator LoadLevelWithBar(int scene_id)
    {
        async = Application.LoadLevelAsync(scene_id);
        // When scene is done loading in the background 
        while (!async.isDone)
        {
            loadingBar.value = async.progress;
            // Wait a frame before reevaluating this same expression
            yield return null;
        }
    }
}

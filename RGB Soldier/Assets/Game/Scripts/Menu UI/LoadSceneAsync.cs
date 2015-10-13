using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadSceneAsync : MonoBehaviour {

    public Slider loadingBar;
    public GameObject loadingImage;

    private AsyncOperation async;

    // scene_id is given by the build settings on the project
    public void ClickAsync(string scene_name)
    {
        if (GameControl.control.currentGameLevel == 0)
        {
            scene_name = "stage_tutorial";
        }
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(scene_name));
    }

    IEnumerator LoadLevelWithBar(string scene_name)
    {
        async = Application.LoadLevelAsync(scene_name);
        // When scene is done loading in the background 
        while (!async.isDone)
        {
            loadingBar.value = async.progress;
            // Wait a frame before reevaluating this same expression
            yield return null;
        }
    }

    public void LoadSceneAdditive(string scene_name) {
        Application.LoadLevelAdditive(scene_name);
    }
}

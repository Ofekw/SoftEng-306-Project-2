using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadSceneAsync : MonoBehaviour {

    public Slider loadingBar;
    public GameObject loadingImage;


    public float levelStartDelay = 2f;                      //Time to wait before starting level, in seconds.
    private Text stageText;                                 //Text to display current level number.
    private GameObject stageImage;                          //Image to block out level as levels are being set up, background for levelText.
    private int level = 1;                                  //Current level number, expressed in game as "Day 1".
    private bool doingSetup = true;                         //Boolean to check if we're setting up board, prevent Player from moving during setup.





    private AsyncOperation async;

    // scene_id is given by the build settings on the project
    public void ClickAsync(int scene_id)
    {
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(scene_id));
    }


    public void ClickStageAsync(int scene_id)
    {
        async = Application.LoadLevelAsync(scene_id);

        //While doingSetup is true the player can't move, prevent player from moving while title card is up.
        doingSetup = true;

        //Get a reference to our image LevelImage by finding it by name.
        stageImage = GameObject.Find("StageImage");

        //Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
        stageText = GameObject.Find("StageText").GetComponent<Text>();

        //Set the text of levelText to the string "Stage " and append the current level number.
        stageText.text = "Stage " + level;

        //Set levelImage to active blocking player's view of the game board during setup.
        stageImage.SetActive(true);

        //Call the HideLevelImage function with a delay in seconds of levelStartDelay.
        Invoke("HideLevelImage", levelStartDelay);
    }


    //Hides black image used between levels
    void HideLevelImage()
    {
        //Disable the levelImage gameObject.
        stageImage.SetActive(false);
        doingSetup = false;
    }



    //GameOver is called when the player reaches 0 health points
    public void GameOver()
    {
        //Set levelText to display number of levels passed and game over message
        stageText.text = "You have died";

        //Enable black background image gameObject.
        stageImage.SetActive(true);

        //Disable this GameManager.
        enabled = false;
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

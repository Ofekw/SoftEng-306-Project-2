using UnityEngine;
using System.Collections;

public class InGameText : MonoBehaviour
{
    public GameObject gameText;

    public void show_Text()
    {
        Instantiate(gameText, this.transform.position, this.transform.rotation);
    }




}

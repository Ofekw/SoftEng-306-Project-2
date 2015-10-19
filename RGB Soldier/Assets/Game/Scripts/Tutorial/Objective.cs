using UnityEngine;
using System.Collections;

/* Abstract class for all objectives in the tutorial
 */
public abstract class Objective : MonoBehaviour
{
    /* 
     * Returns the description of the tutorial to display on the stage text
     */
    public abstract string getDescription();
    
    /*
     * Checks if the objective is complete
    */
    public abstract bool isCompleted();
    
    /*
     * Starts the objective and enables any buttons that are needed
     */
    public abstract IEnumerator startObjective();
}

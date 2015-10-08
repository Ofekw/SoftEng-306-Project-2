using UnityEngine;
using System.Collections;

public abstract class Objective : MonoBehaviour
{
    public abstract string getDescription();
    public abstract bool isCompleted();
    public abstract IEnumerator startObjective();
}

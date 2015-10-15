using UnityEngine;
using System.Collections;



public class NormalEnemy : BaseEnemy
{
   public override void Update()
    {
        aiModifiers();
        base.Update();
    }


    //hook method
    public void aiModifiers()
    {
        
    }
}

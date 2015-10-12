using UnityEngine;
using System.Collections;



public class Zombie : BaseEnemy
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

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
        int rand = Random.Range(1, 500); // 1 in 500 chance to randomly change direction
        if (rand == 10)
        {
            entityMovement.Flip();
        }
    }
}

using UnityEngine;

public class ColourOrb : CollectibleEntity {

    public override void entityCollected()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //If player walks into orb call collection method
        if (coll.gameObject.CompareTag("Player"))
        {
            entityCollected();
        }
    }
}

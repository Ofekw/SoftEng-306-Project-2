using UnityEngine;

// Enforces these modules to be loaded up with this module when placed on a prefab/game object
[RequireComponent(typeof(EntityMovement))]
public class ColourOrb : CollectibleEntity {

    public EntityMovement entityMovement;
    // Use this for initialization
    void Start()
    {
        this.entityMovement = GetComponent<EntityMovement>();
    }

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

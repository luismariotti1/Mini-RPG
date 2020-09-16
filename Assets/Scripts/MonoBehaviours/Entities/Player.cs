using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public HitPoints hitPoints;
    public HealthBar healthBarPrefab;
    HealthBar healthBar;
    public Inventory inventoryPrefab;
    Inventory inventory;

    void Start()
    {

    }

void OnTriggerEnter2D(Collider2D collision)
{
    if(collision.gameObject.CompareTag("CanBePickedUP"))
    {
        Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
        if(hitObject!=null)
        {
                bool shouldDisapeear = false;
            
                switch(hitObject.itemType)
            {
                case Item.ItemType.COIN:
                        shouldDisapeear = inventory.AddItem(hitObject);
                    break;
                case Item.ItemType.HEALTH:
                        shouldDisapeear = AdjustHitPoints(hitObject.quantity);
                    break;
                default:
                    break;
            }
                if(shouldDisapeear)
                {
                    collision.gameObject.SetActive(false);
                }
        }
    }

}
    public bool AdjustHitPoints(int amount)
    {
        if (hitPoints.value < maxHitpoints)
        {
            hitPoints.value = hitPoints.value + amount;
            return true;
        }
        return false;
    }

    public override void ResetCharacter()
    {
        inventory = Instantiate(inventoryPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
        hitPoints.value = startingHitPoints;
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while(true)
        {
            hitPoints.value = hitPoints.value - damage;
            if(hitPoints.value<=float.Epsilon)
            {
                KillCharacter();
                break;
            }
            if(interval>float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }

    public override void KillCharacter()
    {
        base.KillCharacter();
        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);
    }
}

using System.Collections;
using UnityEngine;
public abstract class Character : MonoBehaviour
{
    public float maxHitpoints;
    public float startingHitPoints;
    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        ResetCharacter();
    }

    public abstract void ResetCharacter();
    public abstract IEnumerator DamageCharacter(int damage, float interval);
}

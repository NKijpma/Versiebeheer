using UnityEngine;
// dmg types          // idee voor game richting maak enemy dmg velocity based 
public enum Damage
{
    smallDamage = 10,
    mediumDamage = 20,
    largeDamage = 30,
    maxDamage = 9999
};

// heal types
public enum Heal
{
    smallHeal = 10,
    mediumHeal = 20,
    largeHeal = 30,
    maxHeal = 9999
};

[System.Serializable]
public class DamageStats
{
    public float invincibilityDuration;
    public float knockbackForce;
    public Damage Damageamount;
}

public class DamageCalc : MonoBehaviour
{
    // calculate knockback
    public float GetKnockbackForce(Damage dmg)
    {
        return Mathf.Abs((int)dmg) * 0.5f;
    }

}
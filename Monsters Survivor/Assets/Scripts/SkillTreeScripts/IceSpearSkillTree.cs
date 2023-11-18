public class IceSpearSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedAttackSpeed;

    public int additionalNumberOfIceSpears;
    public int additionalNumberOfIceSpearPierces;
    public float increasedIceSpearDamage;
    public float increasedBaseIceSpearDamage;
    public float increasedIceSpearRange;
    public float increasedIceSpearSpeed;
    public float increasedIceSpearSpread;

    public float increasedSlowEffect;
    public float increasedSlowChance;
    public float increasedSlowDuration;

    public bool spearsShootInNova;

    public void SpearDamage()
    {
        increasedIceSpearDamage += 0.3f;
    }

    public void TargetForcus()
    {
        increasedIceSpearSpread -= 0.2f;
    }

    public void MoreSpears()
    {
        additionalNumberOfIceSpears += 1;
    }

    public void ManaInfused()
    {
        increasedIceSpearDamage += 0.7f;
        additionalManaCost += 10;
    }

    public void QuantityOverQuality()
    {
        additionalNumberOfIceSpears += 4;
        increasedBaseIceSpearDamage -= 0.2f;
    }

    public void SharpSpears()
    {
        additionalNumberOfIceSpearPierces += 1;
    }

    public void CastSpeed()
    {
        increasedAttackSpeed += 20;
    }

    public void IceBurst()
    {
        additionalNumberOfIceSpears += 2;
        increasedIceSpearSpread += 0.3f;
    }

    public void FreezingSpears()
    {
        additionalNumberOfIceSpearPierces += 2;
        increasedSlowChance += 20;
        increasedSlowDuration += 0.2f;
        increasedSlowEffect += 20;
    }

    public void IceNova()
    {
        additionalNumberOfIceSpears += 4;
        additionalManaCost += 10;
        spearsShootInNova = true;
    }

    public void SpearRange()
    {
        increasedIceSpearRange += 0.2f;
    }

    public void ReducedCosts()
    {
        additionalManaCost -= 5;
    }

    public void SpearSpeed()
    {
        increasedIceSpearSpeed += 0.35f;
    }
    
    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        increasedAttackSpeed = 0;

        additionalNumberOfIceSpearPierces = 0;
        additionalNumberOfIceSpears = 0;
        increasedIceSpearDamage = 0;
        increasedBaseIceSpearDamage = 0;
        increasedIceSpearSpread = 0;
        increasedIceSpearSpeed = 0;
        increasedIceSpearRange = 0;

        increasedSlowEffect = 0;
        increasedSlowChance = 0;
        increasedSlowDuration = 0;

        spearsShootInNova = false;
    }
}

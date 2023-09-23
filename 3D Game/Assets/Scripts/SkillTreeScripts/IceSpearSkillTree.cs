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

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseAttackSpeed(float value)
    {
        increasedAttackSpeed += value;
    }

    public void IncreaseNumberOfIceSpears(int value)
    {
        additionalNumberOfIceSpears += value;
    }

    public void IncreaseIceSpearPierce(int value)
    {
        additionalNumberOfIceSpearPierces += value;
    }

    public void IncreaseIceSpearDamage(float value)
    {
        increasedIceSpearDamage += value;
    }

    public void IncreaseBaseIceSpearDamage(float value)
    {
        increasedBaseIceSpearDamage += value;
    }

    public void IncreaseIceSpearRange(float value)
    {
        increasedIceSpearRange += value;
    }

    public void IncreaseIceSpearSpeed(float value)
    {
        increasedIceSpearSpeed += value;
    }

    public void IncreaseIceSpearSpread(float value)
    {
        increasedIceSpearSpread += value;
    }

    public void IncreaseSlowEffect(float value)
    {
        increasedSlowEffect += value;
    }

    public void IncreaseSlowChance(float value)
    {
        increasedSlowChance += value;
    }

    public void IncreaseSlowDuration(float value)
    {
        increasedSlowDuration += value;
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
    }
}

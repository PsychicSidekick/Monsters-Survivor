public class FrozenOrbSkillTree : SkillTree
{
    public int additionalManaCost;
    public float additionalCooldownTime;

    public float increasedFrozenOrbDuration;
    public float increasedFrozenOrbSpeed;

    public float increasedFrozenOrbSlowEffect;

    public int additionalNumberOfIcicles;
    public int additionalIciclePierce;
    public float increasedIcicleDamage;
    public float increasedBaseIcicleDamage;
    public float increasedIcicleRange;
    public float increasedIcicleSpeed;
    public float increasedIcicleShootRate;

    public float increasedIcicleSlowEffect;
    public float increasedIcicleSlowChance;
    public float increasedIcicleSlowDuration;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseCoolDownTime(float value)
    {
        additionalCooldownTime += value;
    }

    public void IncreaseFrozenOrbDuration(float value)
    {
        increasedFrozenOrbDuration += value;
    }

    public void IncreaseFrozenOrbSpeed(float value)
    {
        increasedFrozenOrbSpeed += value;
    }

    public void IncreaseFrozenOrbSlowEffect(float value)
    {
        increasedFrozenOrbSlowEffect += value;
    }

    public void IncreaseNumberOfIcicles(int value)
    {
        additionalNumberOfIcicles += value;
    }

    public void IncreaseIciclePierce(int value)
    {
        additionalIciclePierce += value;
    }

    public void IncreaseIcicleDamage(float value)
    {
        increasedIcicleDamage += value;
    }

    public void IncreaseBaseIcicleDamage(float value)
    {
        increasedBaseIcicleDamage += value;
    }

    public void IncreaseIcicleRange(float value)
    {
        increasedIcicleRange += value;
    }

    public void IncreaseIcicleSpeed(float value)
    {
        increasedIcicleSpeed += value;
    }

    public void IncreaseIcicleShootRate(float value)
    {
        increasedIcicleShootRate += value;
    }

    public void IncreaseIcicleSlowEffect(float value)
    {
        increasedIcicleSlowEffect += value;
    }

    public void IncreaseIcicleSlowChance(float value)
    {
        increasedIcicleSlowChance += value;
    }

    public void IncreaseIcicleSlowDuration(float value)
    {
        increasedIcicleSlowDuration += value;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        additionalCooldownTime = 0;

        increasedFrozenOrbDuration = 0;
        increasedFrozenOrbSpeed = 0;

        increasedFrozenOrbSlowEffect = 0;

        additionalNumberOfIcicles = 0;
        additionalIciclePierce = 0;
        increasedIcicleDamage = 0;
        increasedBaseIcicleDamage = 0;
        increasedIcicleRange = 0;
        increasedIcicleSpeed = 0;
        increasedIcicleShootRate = 0;

        increasedIcicleSlowEffect = 0;
        increasedIcicleSlowChance = 0;
        increasedIcicleSlowDuration = 0;
    }
}

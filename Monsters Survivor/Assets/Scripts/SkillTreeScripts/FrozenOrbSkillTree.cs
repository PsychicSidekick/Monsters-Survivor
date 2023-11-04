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

    public void IcicleDamage()
    {
        increasedIcicleDamage += 0.3f;
    }

    public void FrozenMana()
    {
        additionalNumberOfIcicles += 2;
        additionalManaCost += 2;
    }

    public void ManaInfused()
    {
        increasedIcicleDamage += 0.5f;
        additionalManaCost += 5;
    }

    public void IceFactory()
    {
        additionalNumberOfIcicles += 3;
        increasedBaseIcicleDamage += 0.3f;
    }

    public void MultiOrb()
    {
        additionalCooldownTime -= 4;
        additionalManaCost -= 20;
    }

    public void IcicleShootRate()
    {
        increasedIcicleShootRate += 0.2f;
    }

    public void Streamlined()
    {
        additionalIciclePierce += 2;
        increasedIcicleRange += 0.3f;
    }

    public void SplittedIcicles()
    {
        additionalNumberOfIcicles += 2;
        increasedBaseIcicleDamage -= 0.1f;
    }

    public void IcicleStorm()
    {
        additionalNumberOfIcicles += 4;
        increasedIcicleSpeed += 0.5f;
    }

    public void ManaEngine()
    {
        additionalManaCost += 15;
        increasedIcicleShootRate += 1;
    }

    public void OrbDuration()
    {
        increasedFrozenOrbDuration += 0.5f;
    }

    public void HeavyOrb()
    {
        increasedFrozenOrbSpeed -= 0.5f;
    }

    public void ReducedCosts()
    {
        additionalManaCost -= 5;
    }

    public void IcicleSlowEffect()
    {
        increasedIcicleSlowEffect += 0.15f;
    }

    public void IcicleSlowDuration()
    {
        increasedIcicleSlowDuration += 1;
    }

    public void IceTrap()
    {
        increasedFrozenOrbSlowEffect += 0.2f;
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

public class FireBlastSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedAttackSpeed;
    public float additionalCooldownTime;

    public float increasedFireBlastDamage;
    public float increasedFireBlastRadius;
    public float increasedFireBlastExpansionTime;

    public float increasedIgniteDamage;
    public float increasedIgniteChance;
    public float increasedIgniteDuration;

    public bool doesNotDealDamageOnHit;
    public bool destroysProjectiles;
    public bool doesNotStopToUseSkill;

    public void BlastDamage()
    {
        increasedFireBlastDamage += 0.2f;
    }

    public void AttackSpeed()
    {
        increasedAttackSpeed += 15;
    }

    public void ManaBurner()
    {
        increasedFireBlastDamage += 0.5f;
        additionalManaCost += 10;
    }

    public void FreedomBlaster()
    {
        doesNotStopToUseSkill = true;
    }

    public void FocusBlasting()
    {
        increasedFireBlastDamage += 0.8f;
        increasedFireBlastRadius -= 0.2f;
    }

    public void BlastArea()
    {
        increasedFireBlastRadius += 0.25f;
    }

    public void IgniteDamage()
    {
        increasedIgniteDamage += 0.4f;
    }

    public void FireParty()
    {
        increasedIgniteDamage += 0.3f;
        increasedFireBlastRadius += 0.3f;
    }

    public void SafetyZone()
    {
        destroysProjectiles = true;
        additionalCooldownTime += 3f;
        increasedFireBlastExpansionTime += 1.5f;
    }

    public void IgnitionSpecialist()
    {
        doesNotDealDamageOnHit = true;
        increasedIgniteChance += 30;
        increasedIgniteDamage += 0.7f;
    }

    public void ReducedCosts()
    {
        additionalManaCost -= 7;
    }

    public void BlastSpeed()
    {
        increasedFireBlastExpansionTime -= 0.15f;
    }

    public void IgniteChance()
    {
        increasedIgniteChance += 15;
    }

    public void IgniteDuration()
    {
        increasedIgniteDuration += 0.5f;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        increasedAttackSpeed = 0;
        additionalCooldownTime = 0;

        increasedFireBlastDamage = 0;
        increasedFireBlastRadius = 0;
        increasedFireBlastExpansionTime = 0;

        increasedIgniteDamage = 0;
        increasedIgniteChance = 0;
        increasedIgniteDuration = 0;

        doesNotDealDamageOnHit = false;
        destroysProjectiles = false;
        doesNotStopToUseSkill = false;
    }
}

public class ChillingGroundSkillTree : SkillTree
{
    public int additionalManaCost;
    public float additionalCooldownTime;

    public float increasedChillingGroundDamage;
    public float increasedBaseChillingGroundDamage;
    public float increasedChillingGroundHealing;
    public float increasedChillingGroundRadius;
    public float increasedChillingGroundDuration;

    public float increasedSlowEffect;

    public bool damageModifiersAffectHealing;
    public bool doesNotSlow;

    public void ChillingGroundDamage()
    {
        increasedChillingGroundDamage += 0.3f;
    }

    public void LargerArea()
    {
        increasedChillingGroundRadius += 0.3f;
    }

    public void ManaEnchanted()
    {
        increasedChillingGroundDamage += 0.4f;
        additionalManaCost += 5;
    }

    public void DangerZone()
    {
        doesNotSlow = true;
        increasedChillingGroundDamage += 1;
    }

    public void ComfortZone()
    {
        damageModifiersAffectHealing = true;
        increasedBaseChillingGroundDamage -= 0.5f;
    }

    public void LingeringChill()
    {
        increasedChillingGroundDuration += 0.25f;
    }

    public void BoneChilling()
    {
        increasedSlowEffect += 10;
    }

    public void MaximumCoverage()
    {
        increasedChillingGroundRadius += 0.5f;
        increasedBaseChillingGroundDamage -= 0.1f;
    }

    public void AbsoluteZero()
    {
        increasedSlowEffect += 30;
        increasedChillingGroundDamage += 0.3f;
    }

    public void EternalWinter()
    {
        increasedChillingGroundDuration += 2;
        additionalCooldownTime += 5;
    }

    public void ReducedCooldown()
    {
        additionalCooldownTime -= 1;
    }

    public void ReducedCosts()
    {
        additionalManaCost -= 5;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        additionalCooldownTime = 0;

        increasedChillingGroundDamage = 0;
        increasedChillingGroundDamage = 0;
        increasedChillingGroundHealing = 0;
        increasedChillingGroundRadius = 0;
        increasedChillingGroundDuration = 0;

        increasedSlowEffect = 0;

        doesNotSlow = false;
        damageModifiersAffectHealing = false;
    }
}

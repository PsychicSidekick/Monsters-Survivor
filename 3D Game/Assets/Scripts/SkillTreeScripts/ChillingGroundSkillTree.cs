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

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseCooldownTime(float value)
    {
        additionalCooldownTime += value;
    }

    public void IncreaseChillingGroundDamage(float value)
    {
        increasedChillingGroundDamage += value;
    }

    public void IncreaseBaseChillingGroundDamage(float value)
    {
        increasedBaseChillingGroundDamage += value;
    }

    public void IncreaseChillingGroundHealing(float value)
    {
        increasedChillingGroundHealing += value;
    }

    public void IncreaseChillingGroundRadius(float value)
    {
        increasedChillingGroundRadius += value;
    }

    public void IncreaseChillingGroundDuration(float value)
    {
        increasedChillingGroundDuration += value;
    }

    public void IncreaseSlowEffect(float value)
    {
        increasedSlowEffect += value;
    }

    public void ToggleDamageModifiersAffectHealing()
    {
        damageModifiersAffectHealing = true;
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

        damageModifiersAffectHealing = false;
    }
}

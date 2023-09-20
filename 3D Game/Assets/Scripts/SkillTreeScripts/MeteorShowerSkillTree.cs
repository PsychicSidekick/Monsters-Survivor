public class MeteorShowerSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedAttackSpeed;
    public float additionalCooldownTime;

    public float increasedMeteorShowerDamage;
    public float increasedBaseMeteorShowerDamage;
    public float increasedMeteorShowerRadius;
    public float increasedMeteorShowerDuration;

    public float increasedIgniteDamage;
    public float increasedIgniteChance;
    public float increasedIgniteDuration;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseAttackSpeed(float value)
    {
        increasedAttackSpeed += value;
    }

    public void IncreaseCooldownTime(float value)
    {
        additionalCooldownTime += value;
    }

    public void IncreaseMeteorShowerDamage(float value)
    {
        increasedMeteorShowerDamage += value;
    }

    public void IncreaseMeteorShowerRadius(float value)
    {
        increasedMeteorShowerRadius += value;
    }

    public void IncreaseMeteorShowerDuration(float value)
    {
        increasedMeteorShowerDuration += value;
    }

    public void IncreaseIgniteDamage(float value)
    {
        increasedIgniteDamage += value;
    }

    public void IncreaseIgniteDuration(float value)
    {
        increasedIgniteDuration += value;
    }

    public void IncreaseIgniteChance(float value)
    {
        increasedIgniteChance += value;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        increasedAttackSpeed = 0;
        additionalCooldownTime = 0;

        increasedMeteorShowerDamage = 0;
        increasedBaseMeteorShowerDamage = 0;
        increasedMeteorShowerRadius = 0;
        increasedMeteorShowerDuration = 0;

        increasedIgniteDamage = 0;
        increasedIgniteChance = 0;
        increasedIgniteDuration = 0;
    }
}

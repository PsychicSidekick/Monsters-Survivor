public class MeteorShowerSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedAttackSpeed;
    public float additionalCooldownTime;

    public float increasedMeteorShowerDamage;
    public float increasedBaseMeteorShowerDamage;
    public float increasedMeteorShowerRadius;
    public float increasedBaseMeteorShowerRadius;
    public float increasedMeteorShowerDuration;

    public float increasedIgniteDamage;
    public float increasedIgniteChance;
    public float increasedIgniteDuration;

    public bool raiseEmission;

    public void LongerDuration()
    {
        increasedMeteorShowerDuration += 0.1f;
    }

    public void ReducedCoolDown()
    {
        additionalCooldownTime -= 1;
    }

    public void ManaInfused()
    {
        increasedMeteorShowerRadius += 0.3f;
        additionalManaCost += 5;
    }

    public void InfiniteMeteors()
    {
        additionalCooldownTime -= 3;
        increasedMeteorShowerDuration -= 1;
    }

    public void Hellfire()
    {
        increasedIgniteDamage += 0.6f;
        increasedIgniteDuration += 0.6f;
        increasedIgniteChance += 20;
        additionalManaCost += 20;
    }

    public void IgniteDamage()
    {
        increasedIgniteDamage += 0.4f;
    }

    public void MeteorDamage()
    {
        increasedMeteorShowerDamage += 0.5f;
    }

    public void ManaMeteor()
    {
        additionalManaCost += 10;
        increasedMeteorShowerDamage += 0.7f;
    }

    public void FireStorm()
    {
        increasedMeteorShowerDamage += 0.25f;
        increasedMeteorShowerDuration += 0.25f;
        additionalCooldownTime += 1;
    }

    public void HeavenlyPillars()
    {
        increasedBaseMeteorShowerRadius -= 0.95f;
        increasedBaseMeteorShowerDamage += 5f;
        additionalCooldownTime += 5;
        raiseEmission = true;
    }

    public void ReducedCosts()
    {
        additionalManaCost -= 5;
    }

    public void IgniteChance()
    {
        increasedIgniteChance += 20;
    }

    public void IgniteDuration()
    {
        increasedIgniteDuration += 0.5f;
    }

    public void LargerArea()
    {
        increasedMeteorShowerRadius += 0.2f;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        increasedAttackSpeed = 0;
        additionalCooldownTime = 0;

        increasedMeteorShowerDamage = 0;
        increasedBaseMeteorShowerDamage = 0;
        increasedMeteorShowerRadius = 0;
        increasedBaseMeteorShowerRadius = 0;
        increasedMeteorShowerDuration = 0;

        increasedIgniteDamage = 0;
        increasedIgniteChance = 0;
        increasedIgniteDuration = 0;

        raiseEmission = false;
    }
}

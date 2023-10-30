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
        increasedMeteorShowerDuration += 0.2f;
    }

    public void ReducedCoolDown()
    {
        additionalCooldownTime -= 1;
    }

    public void InfiniteMeteors()
    {
        additionalCooldownTime -= 4;
        increasedAttackSpeed += 10;
    }

    public void FireStorm()
    {
        increasedMeteorShowerDamage += 0.7f;
        additionalManaCost -= 30;
    }

    public void CastSpeed()
    {
        increasedAttackSpeed += 30;
    }

    public void IgniteDamage()
    {
        increasedIgniteDamage += 0.3f;
    }

    public void MeteorDamage()
    {
        increasedMeteorShowerDamage += 0.2f;
    }

    public void ManaMeteor()
    {
        additionalManaCost += 20;
        increasedMeteorShowerDamage += 0.4f;
    }

    public void HellFire()
    {
        increasedIgniteDamage += 0.6f;
        increasedIgniteDuration += 0.6f;
        increasedIgniteChance += 20;
    }

    public void HeavenlyPillars()
    {
        increasedBaseMeteorShowerRadius -= 0.95f;
        increasedBaseMeteorShowerDamage += 1;
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
        increasedMeteorShowerRadius += 0.3f;
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

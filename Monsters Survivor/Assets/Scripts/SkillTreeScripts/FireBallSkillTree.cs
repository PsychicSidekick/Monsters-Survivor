public class FireBallSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedAttackSpeed;

    public int additionalNumberOfFireBalls;
    public float increasedFireBallDamage;
    public float increasedBaseFireBallDamage;
    public float increasedFireBallRange;
    public float increasedFireBallSpeed;

    public float increasedExplosionDamage;
    public float increasedBaseExplosionDamage;
    public float increasedExplosionRadius;

    public float increasedIgniteDamage;
    public float increasedIgniteChance;
    public float increasedIgniteDuration;

    public bool igniteAppliedByExplosion;

    public void FireBallDamage()
    {
        increasedFireBallDamage += 0.4f;
    }

    public void IgniteDamage()
    {
        increasedIgniteDamage += 0.3f;
    }

    public void FireExposure()
    {
        increasedIgniteChance += 15;
        increasedIgniteDuration += 0.3f;
    }

    public void SniperWizard()
    {
        increasedBaseFireBallDamage += 0.6f;
        additionalManaCost += 10;
        increasedFireBallSpeed += 0.5f;
    }

    public void FireMachineGun()
    {
        increasedAttackSpeed += 30;
        increasedBaseFireBallDamage += 0.1f;
        additionalManaCost += 5;
    }

    public void ExplosionDamage()
    {
        increasedExplosionDamage += 0.3f;
    }

    public void ExplosionRadius()
    {
        increasedExplosionRadius += 0.2f;
    }

    public void ExplosiveSpecialist()
    {
        increasedBaseExplosionDamage += 0.2f;
        increasedExplosionRadius += 0.3f;
        additionalManaCost += 5;
    }

    public void MoreExplosions()
    {
        additionalNumberOfFireBalls += 1;
        increasedBaseFireBallDamage -= 0.3f;
    }

    public void Pyromancer()
    {
        igniteAppliedByExplosion = true;
        increasedIgniteChance += 25;
    }

    public void FireBallSpeed()
    {
        increasedFireBallSpeed += 0.3f;
    }

    public void AttackSpeed()
    {
        increasedAttackSpeed += 15;
    }

    public void ReducedCosts()
    {
        additionalManaCost -= 7;
    }

    public void IgniteChance()
    {
        increasedIgniteChance += 20;
    }

    public void IgniteDuration()
    {
        increasedIgniteDuration += 0.5f;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        increasedAttackSpeed = 0;

        additionalNumberOfFireBalls = 0;
        increasedFireBallDamage = 0;
        increasedBaseFireBallDamage = 0;
        increasedFireBallRange = 0;
        increasedFireBallSpeed = 0;

        increasedExplosionDamage = 0;
        increasedBaseExplosionDamage = 0;
        increasedExplosionRadius = 0;

        increasedIgniteDamage = 0;
        increasedIgniteChance = 0;
        increasedIgniteDuration = 0;

        igniteAppliedByExplosion = false;
    }
}

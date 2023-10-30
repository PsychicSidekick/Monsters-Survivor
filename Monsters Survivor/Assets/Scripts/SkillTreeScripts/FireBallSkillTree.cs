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
        additionalManaCost += 20;
        increasedFireBallSpeed += 0.5f;
    }

    public void FireMachineGun()
    {
        increasedAttackSpeed += 30f;
        increasedBaseFireBallDamage += 0.1f;
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
    }

    public void MoreExplosions()
    {
        additionalManaCost += 10;
        additionalNumberOfFireBalls += 1;
        increasedBaseFireBallDamage -= 0.25f;
    }

    public void Pyromancer()
    {
        igniteAppliedByExplosion = true;
        increasedIgniteChance += 30;
    }

    public void FireBallSpeed()
    {
        increasedFireBallSpeed += 0.3f;
    }

    public void CastSpeed()
    {
        increasedAttackSpeed += 15;
    }

    public void ReducedCosts()
    {
        additionalManaCost -= 5;
    }

    public void IgniteChance()
    {
        increasedIgniteChance += 25;
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

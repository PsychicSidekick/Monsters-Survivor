public class LightningBoltSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedAttackSpeed;

    public int additionalNumberOfLightningBolts;
    public int additionalNumberOfLightningBoltChains;
    public float increasedLightningBoltChainingRange;
    public float increasedLightningBoltDamage;
    public float increasedBaseLightningBoltDamage;
    public float increasedLightningBoltRange;
    public float increasedLightningBoltSpeed;
    public float increasedLightningBoltSpread;

    public float increasedShockEffect;
    public float increasedShockChance;
    public float increasedShockDuration;

    public bool chainsToUser;
    public bool maximumNumberOfLightningBoltsIsOne;

    public void LightningBoltDamage()
    {
        increasedLightningBoltDamage += 0.3f;
    }

    public void ShockEffect()
    {
        increasedShockEffect += 10;
    }

    public void LightningFork()
    {
        additionalNumberOfLightningBolts += 1;
        additionalNumberOfLightningBoltChains -= 1;
    }

    public void EnergyLoss()
    {
        additionalNumberOfLightningBoltChains += 3;
        increasedBaseLightningBoltDamage -= 0.25f;
    }

    public void Amplifier()
    {
        maximumNumberOfLightningBoltsIsOne = true;
    }

    public void MoreChains()
    {
        additionalNumberOfLightningBoltChains += 1;
    }

    public void ShockDuration()
    {
        increasedShockDuration += 0.5f;
    }

    public void ChainingRange()
    {
        increasedLightningBoltChainingRange += 0.3f;
    }

    public void ElectroTherapy()
    {
        chainsToUser = true;
        increasedLightningBoltChainingRange += 1;
    }

    public void MassElectrocution()
    {
        increasedShockChance += 30;
        additionalNumberOfLightningBolts += 1;
        additionalManaCost += 20;
    }

    public void CastSpeed()
    {
        increasedAttackSpeed += 15;
    }

    public void ReducedCosts()
    {
        additionalManaCost -= 5;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        increasedAttackSpeed = 0;

        additionalNumberOfLightningBolts = 0;
        additionalNumberOfLightningBoltChains = 0;
        increasedLightningBoltChainingRange = 0;
        increasedLightningBoltDamage = 0;
        increasedBaseLightningBoltDamage = 0;
        increasedLightningBoltRange = 0;
        increasedLightningBoltSpeed = 0;
        increasedLightningBoltSpread = 0;

        increasedShockChance = 0;
        increasedShockDuration = 0;
        increasedShockEffect = 0;

        chainsToUser = false;
        maximumNumberOfLightningBoltsIsOne = false;
    }
}

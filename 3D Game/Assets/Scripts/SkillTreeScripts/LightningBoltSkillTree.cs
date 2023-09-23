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

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseAttackSpeed(float value)
    {
        increasedAttackSpeed += value;
    }

    public void IncreaseNumberOfLightningBolts(int value)
    {
        additionalNumberOfLightningBolts += value;
    }

    public void IncreaseLightningBoltChains(int value)
    {
        additionalNumberOfLightningBoltChains += value;
    }

    public void IncreaseLightningBoltChainingRange(float value)
    {
        increasedLightningBoltChainingRange += value;
    }

    public void IncreaseLightningBoltDamage(float value)
    {
        increasedLightningBoltDamage += value;
    }

    public void IncreaseBaseLightningBoltDamage(float value)
    {
        increasedBaseLightningBoltDamage += value;
    }

    public void IncreaseLightningBoltRange(float value)
    {
        increasedLightningBoltRange += value;
    }

    public void IncreaselightningBoltSpeed(float value)
    {
        increasedLightningBoltSpeed += value;
    }

    public void IncreaseLightningBoltSpread(float value)
    {
        increasedLightningBoltSpread += value;
    }

    public void IncreaseShockEffect(float value)
    {
        increasedShockEffect += value;
    }

    public void IncreaseShockChance(float value)
    {
        increasedShockChance += value;
    }

    public void IncreaseShockDuration(float value)
    {
        increasedShockDuration += value;
    }

    public void ToggleChainsToUser()
    {
        chainsToUser = true;
    }

    public void ToggleMaximumNumberOfProjectilesIsOne()
    {
        maximumNumberOfLightningBoltsIsOne = true;
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

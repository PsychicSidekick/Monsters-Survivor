public enum DamageType
{
    Fire,
    Cold,
    Lightning
}

public class Damage
{
    public float value;
    public Character origin;
    public DamageType type;

    public Damage(float _value, Character _origin, DamageType _type)
    {
        value = _value;
        origin = _origin;
        type = _type;
    }
}

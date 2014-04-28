public struct Stats
{
    public int HP;
    public int Damage;
    public float GatheringSpeed;
    public float MovementSpeed;

    public Stats(int hp, int damage, float gatheringSpeed, float movementSpeed)
    {
        HP = hp;
        Damage = damage;
        GatheringSpeed = gatheringSpeed;
        MovementSpeed = movementSpeed;
    }

    public static Stats operator +(Stats a, Stats b)
    {
        return new Stats()
        {
            HP = a.HP + b.HP,
            Damage = a.Damage + b.Damage,
            GatheringSpeed = a.GatheringSpeed + b.GatheringSpeed,
            MovementSpeed = a.MovementSpeed + b.MovementSpeed
        };
    }

    public static readonly Stats Zero = new Stats();
    public static readonly Stats Base = new Stats(10, 0, 1f, 1f);
}
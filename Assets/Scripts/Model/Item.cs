public class Item
{
    /// <summary>
    /// Type of this item.
    /// </summary>
    public ItemType Type { get; private set; }

    /// <summary>
    /// Current durability of this item.
    /// </summary>
    public float Durability { get; set; }

    /// <summary>
    /// Durability percentage of this item.
    /// Is +infinity if this item cannot be broken.
    /// Is between 0f and 1f if this item can be broken.
    /// </summary>
    public float DurabilityPercentage { get { return float.IsPositiveInfinity(Durability) ? float.PositiveInfinity : (Durability / Type.MaxDurability); } }

    public Item(ItemType type)
    {
        Type = type;
        Durability = type.MaxDurability;
    }
}
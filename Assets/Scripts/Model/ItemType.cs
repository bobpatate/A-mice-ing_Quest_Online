using System.Linq;

public class ItemType
{
    /// <summary>
    /// Name of this item.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Asset to use.
    /// </summary>
    public string Asset { get; private set; }

    /// <summary>
    /// Slot of this item.
    /// </summary>
    public Slots Slot { get; private set; }

    /// <summary>
    /// Tier of this item.
    /// </summary>
    public int Tier { get; private set; }

    /// <summary>
    /// Max durability of this item.
    /// </summary>
    public float MaxDurability { get; private set; }

    /// <summary>
    /// Bonus stats given by this item.
    /// </summary>
    public Stats Stats { get; private set; }

    private ItemType(string name, string asset, Slots slot, int tier, float maxDurability, Stats stats)
    {
        Name = name;
        Asset = asset;
        Slot = slot;
        Tier = tier;
        MaxDurability = maxDurability;
        Stats = stats;
    }

    /// <summary>
    /// Calculate stats of a player after taking his equipement in account.
    /// </summary>
    public static Stats CalculateStats(ItemType[] equipment)
    {
        Stats stats = Stats.Base;
        foreach (ItemType item in equipment)
        {
            stats += item.Stats;
        }
        return stats;
    }

    // --- DEFINE GAME ITEMS HERE ---

    public static readonly ItemType WeaponT0 = new ItemType("Journal roulé", "todo", Slots.Weapon, 0, float.PositiveInfinity, new Stats(0, 5, 0f, 0f));
    public static readonly ItemType WeaponT1 = new ItemType("Tuyaux plomberie", "todo", Slots.Weapon, 1, 10, new Stats(0, 7, 0f, 0f));
    public static readonly ItemType WeaponT2 = new ItemType("Sledgehammer", "todo", Slots.Weapon, 2, 10, new Stats(0, 11, 0f, 0f));
    public static readonly ItemType WeaponT3 = new ItemType("Lightsaber", "todo", Slots.Weapon, 3, 10, new Stats(0, 15, 0f, 0f));

    public static readonly ItemType ArmorT0 = new ItemType("Barre de censure", "todo", Slots.Armor, 0, float.PositiveInfinity, new Stats(10, 0, 0f, 0f));
    public static readonly ItemType ArmorT1 = new ItemType("Veste pare-balle", "todo", Slots.Armor, 1, 10, new Stats(30, 0, 0f, 0f));
    public static readonly ItemType ArmorT2 = new ItemType("Riot gear", "todo", Slots.Armor, 2, 10, new Stats(60, 0, 0f, 0f));
    public static readonly ItemType ArmorT3 = new ItemType("Power armor", "todo", Slots.Armor, 3, 10, new Stats(100, 0, 0f, 0f));

    public static readonly ItemType ToolT0 = new ItemType("Main sale", "todo", Slots.Tool, 0, float.PositiveInfinity, new Stats(0, 0, 0.1f, 0f));
    public static readonly ItemType ToolT1 = new ItemType("Pelle", "todo", Slots.Tool, 1, 10, new Stats(0, 0, 0.25f, 0f));
    public static readonly ItemType ToolT2 = new ItemType("Super aimant", "todo", Slots.Tool, 2, 10, new Stats(0, 0, 0.50f, 0f));
    public static readonly ItemType ToolT3 = new ItemType("Gravity gun", "todo", Slots.Tool, 3, 10, new Stats(0, 0, 1.0f, 0f));

    public static readonly ItemType BootsT0 = new ItemType("Pieds sales", "todo", Slots.Boots, 0, float.PositiveInfinity, new Stats(0, 0, 0f, 0.0f));
    public static readonly ItemType BootsT1 = new ItemType("Soulier", "todo", Slots.Boots, 1, 60, new Stats(0, 0, 0f, 0.2f));
    public static readonly ItemType BootsT2 = new ItemType("Soulier qui allume", "todo", Slots.Boots, 2, 60, new Stats(0, 0, 0f, 0.4f));
    public static readonly ItemType BootsT3 = new ItemType("Bacorn slippers", "todo", Slots.Boots, 3, 60, new Stats(0, 0, 0f, 2.6f));

    /// <summary>
    /// Available items ingame.
    /// </summary>
    public static readonly ItemType[] List = {
                                                WeaponT0, WeaponT1, WeaponT2, WeaponT3,
                                                ArmorT0, ArmorT1, ArmorT2, ArmorT3,
                                                ToolT0, ToolT1, ToolT2, ToolT3,
                                                BootsT0, BootsT1, BootsT2, BootsT3
                                             };

    public ItemType[] GetAvailableItems(Slots slot)
    {
        return List.Where(i => i.Slot == slot).ToArray();
    }
}
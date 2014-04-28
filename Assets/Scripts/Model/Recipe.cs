public class Recipe
{
    /// <summary>
    /// Slot built by this recipe.
    /// </summary>
    public Slots Slot { get; private set; }

    public float MetalRatio { get; private set; }
    public float TextileRatio { get; private set; }
    public float ElectronicRatio { get; private set; }

    private Recipe(Slots slot, float metal, float textile, float electronic)
    {
        Slot = slot;
        MetalRatio = metal;
        TextileRatio = textile;
        ElectronicRatio = electronic;
    }

    // --- DEFINE GAME RECIPES HERE ---

    public static readonly Recipe Weapon = new Recipe(Slots.Weapon, 0.5f, 0.2f, 0.1f);
    public static readonly Recipe Armor = new Recipe(Slots.Armor, 0.2f, 0.1f, 0.5f);
    public static readonly Recipe Tool = new Recipe(Slots.Tool, 0.1f, 0.5f, 0.2f);
    public static readonly Recipe Boots = new Recipe(Slots.Boots, 0.3f, 0.3f, 0.3f);

    /// <summary>
    /// Possible recipes ingame.
    /// </summary>
    public static readonly Recipe[] List = { Weapon, Armor, Tool, Boots };
}
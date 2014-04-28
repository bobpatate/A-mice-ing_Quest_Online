using UnityEngine;
using System.Linq;

public static class ItemBuilder
{
    /// <summary>
    /// Décide d'un ItemType étant donné l'inventaire fourni.
    /// </summary>
    public static ItemType Pick(Ressource[] inventory)
    {
        // Filtrage des items "vides"
        inventory = inventory.Where(i => !string.IsNullOrEmpty(i.nom)).ToArray();

        Recipe recipe = PickRecipe(inventory);
        int tier = PickTier(inventory);

        return ItemType.List.FirstOrDefault(i => i.Slot == recipe.Slot && i.Tier == tier);
    }

    // --- MAGIE VAUDOU A PARTIR D'ICI ---

    private const int MinTier = 1;
    private const int MaxTier = 3;
    private const float Variance = 0.3f; // Variance tier +/-

    private static int CalculateWeight(Ressource[] inventory, Recipe recipe)
    {
        float metalRatio = (float)inventory.Count(r => r.nom == "Metal") / (float)inventory.Length;
        float electronicRatio = (float)inventory.Count(r => r.nom == "Electronique") / (float)inventory.Length;
        float textileRatio = (float)inventory.Count(r => r.nom == "Textile") / (float)inventory.Length;

        float metalCompatibility = Mathf.Pow(1.0f - Mathf.Abs(metalRatio - recipe.MetalRatio), 2f);
        float electronicCompatibility = Mathf.Pow(1.0f - Mathf.Abs(electronicRatio - recipe.ElectronicRatio), 2f);
        float textileCompatibility = Mathf.Pow(1.0f - Mathf.Abs(textileRatio - recipe.TextileRatio), 2f);

        float compatibility = metalCompatibility * electronicCompatibility * textileCompatibility;

        //Debug.Log(string.Format("Slot: {0} / Compability: {1}", recipe.Slot, compatibility));

        return Mathf.Max(1, Mathf.FloorToInt(100 * compatibility));
    }

    private static Recipe PickRecipe(Ressource[] inventory)
    {
        Recipe[] recipes = Recipe.List;
        int[] weights = new int[recipes.Length];
        for (int i = 0; i < recipes.Length; i++)
        {
            weights[i] = CalculateWeight(inventory, recipes[i]);
        }
        return Utility.PickWeightedRandom<Recipe>(recipes, weights);
    }

    private static int PickTier(Ressource[] inventory)
    {
        int sumTier = inventory.Sum(r => r.tier);
        float tier = ((float)sumTier) / ((float)inventory.Length);

        //Debug.Log("Tier: " + tier);

        return Mathf.RoundToInt(Mathf.Clamp(tier + Random.Range(-Variance, Variance), MinTier, MaxTier));
    }
}
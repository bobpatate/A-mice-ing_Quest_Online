using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerEquipment : MonoBehaviour {
    public int sword { get { return items[(int)Slots.Weapon].Type.Tier; } }
    public int armor { get { return items[(int)Slots.Armor].Type.Tier; } }
    public int boots { get { return items[(int)Slots.Boots].Type.Tier; } }
    public int gathering { get { return items[(int)Slots.Tool].Type.Tier; } }

    public float swordDurability { get { return items[(int)Slots.Weapon].DurabilityPercentage; } }
    public float armorDurability { get { return items[(int)Slots.Armor].DurabilityPercentage; } }
    public float bootsDurability { get { return items[(int)Slots.Boots].DurabilityPercentage; } }
    public float gatheringDurability { get { return items[(int)Slots.Tool].DurabilityPercentage; } }

    private Item[] items = new Item[4];

	void Start () {
        items[(int)Slots.Weapon] = new Item(ItemType.WeaponT0);
        items[(int)Slots.Armor] = new Item(ItemType.ArmorT0);
        items[(int)Slots.Boots] = new Item(ItemType.BootsT0);
        items[(int)Slots.Tool] = new Item(ItemType.ToolT0);
        this.UpdateStats(true);
	}

    private void UpdateStats(bool resetHP)
    {
        Stats stats = ItemType.CalculateStats(items.Select(i => i.Type).ToArray());

        PlayerCombat playerCombat = GetComponent<PlayerCombat>();
        playerCombat.strength = stats.Damage;
        playerCombat.maxhp = stats.HP;
		if (resetHP || playerCombat.hp > playerCombat.maxhp) {
			playerCombat.hp = playerCombat.maxhp;
		}

        PlayerInventory playerInventory = GetComponent<PlayerInventory>();
		playerInventory.collectingSpeed = stats.GatheringSpeed;

        PlayerController playerController = GetComponent<PlayerController>();
        playerController._speed = playerController._maxVelocityChange = 5.0f * stats.MovementSpeed;
    }

    public void UseWeapon(int quantity)
    {
        this.Use(Slots.Weapon, quantity);
    }

    public void UseArmor(int quantity)
    {
        this.Use(Slots.Armor, quantity);
    }

    public void UseTool(int quantity)
    {
        this.Use(Slots.Tool, quantity);
    }

    public void GiveItem(ItemType item)
    {
        int slot = (int)item.Slot;
        if (item.Tier >= items[slot].Type.Tier)
        {
            items[slot] = new Item(item);
            this.UpdateStats(false);
        }
    }

    public void Update()
    {
        this.Use(Slots.Boots, Time.deltaTime);
    }

    private void Use(Slots slot, float quantity)
    {
        Item item = items[(int)slot];
        item.Durability = Mathf.Max(0, item.Durability - quantity);

        if (item.Durability == 0)
        {
            items[(int)slot] = new Item(ItemType.List.FirstOrDefault(i => i.Slot == slot && i.Tier == 0));
        }
		UpdateStats (false);
    }
}

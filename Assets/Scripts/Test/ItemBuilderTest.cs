using UnityEngine;
using System.Collections;

public class ItemBuilderTest : MonoBehaviour {
    void Start()
    {
        Test();
    }

    private static Ressource metalTestT0 = new Ressource() { nom = "metal", tier = 0 };
    private static Ressource metalTestT1 = new Ressource() { nom = "metal", tier = 1 };
    private static Ressource metalTestT2 = new Ressource() { nom = "metal", tier = 2 };
    private static Ressource metalTestT3 = new Ressource() { nom = "metal", tier = 3 };
    private static Ressource electronicTestT0 = new Ressource() { nom = "electronic", tier = 0 };
    private static Ressource electronicTestT1 = new Ressource() { nom = "electronic", tier = 1 };
    private static Ressource electronicTestT2 = new Ressource() { nom = "electronic", tier = 2 };
    private static Ressource electronicTestT3 = new Ressource() { nom = "electronic", tier = 3 };
    private static Ressource textileTestT0 = new Ressource() { nom = "textile", tier = 0 };
    private static Ressource textileTestT1 = new Ressource() { nom = "textile", tier = 1 };
    private static Ressource textileTestT2 = new Ressource() { nom = "textile", tier = 2 };
    private static Ressource textileTestT3 = new Ressource() { nom = "textile", tier = 3 };

    private static Ressource[] inventoryTest1 = new Ressource[] {
       metalTestT2,
       metalTestT0,
       metalTestT2,
       metalTestT1,
       metalTestT1,
       electronicTestT3,
       textileTestT1,
    };

    public static void Test()
    {
        for (int i = 0; i < 10; i++)
        {
            ItemType type = ItemBuilder.Pick(inventoryTest1);

            Debug.Log(string.Format("{0}. Chosen item: {1}", i, type.Name));
        }
    }
}
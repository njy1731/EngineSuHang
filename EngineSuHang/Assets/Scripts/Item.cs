using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType 
    { 
        Dagger,
        Katana,
        GreatSword,
        Tomahawk,
        Zweihander
    }

    public static int GetCost (ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Dagger: return 80;
            case ItemType.Katana: return 120;
            case ItemType.GreatSword: return 150;
            case ItemType.Tomahawk: return 170;
            case ItemType.Zweihander: return 200;
        }
    }

    public static Sprite GetSpite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Dagger: return GameAssets.i.s_Dagger;
            case ItemType.Katana: return GameAssets.i.s_Katana;
            case ItemType.GreatSword: return GameAssets.i.s_GreatSword;
            case ItemType.Tomahawk: return GameAssets.i.s_Tomahawk;
            case ItemType.Zweihander: return GameAssets.i.s_Zweihander;
        }
    }
}

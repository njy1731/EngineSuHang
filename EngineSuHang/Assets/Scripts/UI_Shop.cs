using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : MonoBehaviour
{
    private PlayerCtrl player;
    private Transform container;
    private Transform shopItemTemplate;
    private void Awake()
    {
        //player = GetComponent<PlayerCtrl>();
        //player.HpText.gameObject.SetActive(false);
        //player.StrengthText.gameObject.SetActive(false);
        //player.SpeedText.gameObject.SetActive(false);
        container = transform.Find("container");
        shopItemTemplate = transform.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateItemButton(Item.GetSpite(Item.ItemType.Dagger), "Dagger", Item.GetCost(Item.ItemType.Dagger), 0);
        CreateItemButton(Item.GetSpite(Item.ItemType.Katana), "Katana", Item.GetCost(Item.ItemType.Katana), 0);
    }

    private void CreateItemButton(Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 30f;
        shopItemRectTransform.anchorMin = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System;

public class UIProduct : MonoBehaviour  
{   
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private Image Icon;
    [SerializeField] private TextMeshProUGUI PriceText;
    [SerializeField] private Button PurchaseButton;

    private Product Model;
    public delegate void PurchaseEvent(Product Model, Action OnComplete);
    public event PurchaseEvent OnPurchase;

    public void Setup(Product Product){

        Model = Product;
        TitleText.SetText(Product.metadata.localizedTitle);

        string formattedPrice = $"{Product.metadata.isoCurrencyCode}{Product.metadata.localizedPrice}";
        PriceText.SetText(formattedPrice);

        Texture2D texture = ShopIconLoader.GetIcon(Product.definition.id);

        if (texture != null)
        {
            Sprite sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                Vector2.one * 0.5f // Using Vector2.one * 0.5f as the pivot point
            );

            Icon.sprite = sprite; // the Image component
        }

        else
        {
            Debug.LogError($"No Sprite found for {Product.definition.id}!");
        }

    }

    public void Purchase(){

        PurchaseButton.enabled = false;
        OnPurchase?.Invoke(Model, HandlePurchaseComplete);
 
    }

    private void HandlePurchaseComplete(){

        PurchaseButton.enabled = true;
    }


}
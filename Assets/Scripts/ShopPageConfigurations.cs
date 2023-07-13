using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
public class ShopPageConfigurations : MonoBehaviour, IStoreListener  
{
    [SerializeField] private UIProduct UIProductPrefab;
    [SerializeField] private Transform ShopContentPanel;
    [SerializeField] private GameObject LoadingOverlay;
    private IStoreController StoreController;
    private IExtensionProvider ExtensionProvider;
    private Action OnPurchaseCompleted;


    [System.Obsolete]
    private async void Awake()
    {
        InitializationOptions options = new InitializationOptions()
            
            .SetEnvironmentName("production");

        await UnityServices.InitializeAsync(options);
        ResourceRequest operation = Resources.LoadAsync<TextAsset>("IAPProductCatalog");
        operation.completed += HandleIAPCatalogLoaded;


    }

    [System.Obsolete]
    private void HandleIAPCatalogLoaded(AsyncOperation Operation){

        ResourceRequest request = Operation as ResourceRequest; 

        Debug.Log($"Loaded Asset: {request.asset}");   
        ProductCatalog catalog = JsonUtility.FromJson<ProductCatalog>((request.asset as TextAsset).text);
        Debug.Log($"Loaded Catalog with {catalog.allProducts.Count} items");

        //? For testing
        //! Remove or comment before building the APK for production or release
        
        StandardPurchasingModule.Instance().useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
        StandardPurchasingModule.Instance().useFakeStoreAlways = true;

        //! Remove or comment before building the APK for production or release

#if UNITY_ANDROID

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(
            StandardPurchasingModule.Instance(AppStore.GooglePlay)

        );

#elif UNITY_IOS

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(
            StandardPurchasingModule.Instance(AppStore.AppleAppStore)

        );
        
#else
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(
            StandardPurchasingModule.Instance(AppStore.NotSpecified)

        );

#endif 
        foreach (ProductCatalogItem item in catalog.allProducts)
        {
            builder.AddProduct(item.id, item.type);
        }
        UnityPurchasing.Initialize(this, builder);

    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        StoreController = controller;
        ExtensionProvider = extensions;

        ShopIconLoader.Initialize(StoreController.products);
        ShopIconLoader.OnLoadComplete += HandleAllIconsLoaded;
    }

    private void HandleAllIconsLoaded(){

        StartCoroutine(ArrangeUI());
    }

    private IEnumerator ArrangeUI(){

        List<Product> sortedProducts = StoreController.products.all
            .OrderBy(item => item.metadata.localizedPrice)
            .ToList();

        foreach (Product product in sortedProducts)
        {
            UIProduct uIProduct = Instantiate(UIProductPrefab);
            uIProduct.OnPurchase += HandlePurchase;
            uIProduct.Setup(product);
            uIProduct.transform.SetParent(ShopContentPanel.transform, false);
            yield return null; // Wait for next frame before adding more products so they don't overlap each
        }
    }

    private void HandlePurchase(Product product, Action OnPurchaseCompleted){

        LoadingOverlay.SetActive(true);
        this.OnPurchaseCompleted = OnPurchaseCompleted;
        StoreController.InitiatePurchase(product);

    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"Error initializing IAP becasue of {error}." + 
            $"\r\n Show a message to the player dep[ending on the error.");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError($"Failed to purchase {product.definition.id} because {failureReason}");
        OnPurchaseCompleted?.Invoke();
        
        OnPurchaseCompleted = null;
        LoadingOverlay.SetActive(false);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {

        Debug.Log("<color=green>Successfully purchased " + purchaseEvent.purchasedProduct.definition.id + "</color>");
        OnPurchaseCompleted?.Invoke();
        
        OnPurchaseCompleted = null;
        LoadingOverlay.SetActive(false);

        //! Add mechanic to guive player currency or item as the case maybe
        //? Also ask Soeren how we plan to store the data for when the player loads the app

        return PurchaseProcessingResult.Complete;
    }

}

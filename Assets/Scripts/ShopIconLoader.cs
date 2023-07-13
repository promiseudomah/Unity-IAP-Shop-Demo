using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class ShopIconLoader
{
    public static Dictionary<string, Texture2D> Icons{

        get;
        private set;

    } = new();

    private static int TargetIconCount;

    public delegate void LoadComppleteAction();
    public static event LoadComppleteAction OnLoadComplete;

    
    public static void Initialize(ProductCollection Products){

        if(Icons.Count == 0){

            Debug.Log($"Loading Shop icons for {Products.all.Length} products."); 
            TargetIconCount = Products.all.Length;

            foreach (Product product in Products.all)
            {   
                string path = $"ShopIcons/{product.definition.id}";
                path = path.Trim();
                Debug.Log($"Loading Shop icon at {path}");
                ResourceRequest operation = Resources.LoadAsync<Texture2D>(path);
                operation.completed += HandleLoadIcon;

            }
        }
        else{

            Debug.LogError("ShopIconLoader has alaready been initialized!");
        }
    }

    public static Texture2D GetIcon(string id){

        if(Icons.Count == 0){

            Debug.LogError("Called ShopIconLoader.GetIcon before Initailizing!" +
            "This isn't supported");

            throw new InvalidOperationException("ShopIconLoader.GetIcon() cannot be called before initializing ShopIconLoader");
        }

        else{

            Icons.TryGetValue(id, out Texture2D icon);
            return icon;
        }
    }

    private static void HandleLoadIcon(AsyncOperation Operation){

        ResourceRequest request = Operation as ResourceRequest; 
        if(request.asset != null){

            Icons.Add(request.asset.name, request.asset as Texture2D);
            Debug.Log($"Successfullly loaded {request.asset.name}, Icons.Count = {Icons.Count}");
            
            if(Icons.Count == TargetIconCount){

                OnLoadComplete?.Invoke();
            }   
        }

        else{
            
            //Something failed to load
            TargetIconCount--;
        }

    }


}


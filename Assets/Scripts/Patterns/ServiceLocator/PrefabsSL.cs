using UnityEngine;

public class PrefabsServiceLocator: ServiceLocator<string, GameObject>
{
    // public override void Init(){
    //     var gameObjects = Resources.LoadAll("Prefabs");

    //     foreach (var item in gameObjects)
    //     {
    //         var go = item as GameObject;
    //         AddService(item.name, go);
    //     }
    // }
}
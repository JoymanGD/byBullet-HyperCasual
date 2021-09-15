using UnityEngine;
using Waldem.Unity.Events;

public class TapScreenController : MonoBehaviour
{
    [SerializeField] ClassicEvent OnShow, OnHide;
    
    public void Show(){
        OnShow?.Invoke();
    }

    public void Hide(){
        OnHide?.Invoke();
    }
}

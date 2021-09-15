using UnityEngine;
using Waldem.Unity.Events;

public class TapScreenModel : MonoBehaviour
{
    [SerializeField] FloatEvent OnShow;
    [SerializeField] ClassicEvent OnHide;
    [SerializeField] float BlinkingSpeed, HideSpeed;

    public void Show(){
        OnShow?.Invoke(BlinkingSpeed);
    }

    public void Hide(){
        OnHide?.Invoke();
    }
}
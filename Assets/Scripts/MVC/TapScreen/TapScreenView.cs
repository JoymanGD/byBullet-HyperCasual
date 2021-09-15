using TMPro;
using UnityEngine;
using Waldem.Unity.Events;
using DG.Tweening;

public class TapScreenView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TapText;
    [SerializeField] GameObject TapPanel;

    public void Show(float _blinkingSpeed){
        TapPanel.SetActive(true);
        TapText.DOFade(1, _blinkingSpeed).SetLoops(-1, LoopType.Yoyo);
    }

    public void Hide(){
        TapText.DOKill();
        TapText.alpha = 0;
        TapPanel.SetActive(false);
    }
}

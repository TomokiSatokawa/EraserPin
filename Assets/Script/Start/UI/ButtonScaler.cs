using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening; // © –Y‚ê‚¸‚É

public class ButtonScalerTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected float scaleUp = 1.1f;       // Šg‘å”{—¦
    protected float duration = 0.2f;      //
                                          //ƒAƒjƒ[ƒVƒ‡ƒ“‚ÌŠÔ

    private Vector3 originalScale;
    private Tween currentTween;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        // Šù‘¶‚ÌTween‚ª‚ ‚ê‚Î~‚ß‚é
        currentTween?.Kill();

        // Šg‘åTween
        currentTween = transform.DOScale(originalScale * scaleUp, duration).SetEase(Ease.OutBack);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        // k¬Tween
        currentTween?.Kill();

        currentTween = transform.DOScale(originalScale, duration).SetEase(Ease.OutBack);
    }
}

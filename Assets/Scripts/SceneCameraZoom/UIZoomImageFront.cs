using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIZoomImageFront : MonoBehaviour, IScrollHandler
{
    private Vector3 initalScale;

    public float ZoomSpeed = 0.1f;

    public float maxZoom = 10f;

    public Image BackImage;

    private UIZoomImage BackProperties;

    private void Awake()
    {
        initalScale = transform.localScale;

        if (BackImage != null)
        {
            BackProperties = BackImage.GetComponent<UIZoomImage>();
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        var delta = Vector3.one * (eventData.scrollDelta.y * ZoomSpeed);
        var desiredScale = transform.localScale + delta;

        desiredScale = ClampDesiredScale(desiredScale);

        transform.localScale = desiredScale;

        if (BackImage != null)
        {
            if (transform.localScale.z < 2.4f)
            {
                BackImage.SendMessage("OnScroll", eventData, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                BackProperties.ZoomSpeed = 0.01f;
                gameObject.GetComponent<Image>().raycastTarget = false;
            }
        }
    }

    private Vector3 ClampDesiredScale(Vector3 desiredScale)
    {
        desiredScale = Vector3.Max(initalScale, desiredScale);
        desiredScale = Vector3.Min(initalScale * maxZoom, desiredScale);
        
        return desiredScale;
    }
}
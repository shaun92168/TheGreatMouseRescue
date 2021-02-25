using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIZoomImage : MonoBehaviour, IScrollHandler
{
    private Vector3 initalScale;
    public float ZoomSpeed = 0.1f;
    public float maxZoom = 10f;

    public CS_PageMgr CS_PgMgr;

    private void Awake()
    {
        initalScale = transform.localScale;
    }

    public void OnScroll(PointerEventData eventData)
    {
        var delta = Vector3.one * (eventData.scrollDelta.y * ZoomSpeed);
        var desiredScale = transform.localScale + delta;

        desiredScale = ClampDesiredScale(desiredScale);

        transform.localScale = desiredScale;
    }

    private Vector3 ClampDesiredScale(Vector3 desiredScale)
    {
        desiredScale = Vector3.Max(initalScale, desiredScale);
        desiredScale = Vector3.Min(initalScale * maxZoom, desiredScale);
        
        return desiredScale;
    }

    public void AnimationEvent(int eventID)
    {
        if (CS_PgMgr != null)
        {
            // Forward message to manager
            CS_PgMgr.AnimationEvent(eventID);
        }

//        Debug.Log("PrintEvent: " + eventID + " called at: " + Time.time);
    }
}
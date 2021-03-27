using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class VectorLerp : MonoBehaviour
{
    public Transform startTrans;
    public Transform endTrans;
    [SerializeField]
    [Range(0f, 1f)]
    private float lerpPct = 0.5f;
    public Vector3 updatedEndTrans;
    public Quaternion movementVector;
    private void Awake()
    {
        updatedEndTrans = new Vector3(endTrans.position.x, endTrans.position.y + 10.0f, endTrans.position.z);
        movementVector = new Quaternion(endTrans.position.x - startTrans.position.x, endTrans.position.y - startTrans.position.y, endTrans.position.z - startTrans.position.y, 
            lerpPct);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startTrans.position, updatedEndTrans, lerpPct);
        transform.rotation = Quaternion.Lerp(startTrans.rotation, movementVector, lerpPct);
    }
}

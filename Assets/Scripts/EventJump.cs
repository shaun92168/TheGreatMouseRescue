using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EventJump : MonoBehaviour
{
    // Apply force
    public Rigidbody mouseRigid;
    public float speed = 4f;
    public Transform target;
/*    public float t;
    public float forceUpward;
    public float force;*/

    // Spot the player when enter
    // Ask for jump 
    public Color SpotColor;
    public Text label;
    public bool canJump = false;

    // Attempt for Lerp
    public Transform startTrans;
    public Transform endTrans;
    [SerializeField]
    [Range(0f, 1f)]
    public Quaternion movementVector;
    public Vector3 positionToMoveTo;

    private void Awake()
    {
        label.text = "";        
    }


    // Lerp to target position

    void Start()
    {
        positionToMoveTo = endTrans.position;
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = startTrans.position;

        while (time < duration)
        {
            startTrans.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        startTrans.position = targetPosition;
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.O) && canJump)
        {
            FindObjectOfType<AudioManager>().Play("jump");

            // Attempt for Lerp
            StartCoroutine(LerpPosition(positionToMoveTo, 2));

            // Attempt for Adding force
            /*    // Jump: Add force to the rigid. 
                // f is the vector towards the target
                Vector3 f = target.position - transform.position;
                f = f.normalized;
                f = f * force;
                mouseRigid.AddForce(f);*/
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canJump = true;
            label.text = "Press [O] to Jump!!!";
            label.fontSize = 70;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            label.text = "";
        }
    }


}

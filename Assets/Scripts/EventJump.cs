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

    // Spot the player when enter
    // Ask for jump 
    public Color SpotColor;
    public Text label;
    public bool canJump = false;
    public GameObject goJumpImage;

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
        canJump = false;
        goJumpImage.SetActive(false);
        goJumpImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.7f);
    }


    // Lerp to target position
    void Start()
    {
        positionToMoveTo = endTrans.position;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && canJump)
        {
            //FindObjectOfType<AudioManager>().Play("jump");

            // Attempt for Lerp
            StartCoroutine(LerpPosition(positionToMoveTo, 1));

            // Attempt for Adding force
            /*    // Jump: Add force to the rigid. 
                // f is the vector towards the target
                Vector3 f = target.position - transform.position;
                f = f.normalized;
                f = f * force;
                mouseRigid.AddForce(f);*/
        }
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canJump = true;
            label.text = "";
            label.fontSize = 70;
            goJumpImage.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            label.text = "";
            goJumpImage.SetActive(false);
        }
        canJump = false;
    }
}

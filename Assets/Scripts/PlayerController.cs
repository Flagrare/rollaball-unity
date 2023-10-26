using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI scoreCountText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private int scoreCount;
    private GameObject[] pickUps;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreCount = 0;
        pickUps = GameObject.FindGameObjectsWithTag(GeneralVariables.Tags.PickUp);

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue momentValue)
    {
        Vector2 movementVector = momentValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }


    void SetCountText()
    {
        scoreCountText.text = $"Count: {scoreCount}";
        if (scoreCount >= pickUps.Length)
        {
            winTextObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GeneralVariables.Tags.PickUp))
        {
            other.gameObject.SetActive(false);
            scoreCount++;
            SetCountText();
        }
    }
}

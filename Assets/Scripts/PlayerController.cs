using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float moveX;
    private float moveY;
    private Rigidbody rb;
    public float speed;
    public GameObject explosionPrefab;
    private int count=0;
    public TextMeshProUGUI score;
    public TextMeshProUGUI finalScore;
    public GameObject winScreen;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetScore();
        winScreen.SetActive(false);
    }

    void OnMove(InputValue moveValue)
    {
        moveX = moveValue.Get<Vector2>().x;
        moveY = moveValue.Get<Vector2>().y;
    }
    void SetScore()
    {
        score.text = "Score: " + count;
        if(count==8)
        {
            score.enabled = false;
            gameObject.SetActive(false);
            winScreen.SetActive(true);
            finalScore.text = count + " Points";
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveX, 0, moveY);
        rb.AddForce(movement*speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            Destroy(other.gameObject);
            Instantiate(explosionPrefab, other.gameObject.transform.position, Quaternion.identity);
            count++;
            SetScore();
        }
        
    }
}

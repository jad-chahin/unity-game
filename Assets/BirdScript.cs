using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float flapStrength;
    private LogicScript logic;
    public bool birdIsAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            rb.velocity = Vector2.up * flapStrength;
            startFlap();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ceiling
        if (collision.gameObject.layer == 6)
        {
            return;
        }
        gameOver();
    }

    private void gameOver()
    {
        logic.gameOver();
        birdIsAlive = false;
    }

    private void startFlap()
    {
        StartCoroutine(flap());
    }

    private IEnumerator flap()
    {
        Transform wing = transform.Find("Wing");
        wing.localScale = new Vector3(wing.localScale.x, wing.localScale.y * -1, wing.localScale.z);

        yield return new WaitForSeconds(0.5f);

        wing.localScale = new Vector3(wing.localScale.x, wing.localScale.y * -1, wing.localScale.z);
    }
}

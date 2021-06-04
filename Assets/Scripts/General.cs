using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    [SerializeField] bool canMove = false;
    [SerializeField] float maxDistanceDelta = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveTowardToTheTarget(FindObjectOfType<Player>().transform);
        }
    }

    private bool TouchedTheBoat
    {
        get
        {
            return GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Boat"));
        }
    }

    private void MoveTowardToTheTarget(Transform boat)
    {
        if (TouchedTheBoat)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            return;
        }

        float step = maxDistanceDelta * Time.deltaTime;
        var movingToward = Vector3.MoveTowards(transform.position, boat.position, step);
        transform.position = movingToward;
    }

    public void Unlock()
    {
        canMove = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}

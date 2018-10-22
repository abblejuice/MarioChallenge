using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour
{
    private Animator animator;
    public float speed;
    public LayerMask isGround;
    public Transform wallHitBox;
    private bool wallHit;

    public float wallHitWidth;
    public float wallhitHeight;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
    }
	
	void FixedUpdate ()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallhitHeight), 0, isGround);
        if (wallHit)
            speed *= -1;
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallHitBox.position, new Vector3(wallHitWidth, wallhitHeight, 1));
    }
}

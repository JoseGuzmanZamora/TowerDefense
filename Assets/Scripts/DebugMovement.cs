using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D> ();
    }

    private void FixedUpdate() {
        var horizontalPress = Input.GetAxis("Horizontal");
        var verticalPress = Input.GetAxis("Vertical");
        var newPosition = new Vector3(horizontalPress, verticalPress, transform.position.z).normalized * (movementSpeed * Time.deltaTime);
        rb.MovePosition(transform.position + newPosition);
    }
}

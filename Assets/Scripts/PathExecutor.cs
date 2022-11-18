using UnityEngine;

public class PathExecutor : MonoBehaviour
{
    public int movementSpeed = 3;
    public PathDeterminator pathInstructionsSetup;
    public int instructionCounter = 0;
    private int instructionsAmount = 0;
    private Rigidbody2D rb;
    public float differenceThreshold = 0.1f;
    public Vector2 deltaMovement = Vector2.zero;

    private void Start() {
        rb = GetComponent<Rigidbody2D> ();
    }

    void FixedUpdate()
    {
        if (pathInstructionsSetup.instructionsReady)
        {
            instructionsAmount = pathInstructionsSetup.instructions.Count;
            var currentInstruction = pathInstructionsSetup.instructions[instructionCounter];
            var difference = currentInstruction - transform.position;
            var absDifference = new Vector3(Mathf.Abs(difference.x), Mathf.Abs(difference.y), difference.z);
            var newPosition = new Vector3(difference.x, difference.y, difference.z) * (movementSpeed * Time.fixedDeltaTime);
            var originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, currentInstruction, movementSpeed * Time.fixedDeltaTime);
            // Figure out delta movement
            deltaMovement.x = transform.position.x - originalPosition.x;
            deltaMovement.y = transform.position.y - originalPosition.y;

            // re adjust if reached destination
            if (absDifference.x <= differenceThreshold && absDifference.y <= differenceThreshold)
            {
                if (instructionCounter < instructionsAmount - 1)
                {
                    // Move on to the second instruction
                    instructionCounter++;
                }
                else
                {
                    // reached destination
                }
            }
        }
    }
}

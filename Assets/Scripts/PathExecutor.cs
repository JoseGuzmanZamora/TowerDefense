using UnityEngine;

public class PathExecutor : MonoBehaviour
{
    public int movementSpeed = 3;
    public PathDeterminator pathInstructionsSetup;
    public int instructionCounter = 0;
    private int instructionsAmount = 0;
    private Rigidbody2D rb;
    public int differenceThreshold = 1;

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
            transform.position = Vector3.MoveTowards(transform.position, currentInstruction, movementSpeed * Time.fixedDeltaTime);
            //rb.MovePosition(transform.position + newPosition);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementAnimationController : MonoBehaviour
{
    private Animator anim;
    private PathExecutor movementExecutor;
    private bool isFront = false;
    private bool isBack = false;
    private bool isSide = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        movementExecutor = gameObject.GetComponent<PathExecutor>();
    }

    // Update is called once per frame
    void Update()
    {
        var xDirection = movementExecutor.deltaMovement.x;
        var xDirectionAbs = Mathf.Abs(xDirection);
        var yDirection = movementExecutor.deltaMovement.y;
        var yDirectionAbs = Mathf.Abs(yDirection);

        if (xDirectionAbs > yDirectionAbs)
        {
            // We have an horizontal movement
            isSide = true;
            isBack = false;
            isFront = false;
            // See if we have to flip the sprite
            if (xDirection > 0)
            {
                gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-0.7f, 0.7f, 1f);
            }
        }
        else
        {
            // We have a vertical movement
            if (yDirection > 0)
            {
                // We have an upwards movement
                isBack = true;
                isSide = false;
                isFront = false;
            }
            else
            {
                // We have a downwards movement
                isFront = true;
                isSide = false;
                isBack = false;
            }
        }

        // Either way set the bools
        anim.SetBool("IsFront", isFront);
        anim.SetBool("IsSide", isSide);
        anim.SetBool("IsBack", isBack);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public LayerMask groundLayer;

    private Animator animator;
    private Rigidbody rigidbody;

    private Vector3 targetPos;
    private bool isMoving = false;
    private float range = 5f;
    private float speed = 2f;

    private const float timeToResetTarget = 5f;
    private float currentTargetTime = 0f;

    private const float moveCooldown = 1.5f;
    private float currentMoveCooldown = 0f;

    private Vector3 jumpForce = new Vector3(0, 15, 0);
    private const float jumpCooldown = 1f;
    private float currentJumpCooldown = 0f;


    private void Awake() {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        if(currentJumpCooldown > 0)
            currentJumpCooldown -= Time.deltaTime;

        if(isMoving) {
            currentTargetTime += Time.deltaTime;

            if (timeToResetTarget > currentTargetTime) {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

                RaycastHit hitInfo;
                if (Physics.Raycast(transform.position, -transform.right, out hitInfo, 1.3f, groundLayer)) {
                    if (hitInfo.distance < 1.2f)
                        Jump();
                }

                if (Mathf.Abs(transform.position.x - targetPos.x) < .2f) {
                    isMoving = false;
                    animator.SetTrigger("Idle");
                    animator.ResetTrigger("Run");
                    currentTargetTime = 0f;
                }
            }
            else {
                isMoving = false;
                currentTargetTime = 0f;
            }
        }

        
        if (!isMoving) {
            currentMoveCooldown += Time.deltaTime;
            
            if(moveCooldown < currentMoveCooldown) {
                currentMoveCooldown = 0f;
                MoveToNewPosition();
            }
        }
    }

    private void MoveToNewPosition() {
        isMoving = true;
        animator.SetTrigger("Run");
        animator.ResetTrigger("Idle");


        var currentPos = transform.position;

        targetPos = new Vector3(
            Random.Range(currentPos.x - range, currentPos.x + range), 
            currentPos.y,
            Random.Range(currentPos.z - range, currentPos.z + range));

        transform.LookAt(targetPos);
        transform.rotation = Quaternion.Euler(new Vector3(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + 90,
            transform.rotation.eulerAngles.z));
    }

    private void Jump() {
        if(currentJumpCooldown <= 0) {
            Debug.Log("Jump");
            rigidbody.AddForce(transform.right * .5f, ForceMode.Impulse);
            rigidbody.AddForce(jumpForce, ForceMode.Impulse);
            rigidbody.AddForce(-transform.right * 2, ForceMode.Force);

            currentJumpCooldown = jumpCooldown;
        }
    }
}
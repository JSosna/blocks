using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Material redWool;

    private Inventory inventory;

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

    private int health = 3;
    private bool alive = true;


    private void Awake() {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    internal void SetInventory(Inventory inventory) {
        this.inventory = inventory;
    }

    void Update() {
        if (!alive) return;

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
            rigidbody.AddForce(transform.right * .5f, ForceMode.Impulse);
            rigidbody.AddForce(jumpForce, ForceMode.Impulse);
            rigidbody.AddForce(-transform.right * 2, ForceMode.Force);

            currentJumpCooldown = jumpCooldown;
        }
    }

    public void ReduceHealth(Vector3 otherPosition) {
        if (inventory == null) return;

        inventory.AddItem(new Item { itemType = ItemType.Wool, amount = 1 });

        health--;
        GetComponent<ParticleSystem>().Play();

        // Knockback
        Vector3 direction = (transform.position - new Vector3(otherPosition.x, transform.position.y - 5, otherPosition.z)).normalized;
        rigidbody.AddForce(direction * 10, ForceMode.Impulse);

        if (health <= 0) {
            inventory.AddItem(new Item { itemType = ItemType.Mutton, amount = 2 });

            alive = false;

            transform.Find("Cube").GetComponent<SkinnedMeshRenderer>().materials = new Material[] { redWool, redWool, redWool, redWool, redWool, redWool };

            transform.rotation = Quaternion.Euler(new Vector3(
            transform.rotation.eulerAngles.x + 90,
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z));

            GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 5);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float movementSpeed;
    public float turningSpeed;
    public float turretTurningSpeed;
    public float shootingCooldown;

    public Transform turret;
    public Transform muzzle;
    public GameObject projectile;

    private Rigidbody rb;
    private Camera mainCamera;
    private float maxRayDistance = 100f;
    private int floorMask;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        t = 0f;
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        floorMask = LayerMask.GetMask("Floor");
    }

    private void Update()
    {
        if (t <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject proj = Instantiate(projectile, muzzle.position, muzzle.rotation);
                proj.GetComponent<Projectile>().shooterTag = tag;
                t = shootingCooldown;
            }
        }
        else
        {
            t -= Time.deltaTime;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Sy�te
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        // K��ntyminen
        if(inputHorizontal != 0)
        {
            Vector3 turning = Vector3.up * inputHorizontal * turningSpeed;
            rb.angularVelocity = turning;
        }

        // Liikkuminen
        if(inputVertical != 0)
        {
            Vector3 movement = transform.forward * inputVertical * movementSpeed;
            rb.velocity = movement;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, maxRayDistance, floorMask))
        {
            Vector3 targetDirection = hit.point - turret.position;
            targetDirection.y = 0f;
            Vector3 turningDirection = Vector3.RotateTowards(turret.forward, targetDirection, turretTurningSpeed * Time.deltaTime, 0f);
            turret.rotation = Quaternion.LookRotation(turningDirection);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [Header("# Jump")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpPower;
    private PlayerController controller;
    private Rigidbody rigid;
    private Vector2 curMoveDir;
    private bool isJumpping;
    private RaycastHit slopeHit;
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        controller.onMoveEvent += SetMoveEventDir;
        controller.OnJumpEvent += Jump;
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private bool IsSlope() // Todo: 경사로 버그 수정
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        
        if(Physics.Raycast(ray,out slopeHit, 2f, groundLayer) && slopeHit.normal.y < 1)
        {
            return true;
        }
        return false;
    }
    private void Move()
    {
        Vector3 dir = Vector3.zero;
        // 앞뒤 + 좌우로 플레이어를 움직임
        dir = transform.forward * curMoveDir.y + transform.right * curMoveDir.x;
        dir *= moveSpeed;

        if (isJumpping || rigid.velocity.y < 0)
        {
            dir.y = rigid.velocity.y;
        }

        rigid.useGravity = !IsSlope();
        
        rigid.velocity = dir;
    }

    private void SetMoveEventDir(Vector2 dir)
    {
        curMoveDir = dir;
    }

    private void Jump()
    {
        if (!isJumpping)
        {
            rigid.AddForce(Vector2.up *jumpPower , ForceMode.Impulse);
            isJumpping = true;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (isJumpping && GameManager.Instance.IsLayerMatched(groundLayer.value, other.gameObject.layer))
        {
            isJumpping = false;
        }
    }
   
}

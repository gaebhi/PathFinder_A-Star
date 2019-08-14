using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public LayerMask HitLayers;
    public float MoveSpeed = 10f;
    public float RotateSpeed = 5f;
    [HideInInspector]
    public Vector3 TargetPosition;

    [HideInInspector]
    public Node CurrentNode;
    [HideInInspector]
    public Node NextNode;

    void Awake()
    {
        TargetPosition = this.transform.position;
    }

    void Update()
    {
        InputHandle();

        if (CurrentNode != null && NextNode != null)
        {
            if (CurrentNode == NextNode)
            {
                return;
            }
            MoveToNextNode();
            LookAtNextNode();
        }
    }

    private void InputHandle()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Touch _touch = Input.GetTouch(0);
                SetTarget(_touch.position);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 _pos = Input.mousePosition;
                SetTarget(_pos);
            }
        }
    }

    private void SetTarget(Vector3 _pos)
    {
        Ray _ray = Camera.main.ScreenPointToRay(_pos);
        RaycastHit _hit;
        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, HitLayers))
        {
            TargetPosition = _hit.point;
        }
    }

    private void SetTarget(Vector2 _pos)
    {
        Ray _ray = Camera.main.ScreenPointToRay(_pos);
        RaycastHit _hit;
        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, HitLayers))
        {
            TargetPosition = _hit.point;
        }
    }

    private void MoveToNextNode()
    {
        Vector3 _direction = (NextNode.Position - CurrentNode.Position);
        _direction.Normalize();
        transform.Translate(_direction * MoveSpeed * Time.deltaTime, Space.World);
    }

    private void LookAtNextNode()
    {
        Vector3 _direction = (NextNode.Position - CurrentNode.Position);

        Quaternion _lookRot = Quaternion.identity;
        if (_direction != Vector3.zero)
        {
             _lookRot = Quaternion.LookRotation(_direction);
        }

        float _magnitude = _direction.magnitude;

        if (_magnitude < 0.1f)
            return;

        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRot, Mathf.Clamp01(Time.deltaTime * RotateSpeed));
    }
}


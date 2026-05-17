using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    private Vector3 _offset;
    private Vector3 _smoothDampVelocity;
    [SerializeField]
    private float _smoothTargetTime;

    private void Awake()
    {
        _offset = transform.position - _player.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }


    private void MoveCamera()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _player.position + _offset, ref _smoothDampVelocity, _smoothTargetTime);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    public AimBaseState currentState;
    public HipFireState hip = new HipFireState();
    public AimState aim = new AimState();

    [SerializeField] float mouseSense = 1;
    float xAxis, yAxis;
    [SerializeField] Transform camFallowPos;

    [HideInInspector] public Animator anim;

    [Header("Aim Zoom Camera")]
    [HideInInspector] public CinemachineVirtualCamera vCam;
    public float adsFov = 40;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float currentFov;
    public float fovSmoothSpeed = 10;

    [Header("Weapon")]
    public Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20;
    [SerializeField] LayerMask aimMask;

    void Start()
    {
        aimPos = GameObject.Find("AimPosition").transform;
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        anim = GetComponent<Animator>();
        SwitchState(hip);
    } 

    
    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -50, 50);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            
        currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        //Mouse Cam fallow
        camFallowPos.localEulerAngles = new Vector3(yAxis, camFallowPos.localEulerAngles.y, camFallowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}

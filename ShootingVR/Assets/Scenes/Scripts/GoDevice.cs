using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDevice : MonoBehaviour {

    [SerializeField]
    private Transform rightHandAnchor;
    [SerializeField]
    private Transform leftHandAnchor;
    [SerializeField]
    private float maxDistance = 100f;
    [SerializeField]
    private bool isTestMode;
    private bool isHoldTrigger;
    private bool isHoldBack;
    private PhaseManager phaseManager;
    public bool _isHoldTrigger
    {
        get
        {
            return isHoldTrigger;
        }
    }
    public bool _isHoldBack
    {
        get
        {
            return isHoldBack;
        }
    }

    public Transform goDevice
    {
        get
        {
            var controller = OVRInput.GetActiveController();
            if (controller == OVRInput.Controller.RTrackedRemote)
                return rightHandAnchor;
            else if (controller == OVRInput.Controller.LTrackedRemote)
                return leftHandAnchor;

            return null;
        }
    }

    // Use this for initialization
    void Start()
    {
        isHoldTrigger = false;
        isHoldBack = false;
        phaseManager = GameObject.Find("GameMaster").GetComponent<PhaseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        var pointer = goDevice;
        if (pointer == null && !isTestMode)
            return;

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.Space))
            isHoldTrigger = true;
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyUp(KeyCode.Space))
            isHoldTrigger = false;

        if (OVRInput.GetDown(OVRInput.Button.Back) || Input.GetKeyDown(KeyCode.Return))
            isHoldBack = true;
        if (OVRInput.GetUp(OVRInput.Button.Back) || Input.GetKeyUp(KeyCode.Return))
            isHoldBack = false;
    }
}

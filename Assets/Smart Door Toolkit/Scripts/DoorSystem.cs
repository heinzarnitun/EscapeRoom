using System.Collections;
using UnityEngine;

namespace FMS_SmartDoorToolkit
{
    [RequireComponent(typeof(HingeJoint))]
    public class DoorSystem : MonoBehaviour
    {
        public enum DoorType { Basic, KeyBased, PinBased }
        public DoorType doorType = DoorType.Basic;

        [Tooltip("Manually set the pivot point (anchor) for the hinge joint.")]
        public Vector3 anchorPosition = Vector3.zero;

        [Tooltip("Manually set the axis of rotation for the hinge joint.")]
        public Vector3 hingeAxis = Vector3.up;

        [Tooltip("Angle (in degrees) the door should open.")]
        public float openAngle = 90f;

        [Tooltip("Motor force to move the door.")]
        public float motorForce = 100f;

        [Tooltip("Motor speed (degrees per second).")]
        public float motorSpeed = 90f;

        private HingeJoint hinge;
        private bool isOpen = false;

        [Header("Key-Door System")]
        public KeyItem requiredKey;

        [Header("Pin-Based Door Settings")]
        public string correctPin = "213";
        private string enteredPin = "";
        private bool doorUnlocked = false;

        void Start()
        {
            hinge = GetComponent<HingeJoint>();

            hinge.anchor = anchorPosition;
            hinge.axis = hingeAxis;

            JointLimits limits = new JointLimits();
            limits.min = 0;
            limits.max = openAngle;
            hinge.limits = limits;
            hinge.useLimits = true;

            JointMotor motor = new JointMotor();
            motor.force = motorForce;
            motor.targetVelocity = 0;
            hinge.motor = motor;
            hinge.useMotor = false;
        }

        public void Interact()
        {
            if (doorType == DoorType.Basic)
            {
                ToggleDoor();
            }
            else if (doorType == DoorType.KeyBased)
            {
                if (KeyInventory.Instance.HasKey(requiredKey))
                {
                    ToggleDoor();
                }
                else
                {
                    Debug.Log("Door is locked! Equip the correct key.");
                }
            }
            else if (doorType == DoorType.PinBased)
            {
                if (doorUnlocked)
                {
                    ToggleDoor();
                }
            }
        }

        public void ToggleDoor()
        {
            if (doorType == DoorType.PinBased)
            {
                Debug.Log("Door movement disabled for PinBased doors.");
                return;
            }

            isOpen = !isOpen;

            JointMotor motor = hinge.motor;
            motor.targetVelocity = isOpen ? motorSpeed : -motorSpeed;
            hinge.motor = motor;
            hinge.useMotor = true;
        }

        public void EnterPinDigit(string digit)
        {
            if (doorType == DoorType.PinBased)
            {
                if (enteredPin.Length < correctPin.Length)
                {
                    enteredPin += digit;
                    Debug.Log("Current PIN: " + enteredPin);
                }
            }
        }

        public void ClearPin()
        {
            enteredPin = "";
            Debug.Log("PIN cleared.");
        }

        public bool CheckPinMatch()
        {
            if (enteredPin == correctPin)
            {
                doorUnlocked = true;
                ClearPin();
                Debug.Log("Correct PIN entered.");
                return true;
            }
            else
            {
                ClearPin();
                Debug.Log("Incorrect PIN entered.");
                return false;
            }
        }

        private void DeductMinute()
        {
            Debug.Log("One minute deducted!");
            // Add your minute deduction logic here
        }

        public string GetCurrentPin()
        {
            return enteredPin;
        }

        public void SetEnteredPin(string pin)
        {
            enteredPin = pin;
            Debug.Log("Entered PIN set to: " + enteredPin);
        }

        public void LockDoor()
        {
            if (doorUnlocked)
            {
                Debug.Log("Locked");
                doorUnlocked = false;
                if (isOpen)
                {
                    ToggleDoor();
                }
            }
        }
    }
}

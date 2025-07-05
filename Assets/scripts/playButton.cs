using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayButtonInteraction : MonoBehaviour
{
    private XRBaseInteractable interactable;

    [Header("UI")]
    public GameObject ticTacToePanel;
    public Transform playerHead;
    public float panelDistance = 2.0f;

    [Header("Croupier Character")]
public Animator croupierAnimator;

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();

        if (interactable == null)
        {
            Debug.LogError("[PlayButtonInteractionTester] No XRBaseInteractable found!");
            return;
        }

        interactable.selectEntered.AddListener(OnPlayButtonSelected);

        if (ticTacToePanel != null)
            ticTacToePanel.SetActive(false);
    }

    private void OnDestroy()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnPlayButtonSelected);
    }

    private void OnPlayButtonSelected(SelectEnterEventArgs args)
    {
        Debug.Log("PlayButton pressed by: " + args.interactorObject.transform.name);

        if (ticTacToePanel != null && playerHead != null)
        {
            // Parent the panel to the head
            ticTacToePanel.transform.SetParent(playerHead);

            // Position it a bit in front of the face
            ticTacToePanel.transform.localPosition = new Vector3(0f, 0f, panelDistance);

            // Make it face the same direction as the camera
            ticTacToePanel.transform.localRotation = Quaternion.identity;

            // Show the panel
            ticTacToePanel.SetActive(true);
        }
                croupierAnimator.SetTrigger("Yell");

    }
}

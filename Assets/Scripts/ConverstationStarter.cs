using DialogueEditor;
using System.Collections;
using TMPro;
using UnityEngine;

public class ConverstationStarter : MonoBehaviour
{

    [SerializeField] private NPCConversation myConversation;
    [SerializeField] private float conversationDuration = 10f; // Change this value to 5f for 5 seconds
    [SerializeField] private TextMeshProUGUI interactiveText; 

    private Coroutine closeConversationCoroutine;

    private void Start()
    {
        if (interactiveText != null)
        {
            interactiveText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the interactive text
            if (interactiveText != null)
            {
                interactiveText.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartConversation();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide the interactive text
            if (interactiveText != null)
            {
                interactiveText.gameObject.SetActive(false);
            }
        }
    }

    private void StartConversation()
    {
        ConversationManager.Instance.StartConversation(myConversation);
        if (closeConversationCoroutine != null)
        {
            StopCoroutine(closeConversationCoroutine);
        }
        closeConversationCoroutine = StartCoroutine(CloseConversationAfterDelay(conversationDuration));
    }

    private IEnumerator CloseConversationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ConversationManager.Instance.EndConversation();
    }
}

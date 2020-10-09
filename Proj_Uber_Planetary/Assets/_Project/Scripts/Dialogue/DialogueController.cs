﻿using Febucci.UI;
using TMPro;
using UberPlanetary.Quests;
using UberPlanetary.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        private RideManager _rideManager;
        
        // Dialogue Box objects
        public TextMeshProUGUI custName;
        public TextMeshProUGUI dialogueBox;
        public GameObject dialogueWindow;
        public Image custFace;

        public TextAnimator textAnimator;
        private TextAnimatorPlayer textAnimatorPlayer;

        [SerializeField]
        private CustomerSO customerSO;
        [SerializeField]
        private DialogueSO dialogueSO;
        public DialogueTrigger dialogueTrigger;

        public bool isStarted;
        public bool IsShowing { get; set; }

        public bool hasDialogue => dialogueSO != null;

        private int _lineIndex;
        
        private void Awake()
        {
            dialogueTrigger = GetComponentInParent<DialogueTrigger>();
            textAnimatorPlayer = textAnimator.GetComponent<TextAnimatorPlayer>();
            _rideManager = FindObjectOfType<RideManager>();
            ToggleDialogueBox(false);
        }

        private void Start()
        {
            _rideManager.onCustomerPickedUp.AddListener(StartDialogue);
            _rideManager.onCustomerDroppedOff.AddListener(EndDialogue);
        }

        public void StartDialogue(CustomerSO customerData)
        {
            customerSO = customerData;
            dialogueSO = customerSO.CustomerDialogue;
            ToggleDialogueBox(true);

            InitiateDialogue();
        }

        public void EndDialogue(CustomerSO customerData)
        {
            ClearCustomerData();
            ClearDialogueBox();
            ToggleDialogueBox(false);
        }

        public void ToggleDialogueBox(bool state)
        {
            dialogueWindow.SetActive(state);
            dialogueBox.gameObject.SetActive(state);
        }

        public void ClearDialogueBox()
        {
            dialogueBox.text = "";
        }

        public void ClearCustomerData()
        {
            customerSO = null;
            dialogueSO = null;
            custName.text = null;
            custFace.sprite = null;
        }
        
        private void Update()
        {

            if (IsShowing || !isStarted || !hasDialogue) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_lineIndex >= dialogueSO.lines.Length - 1)
                {
                    FinishDialogue();
                    return;
                }
                DisplayText(dialogueSO.lines[++_lineIndex]);
            }
        }
        public void InitiateDialogue()
        {
            isStarted = true;
            custName.text = customerSO.CustomerName;
            custFace.color = new Color(1, 1, 1, 1);
            custFace.sprite = customerSO.CustomerFace;
            textAnimatorPlayer.ShowText(dialogueSO.lines[0]);
        }
        public void DisplayText(string textToDisplay)
        {
            textAnimatorPlayer.ShowText(textToDisplay);
            //Debug.Log(_lineIndex + " comparing to: " + dialogueSO.lines.Length);
        }
        public void FinishDialogue()
        {
            isStarted = false;
            ToggleDialogueBox(false);
        }
    }
}

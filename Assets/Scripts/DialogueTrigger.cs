using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    protected bool triggered = false;

    public void TriggerDialogue(){
        if(!triggered)
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        triggered = true;
    }
}

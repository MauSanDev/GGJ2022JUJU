using UnityEngine;

public class NoteInteractionAction : AbstractInteractionAction
{
    [SerializeField] private LoreNote loreNote;
    
    public override void OnPlayerTrigger(Collider2D other)
    {
        ShowTextBehaviour.Instance.ShowText(loreNote.note);
        LevelData.NotesCollected++;
    }
}

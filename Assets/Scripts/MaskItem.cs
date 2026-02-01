using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Mask")]
public class MaskItem : ScriptableObject
{
    public Sprite image;
    // none of the masks should be stackable as there is only one slot and only one can occupy it at a time.
    public bool stackable = false;
}

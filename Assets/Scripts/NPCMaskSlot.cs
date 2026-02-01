using UnityEngine;

public class NPCMaskSlot : MonoBehaviour
{
    private SpriteRenderer maskRenderer;
    public MaskItem wornMask;

    void Awake()
    {
        Transform maskSlot = transform.Find("MaskSlot");
        if (!maskSlot)
        {
            Debug.LogWarning("MaskSlot not found in NPCMaskSlot");
            return;
        }
        maskRenderer = maskSlot.GetComponent<SpriteRenderer>();
        maskRenderer.enabled = false;
    }

    void Start()
    {
        if (wornMask != null)
        {
            GetComponent<NPCMaskSlot>().WearMask(wornMask);
        }
    }

    public void WearMask(MaskItem maskItem)
    {
        wornMask = maskItem;
        maskRenderer.sprite = maskItem.image;
        maskRenderer.enabled = true;
    }

    // when NPC is "defeated" the mask is removed
    public void RemoveMask(MaskItem maskItem)
    {
        wornMask = null;
        maskRenderer.sprite = null;
        maskRenderer.enabled = false;
    }
}
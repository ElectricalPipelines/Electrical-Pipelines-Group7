using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTarget : MonoBehaviour
{
    public string Name { get; set; } = "snaptarget";
    public string Zone { get; set; } = "";
    public string Index { get; set; } = "";
    Snap snap;
    SpriteRenderer spriteR;
    Sprite sprite;
    public bool inSnap = false;
    public bool isElectric = false;
    // Start is called before the first frame update
    void Start()
    {
        Index = gameObject.name.Split('_')[1];
        Name = gameObject.name.Split('_')[0];
        Zone = gameObject.name.Split('_')[2];
        GameObject goZone = GameObject.FindGameObjectWithTag("Zone_" + Zone);
        Transform snapInZone = goZone.transform.Find("snap_" + Index + "_" + Zone);
        if (snapInZone == null) Debug.LogError("Snap not found in zone: " + Zone);
        snap = snapInZone.GetComponent<Snap>();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        sprite = spriteR.sprite;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (snap != null)
        {
            snap.setPos(this);
            if (snap.IsSnapped)
            {
                inSnap = true;
            }
            else
            {
                inSnap = false;
            }
        }
        //if(!inSnap || !isElectric) SetSprite();
    }

    public void SetSprite()
    {
        if (inSnap && isElectric)
        {
            Sprite newSprite = null;
            if (Name.ToLower().Contains("corner"))
            {
                newSprite = SpriteManager.Instance.GetElectricSprite("on_corner");
            }
            if (Name.ToLower().Contains("straight"))
            {
                newSprite = SpriteManager.Instance.GetElectricSprite("on_straight");
            }
            if (Name.ToLower().Contains("tpipe"))
            {
                newSprite = SpriteManager.Instance.GetElectricSprite("on_tpipe");
            }
            if (Name.ToLower().Contains("cross"))
            {
                newSprite = SpriteManager.Instance.GetElectricSprite("on_cross");
            }
            spriteR.sprite = newSprite;
        }
        else
        {
            Sprite newSprite = null;
            if (Name.ToLower().Contains("corner"))
            {
                newSprite = SpriteManager.Instance.GetNonElectricSprite("off_corner");
            }
            if (Name.ToLower().Contains("straight"))
            {
                newSprite = SpriteManager.Instance.GetNonElectricSprite("off_straight");
            }
            if (Name.ToLower().Contains("tpipe"))
            {
                newSprite = SpriteManager.Instance.GetNonElectricSprite("off_tpipe");
            }
            if (Name.ToLower().Contains("cross"))
            {
                newSprite = SpriteManager.Instance.GetNonElectricSprite("off_cross");
            }
            spriteR.sprite = newSprite;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAmmo : MonoBehaviour
{
    //Ammo => munitions
    public int clipSize;
    private Text textClipSize;
    public int extraAmmo;
    private Text textExtraAmmo;
    public int maxExtraAmmo;
    private Text textMaxExtraAmmo;
    public int currentAmmo;
    private Text textCurrentAmmo;
    
    public AudioClip magInSound;
    public AudioClip magOutSound;
    public AudioClip releaseSlideSound;

    void Start()
    {
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Transform panelAmmo = canvas.transform.Find("PanelAmmo");
        textClipSize = panelAmmo.Find("TextClipSize").GetComponent<Text>();
        textExtraAmmo = panelAmmo.Find("TextExtraAmmo").GetComponent<Text>();
        textMaxExtraAmmo = panelAmmo.Find("TextMaxExtraAmmo").GetComponent<Text>();
        textCurrentAmmo = panelAmmo.Find("TextCurrentAmmo").GetComponent<Text>();
        currentAmmo = clipSize;
        maxExtraAmmo = extraAmmo;
    }

    public void RefillAmmo()
    {
        currentAmmo = clipSize;
        extraAmmo = maxExtraAmmo;
    }

    
    void Update()
    {
        textClipSize.text = "" + clipSize;
        textExtraAmmo.text = "" + extraAmmo;
        textMaxExtraAmmo.text = "" + maxExtraAmmo;
        textCurrentAmmo.text = "" + currentAmmo;
    }
    

    public void Reload()
    {
        if (extraAmmo >= clipSize)
        {
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;
            currentAmmo += ammoToReload;
        }
        else if (extraAmmo > 0)
        {
            if (extraAmmo + currentAmmo > clipSize)
            {            //3           18     +      15    -    30
                //if more currrentAmmo than left charge
                int leftOverAmmo = extraAmmo + currentAmmo - clipSize;
                extraAmmo = leftOverAmmo;
                currentAmmo = clipSize;
            }
            else
            {
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }
}

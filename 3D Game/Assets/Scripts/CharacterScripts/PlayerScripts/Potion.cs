using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Potion : MonoBehaviour
{
    public float cooldown;
    public KeyCode key;

    private float cooldownTime;

    private Button button;
    private Image image;

    public TMP_Text cooldownText;

    private void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            TryUsePotion();
        }

        if (cooldownTime > 0)
        {
            cooldownText.text = Mathf.Ceil(cooldownTime).ToString();
            cooldownTime -= Time.deltaTime;
        }
        else
        {
            button.enabled = true;
            image.color = button.colors.normalColor;
            cooldownText.text = "";
        }
    }

    public void TryUsePotion()
    {
        if (cooldownTime <= 0)
        {
            GetComponent<AudioSource>().Play();
            ApplyPotionEffect();
            button.enabled = false;
            image.color = button.colors.disabledColor;
            cooldownTime = cooldown;
        }
    }

    public virtual void ApplyPotionEffect() 
    {
        Debug.Log("Used Potion!");
    }
}

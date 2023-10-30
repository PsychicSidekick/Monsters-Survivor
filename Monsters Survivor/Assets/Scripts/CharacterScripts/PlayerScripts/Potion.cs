using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Potion : MonoBehaviour
{
    public float cooldown;
    public KeyCode key;
    public GameObject potionVFX;

    private float cooldownTime;

    private Button button;
    private Image potionIcon;

    public TMP_Text cooldownText;

    private void Start()
    {
        button = GetComponent<Button>();
        potionIcon = transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKeyDown(key) && Time.timeScale != 0)
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
            potionIcon.color = button.colors.normalColor;
            cooldownText.text = "";
        }
    }

    public void TryUsePotion()
    {
        if (cooldownTime <= 0 && Time.timeScale != 0 && Player.instance.gameObject.activeInHierarchy)
        {
            GetComponent<AudioSource>().Play();
            Instantiate(potionVFX, Player.instance.transform.position, Quaternion.identity);
            ApplyPotionEffect();
            button.enabled = false;
            potionIcon.color = button.colors.disabledColor;
            cooldownTime = cooldown;
        }
    }

    public virtual void ApplyPotionEffect() 
    {
        Debug.Log("Used Potion!");
    }
}

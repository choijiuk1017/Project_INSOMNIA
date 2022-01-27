using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointManager : MonoBehaviour
{
    public GameData gameData;

    public Player player;

    public int startHealthContainer = 1;

    public int curHealth;

    public int healthPerHeart = 4;

    private int maxHealthContainer = 5;

    public int maxHealth;

    public Image[] healthImages;

    public Sprite[] healthSprites;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = startHealthContainer * healthPerHeart;
        maxHealth = maxHealthContainer * healthPerHeart;
        CheckHealthAmount();
    }

    void UpdateHealth()
    {
        bool empty = false;
        int i = 0;

        foreach (Image image in healthImages)
        {
            if(empty)
            {
                image.sprite = healthSprites[0];
            }
            else
            {
                i++;
                if(curHealth >= i* healthPerHeart)
                {
                    image.sprite = healthSprites[healthSprites.Length - 1];
                }
                else
                {
                    int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart * i - curHealth));
                    int healthPerImage = healthPerHeart / (healthSprites.Length - 1);
                    int imageIndex = currentHeartHealth / healthPerImage;
                    image.sprite = healthSprites[imageIndex];
                    empty = true;
                }
            }
        }
    }
    void CheckHealthAmount()
    {
        for (int i = 0; i < maxHealthContainer; i++)
        {
            if (startHealthContainer <= i)
            {
                healthImages[i].enabled = false;
            }
            else
            {
                healthImages[i].enabled = true;
            }
        }

        UpdateHealth();
    }
    public void TakeDamage(int amount)
    {
        curHealth += amount;
        curHealth = Mathf.Clamp(curHealth, 0, startHealthContainer * healthPerHeart);
        if (curHealth == 0)
        {
            player.Die();
        }
        UpdateHealth();
    }

    public void AddHeartContainer()
    {
        startHealthContainer++;
        startHealthContainer = Mathf.Clamp(startHealthContainer, 0, maxHealthContainer);

        curHealth = startHealthContainer * healthPerHeart;
        maxHealth = maxHealthContainer * healthPerHeart;
        CheckHealthAmount();
    }

}

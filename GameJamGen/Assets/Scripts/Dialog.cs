using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI deathCounter;
    public TextMeshProUGUI potionCounter;
    public string[] sentences;
    private string text;
    private string[] potionSentences = new string[4];
    private string[] deathSentences = new string[5];
    [HideInInspector] public int potions = 0;
    private int deaths = 0;

    // Desactiva el cuadro de texto 
    // Llamar siempre con un invoke y los segundos que queremos que dure el cuadro
    public void deactivate()
    {
        dialogBox.SetActive(false);
    }
    private void Start()
    {
        //Potion sentences
        potionSentences[0] = "Why in the world would you want me to collect potions in this deathly cave?!?!?!?";
        potionSentences[1] = "Seriously dude, what's your problem with this potions?";
        potionSentences[2] = "PLEASE! I'm really not having a good time, stop going for these useless potions.";
        potionSentences[3] = "You've got them all but... What did it cost?";


        //DeathCounter sentences
        deathSentences[0] = "Are you even trying? You've already killed me five times";
        deathSentences[1] = "Do I have to teach you how to play after 10 deaths?";
        deathSentences[2] = "20 TIMES DIED! YOU'VE KILLED ME 20 TIMES ALREADY! PLEASE STOP!";
        deathSentences[3] = "25 deaths, I don't know what god is watching but it will punish you for sure...";
        deathSentences[4] = "It's literally imposible that you haven't get tired of killing me after 30 deaths.";

    }
    private void Update()
    {
        deathCounter.text = "Deaths: " + deaths;
        potionCounter.text = "x " + potions;
    }

    // Crear una función por cada dialogo. Poner en el array el número de la frase exacta
    // Llamar a la función desde CollideBox
    public void dialogLava()
    {
        deaths++;
        dialogBox.SetActive(true);
        text = randomSentence(4);
        textDisplay.text = text;
        Invoke("deactivate", 4f);
    }

    public void dialogEnemy()
    {
        deaths++;
        dialogBox.SetActive(true);
        text = randomSentence(8);
        textDisplay.text = text;
        Invoke("deactivate", 4f);
    }

    public void dialogSpikes()
    {
        deaths++;
        dialogBox.SetActive(true);
        text = randomSentence(5);
        textDisplay.text = text;
        Invoke("deactivate", 4f);
    }

    public void dialogPotion()
    {
        dialogBox.SetActive(true);
        textDisplay.text = potionSentences[potions];
        potions++;
        Invoke("deactivate", 4f);
    }

    public void dialogFall()
    {
        deaths++;
        dialogBox.SetActive(true);
        text = randomSentence(7);
        textDisplay.text = text;
        Invoke("deactivate", 4f);
    }

    public void dialogUnlockJump()
    {
        dialogBox.SetActive(true);
        textDisplay.text = sentences[3];
        Invoke("deactivate", 10f);
    }

    public void dialogUnlockDash()
    {
        dialogBox.SetActive(true);
        textDisplay.text = sentences[9];
        Invoke("deactivate", 10f);
    }

    public void dialogUnlockClimb()
    {
        dialogBox.SetActive(true);
        textDisplay.text = sentences[10];
        Invoke("deactivate", 10f);
    }

    public void dialogUnlockWallClimb()
    {
        dialogBox.SetActive(true);
        textDisplay.text = sentences[11];
        Invoke("deactivate", 10f);
    }

    public void dialogUnlockGlide()
    {
        dialogBox.SetActive(true);
        textDisplay.text = sentences[12];
        Invoke("deactivate", 10f);
    }

    private string randomSentence(int n)
    {
        switch (deaths)
        {
            case 5:
                return deathSentences[0];
            case 10:
                return deathSentences[1];
            case 20:
                return deathSentences[2];
            case 25:
                return deathSentences[3];
            case 30:
                return deathSentences[4];
            default:
                int num = Random.Range(0, 4);
                if (num < 3)
                {
                    return sentences[num];
                }
                else
                {
                    num = Random.Range(13, 17);
                    if (num < 16)
                    {
                        return sentences[num];
                    }
                    else { return sentences[n]; }
                }
        }
        
    }
}

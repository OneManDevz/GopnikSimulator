﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
//using Playerino;

public class ItemPickUp: MonoBehaviour
{
    public Item item;
    public Text description;
    public CharacterStats character;

    public void Start()
    {
        item.isBought = false;
    }

    public void Update()
    {
        if (item.isBought)
        {
            GameObject.Find(item.name).GetComponent<Button>().interactable = false;
        }
    }

    public void KupItem()
    {
        GameObject.Find("btnClick").GetComponent<AudioSource>().Play();

        //jedlo nepôjde do inventára - rovno sa konzumuje
        if (item.isFood)
        {
            if ((float)item.cost <= character.brubles)
            {
                character.DecreaseMoney(item.cost);
                GameObject.Find("errors_text").GetComponent<Text>().text = "YOU BOUGHT " + item.name + " FOR " + item.cost;
                character.RestoreHealth(item.restoreHealth);
                character.AddCancer(item.plusCancer);
                character.AddDrunk(item.plusDrunk);
                
                if (character.currentHealth >= character.maxHealth)
                    character.RestoreHealth(0);
            }
        }
        else if ((item.cost <= character.brubles) && (item.isBought == false) && ((Inventory.instance.space-4) > Inventory.instance.items.Count))
        {
            character.DecreaseMoney(item.cost);
            GameObject.Find("errors_text").GetComponent<Text>().text = "YOU BOUGHT " + item.name + " FOR " + item.cost;
            Inventory.instance.Add(item);

            item.isBought = true;
        }
        else if (item.isBought == true)
        {
            GameObject.Find("errors_text").GetComponent<Text>().text = "YOU ALREADY OWN THIS ITEM!";
        }
        else if ((Inventory.instance.space-4) <= Inventory.instance.items.Count)
        {
            GameObject.Find("errors_text").GetComponent<Text>().text = "NOT ENOUGH SPACE!";

        }
        
    }

    public void OnMouseOver()
    {
        description.text = "NAME: " + item.name + "\nSKILL BONUS: " + (item.bonusCrim) + "\nADDS HEALTH: " + item.plusHealth + "\nRESTORE HEALTH: "
        + item.restoreHealth + "\nADDS CANCER: " + item.plusCancer + "\nADDS DRUNK: " + item.plusDrunk;
        GameObject.Find("Description_cost").GetComponent<Text>().text = "COST: " + item.cost;
    }

    public void OnMouseExit()
    {
        description.text = "NAME: " + "\nSKILL BONUS: " + "\nADDS HEALTH: " + "\nRESTORE HEALTH: " + "\nADDS CANCER: " + "\nADDS DRUNK: ";
        GameObject.Find("Description_cost").GetComponent<Text>().text = "COST: ";
    }


}


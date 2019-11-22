using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
using UnityEngine.UI;
using System;
using System.IO;

public class CharacterCreator : MonoBehaviour
{

    public DynamicCharacterAvatar avatar;

    private Dictionary<string, DnaSetter> dna;
    public Slider heightSlider;
    public Slider bellySlider;
    
    //Hair Models
    public List<string> hairModelsMale = new List<string>();
    public List<string> hairModelsFemale = new List<string>();
    private int currentHairMale;
    private int currentHairFemale;

    //Shirt Models
    public List<string> shirtModelsMale = new List<string>();
    public List<string> shirtModelsFemale = new List<string>();
    private int currentShirtMale;
    private int currentShirtFemale;

    //Shirt Models
    public List<string> pantsModelsMale = new List<string>();
    public List<string> pantsModelsFemale = new List<string>();
    private int currentPantsMale;
    private int currentPantsFemale;

    public string myRecipe;

    private void OnEnable()
    {
        avatar.CharacterUpdated.AddListener(Updated);
        heightSlider.onValueChanged.AddListener(HeightChange);
        bellySlider.onValueChanged.AddListener(BellyChange);
    }
    

    private void OnDisable()
    {
        avatar.CharacterUpdated.RemoveListener(Updated);
        heightSlider.onValueChanged.RemoveListener(HeightChange);
        bellySlider.onValueChanged.RemoveListener(BellyChange);
    }

    public void SwitchGender(bool male)
    {
        if (male && avatar.activeRace.name != "HumanMaleDCS")
            avatar.ChangeRace("HumanMaleDCS");
        if (!male && avatar.activeRace.name != "HumanFemaleDCS")
            avatar.ChangeRace("HumanFemaleDCS");
    }

    void Updated(UMAData arg0)
    {
        dna = avatar.GetDNA();
        heightSlider.value = dna["height"].Get();
        bellySlider.value = dna["belly"].Get();
    }

    public void HeightChange(float val)
    {
        dna["height"].Set(val);
        avatar.BuildCharacter();
    }

    public void BellyChange(float val)
    {
        dna["belly"].Set(val);
        avatar.BuildCharacter();
    }

    public void ChangeSkinColor(Color col)
    {
        avatar.SetColor("Skin", col);
        avatar.UpdateColors(true);
    }

    public void ChangeHair(bool plus)
    {
        if(avatar.activeRace.name == "HumanMaleDCS")
        {
            if (plus)
                currentHairMale++;
            else
                currentHairMale--;

            currentHairMale = Mathf.Clamp(currentHairMale, 0, hairModelsMale.Count - 1);

            if (hairModelsMale[currentHairMale] == "None")
                avatar.ClearSlot("Hair");
            else
                avatar.SetSlot("Hair", hairModelsMale[currentHairMale]);

            avatar.BuildCharacter();
        }
        if(avatar.activeRace.name == "HumanFemaleDCS")
        {
            if (plus)
                currentHairFemale++;
            else
                currentHairFemale--;

            currentHairFemale = Mathf.Clamp(currentHairFemale, 0, hairModelsFemale.Count - 1);

            if (hairModelsFemale[currentHairFemale] == "None")
                avatar.ClearSlot("Hair");
            else
                avatar.SetSlot("Hair", hairModelsFemale[currentHairFemale]);

            avatar.BuildCharacter();
        }
    }

    public void ChangeShirt(bool plus)
    {
        if (avatar.activeRace.name == "HumanMaleDCS")
        {
            if (plus)
                currentShirtMale++;
            else
                currentShirtMale--;

            currentShirtMale = Mathf.Clamp(currentShirtMale, 0, shirtModelsMale.Count - 1);

            if (shirtModelsMale[currentShirtMale] == "None")
                avatar.ClearSlot("Chest");
            else
                avatar.SetSlot("Chest", shirtModelsMale[currentShirtMale]);

            avatar.BuildCharacter();
        }
        if (avatar.activeRace.name == "HumanFemaleDCS")
        {
            if (plus)
                currentShirtFemale++;
            else
                currentShirtFemale--;

            currentShirtFemale = Mathf.Clamp(currentShirtFemale, 0, shirtModelsFemale.Count - 1);

            if (shirtModelsFemale[currentShirtFemale] == "None")
                avatar.ClearSlot("Chest");
            else
                avatar.SetSlot("Chest", shirtModelsFemale[currentShirtFemale]);

            avatar.BuildCharacter();
        }
    }

    public void ChangePants(bool plus)
    {
        if (avatar.activeRace.name == "HumanMaleDCS")
        {
            if (plus)
                currentPantsMale++;
            else
                currentPantsMale--;

            currentPantsMale = Mathf.Clamp(currentPantsMale, 0, pantsModelsMale.Count - 1);

            if (pantsModelsMale[currentPantsMale] == "None")
                avatar.ClearSlot("Legs");
            else
                avatar.SetSlot("Legs", pantsModelsMale[currentPantsMale]);

            avatar.BuildCharacter();
        }
        if (avatar.activeRace.name == "HumanFemaleDCS")
        {
            if (plus)
                currentPantsFemale++;
            else
                currentPantsFemale--;

            currentPantsFemale = Mathf.Clamp(currentPantsFemale, 0, pantsModelsFemale.Count - 1);

            if (pantsModelsFemale[currentPantsFemale] == "None")
                avatar.ClearSlot("Legs");
            else
                avatar.SetSlot("Legs", pantsModelsFemale[currentPantsFemale]);

            avatar.BuildCharacter();
        }
    }

    public void SaveRecipe()
    {
        myRecipe = avatar.GetCurrentRecipe();
        File.WriteAllText(Application.persistentDataPath + "/character.txt", myRecipe);
    }

    public void LoadRecipe()
    {
        myRecipe = File.ReadAllText(Application.persistentDataPath + "/character.txt");
        avatar.ClearSlots();
        avatar.LoadFromRecipeString(myRecipe);
    }

}

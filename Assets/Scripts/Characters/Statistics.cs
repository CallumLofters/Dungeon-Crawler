using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{

    // CHARACTER STATS
    private int HealthPoints;
    private int ActionPoints;
    private int Strength;
    private int Endurance;
    private int Charisma;
    private int Agility;
    private int Intelligence;
    private int Perception;

    // COMBAT STATS
    // Overall Skill
    private int TwoHanded;
    private int OneHanded;
    private int Polearms;
    private int Shields;
    private int DualWield;
    private int Fists;
    private int Ranged;
    private int Throwing;

    // Additional Skills
    private int Courage;
    private int Intimidation;
    private int Stealth;

    // Weapon Skill
    private int TH_BattleAxe;
    private int TH_Longsword;
    private int TH_Warhammer;
    private int OH_Sword;
    private int OH_Axe;
    private int OH_Mace;
    private int OH_Hammer;
    private int OH_Rapier;
    private int PA_Spear;
    private int PA_Pike;
    private int SH_LightShield;
    private int SH_HeavyShield;
    private int DW_Knife;
    private int DW_Dagger;
    private int DW_Hatchet;
    private int F_KnuckleDusters;
    private int F_SpikedDusters;
    private int F_BareFisted;
    private int R_LongBow;
    private int R_ShortBow;
    private int R_CrossBow;
    private int R_Slingshot;
    private int THROW_Javelin;
    private int THROW_ThrowingKnife;
    private int THROW_ThrowingAxe;
    private int THROW_Grenades;

    // Equipment Stats

    private int Armour;
    private int Damage;
    private int Range;

    // Fight STATS
    private int Defence;
    private int BlockChance;
    private int Dodge;
    private int HealthRegen;
    private int Parry;
    private int CounterAttack;
    private int CritChance;
    private int CritDamage;
    private int DecapitationChance;


    // CRAFTING STATS
    private int Tailoring;
    private int Blacksmithing;
    private int Whittling;
    private int Herbalism;

    // GATHERING STATS
    private int Woodcutting;
    private int Fishing;
    private int Mining;
    private int Picking; // Gathering berrys
    private int Hunting;


    // MISC STATS
    private int Learning;
    private int Teaching;
    private int Storytelling;
    private int Persuasion;
    private int Reading;
    private int Writing;

    // GENERIC STATS

    private int Age;

    enum Species { Human, Dwarf, Elf, Gnome, Goblin, Beast, ManBeast, Orc, Animal, Bird }
    enum Race { Tiny, Small, Average, Large, Huge } // TO BE COMPLETED
    enum Gender { Male, Female, Non }
    enum Language { Human, Dwarf, Elf, Gnome, Goblin, Beast, Orc, Animal }
}

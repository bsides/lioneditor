namespace LionEditor
{
    public class Character
    {
        byte index;
        string name;
        job job;
        bool isGuest;
        bool isMale;
        Zodiac zodiacSign;
        action secondaryAction;
        ability reactAbility;
        ability supportAbility;
        ability movementAbility;

        Item head;
        Item body;
        Item accessory;
        Item rightHand;
        Item rightShield;
        Item leftHand;
        Item leftShield;

        byte experience;
        byte level;
        byte brave;
        byte faith;

        int rawHP;
        int rawMP;
        int rawSP;
        int rawPA;
        int rawMA;

    }

}
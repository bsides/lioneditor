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
        
        skill secondaryAction;

        Ability reactAbility;
        Ability supportAbility;
        Ability movementAbility;

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



        JobInfo jobInfo;

        /// <summary>
        /// Builds a Character from a 256 byte array
        /// </summary>
        /// <param name="charData"></param>
        public Character( byte[] charData )
        {
        }
    }

}
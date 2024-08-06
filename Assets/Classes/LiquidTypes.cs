using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidTypes
{
    
    public struct Preset
    {
        public string _name;
        public int _viscosity;
        public Color _color;
        public string _bottleshape;

        public Preset(string name, int visc, Color col, string bot)
        {
            _name = name;
            _viscosity = visc;
            _color = col;
            _bottleshape = bot;
        }
    }
    //custom colors, determined by noodling around with Unity color picker lol
    public static Color turqoise = new(0, 0.75f, 1, 1);
    public static Color purple = new(2f / 3f, 0, 1, 1);
    public static Color amber = new(1,0.55f,0,1);
    public static Color lightergrey = new(0.75f, 0.75f, 0.75f, 1f);

    //could make liquids procedurally instead of predefining them?
    public static Preset none = new Preset("", 0, Color.white, "");

    public static Preset water = new Preset("water", 0, lightergrey, "common");
    public static Preset blood = new Preset("blood", 0, Color.red, "Testtube");
    public static Preset pee = new Preset("pee", 0, Color.yellow, "common");
    public static Preset reagent = new Preset("reagent", 0, Color.green, "Testtube");
    public static Preset purp = new Preset("???", 0, purple, "common");
    public static Preset mana = new Preset("mana", 0, turqoise, "common");

    public static Preset gel = new Preset("gel", 10, lightergrey, "common");
    public static Preset treeres = new Preset("tree resin", 10, amber, "common"); 
    public static Preset honey = new Preset("honey", 10, Color.yellow, "common");
    public static Preset plutonium = new Preset("plutonium", 10, Color.green, "common");


    public static Dictionary<int, Preset> PresetDictionary = new Dictionary<int, Preset>() //with a dictionary, we can pick a random int to select a random type of liquid
    {
        {999, none},
        { 0, water},
        { 1, blood},
        { 2, pee},
        { 3, reagent},
        { 4, purp},
        { 5, mana},
        { 6, gel},
        { 7, treeres},
        { 8, honey},
        { 9, plutonium}
    };
}

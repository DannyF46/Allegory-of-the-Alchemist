using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidTypes : MonoBehaviour
{
    //wanted to systemize the ingredient class structure a bit, in this case this lets me assign and get any ingredient.ingredientname
    public struct Solids
    {
        public string _name;
        public Solids(string name)
        {
            _name = name;
        }
    }

    public static Solids candle = new Solids("candle");
    public static Solids eyeball = new Solids("eyeball");
    public static Solids mushroom = new Solids("mushroom");
    public static Solids garlic = new Solids("garlic");
    public static Solids pearl = new Solids("pearl");
    public static Solids pointyleaf = new Solids("sharpfir leaf");
    public static Solids roundleaf = new Solids("roundoak leaf");
    public static Solids salt = new Solids("rock salt");
    public static Solids sage = new Solids("sage");
    public static Solids tail = new Solids("lizard tail");

    //elements listed in same order as InvGen.solids
    public static Dictionary<int, Solids> SolidsDictionary = new Dictionary<int, Solids>()
    {
        { 0, candle },
        { 1, eyeball },
        { 2, mushroom },
        { 3, pearl },
        { 4, salt },
        { 5, sage },
        { 6, garlic },
        { 7, roundleaf },
        { 8, pointyleaf },
        { 9, tail}
    };
}

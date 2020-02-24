namespace PoeTradeDesktop.Schemes.Enums
{
    /// <summary>
    /// This enum is a non-null copy of an array found in the main.js file line ~437 on the official poe trade website. (Keyword: typeToField)
    /// It connects result.item.properties[n].type which is a number from the result api to another string key value that can be used for a sorted search based on an item primary property.
    /// </summary>
    public enum PropertyType
    {
        map_tier = 1,
        map_iiq = 2,
        map_iir = 3,
        map_packsize = 4,
        gem_level = 5,
        quality = 6,
        pdamage = 9,
        edamage = 10,
        cdamage = 11,
        crit = 12, 
        aps = 13,
        block = 15,
        ar = 16, 
        ev = 17, 
        es = 18, 
        gem_level_progress = 20
    }
}

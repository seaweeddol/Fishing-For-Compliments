using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordRandomizer : MonoBehaviour
{
    // TODO: fix UK spellings
    public static Dictionary<int, List<string>> unusedWords = new Dictionary<int, List<string>>
    {
        {1,
        new() {
            "best", "bold", "calm", "cool", "deft", "ease", "fair", "free", "gift", "glad", "glee", "glow", "gold", "good", "heal", "hero", "hope", "keen", "kind", "life", "love", "luck", "mind", "neat", "nice", "open", "play", "pure", "rise", "silk", "soft", "song", "star", "time", "true", "warm", "wise", "rare", "safe", "best", "bold", "calm", "cool", "deft", "ease", "fair", "free", "gift", "glad", "glee", 
            }
        },
        {2,
        new() {
            "adept", "brave", "great", "happy", "ideal", "loved", "loyal", "lucky", "nifty", "noble", "pally", "quick", "savvy", "smart", "witty", "sharp", "adept", "brave", "great", "happy", "ideal", "loved", "loyal", "lucky", "nifty", "noble", "pally", "quick", "savvy", "smart", "witty", "sharp", "adept", "brave", "great", "happy", "ideal", "loved", "loyal", "lucky", "nifty", "noble", "pally", "quick", "savvy", "smart", "witty", 
            }
        },
        {3,
        new() {
            "better", "brainy", "bubbly", "caring", "cheery", "classy", "clever", "daring", "dainty", "dapper", "decent", "honest", "humane", "jovial", "joyful", "joyous", "lively", "lovely", "mature", "mighty", "polite", "pretty", "prompt", "serene", "timely", "unique", "upbeat", "valor", "divine", "dreamy", "driven", "breezy", "bright", "kindly", "leader", "loving", "better", "brainy", "bubbly", "caring", "cheery", "classy", "clever", "daring", "dainty", "dapper", "decent", "honest", "humane", 
            }
        },
        {4,
        new() {
            "affable", "amazing", "amusing", "assured", "beaming", "bravery", "capable", "courage", "focused", "harmony", "joysome", "magical", "mindful", "natural", "patient", "perfect", "precise", "quality", "radiant", "refined", "sincere", "stellar", "tactful", "valiant", "vibrant", "delight", "devoted", "durable", "dynamic", "beloved", "knowing", "likable", "relaxed", "shining", "affable", "amazing", "amusing", "assured", "beaming", "bravery", "capable", "courage", "focused", "harmony", "joysome", "magical", "mindful", "natural", 
            }
        },
        {5,
        new() {
            "abundant", "accurate", "adorable", "affluent", "cheerful", "colorful", "defender", "delicate", "dazzling", "decisive", "debonair", "ecstatic", "energise", "enticing", "euphoric", "exciting", "exultant", "fearless", "friendly", "generous", "glorious", "handsome", "kindness", "likeable", "luminous", "obliging", "optimism", "outgoing", "peaceful", "pleasant", "pleasing", "polished", "positive", "relaxing", "reliable", "sensible", "serenity", "skillful", "sportive", "striking", "talented", "terrific", "truthful", "wellread", "detailed", "diligent", "balanced", "becoming", "laudable", 
            }
        },
        {6,
        new() {
            "admirable", "affirming", "assertive", "authentic", "beautiful", "benignant", "brilliant", "collected", "confident", "connected", "deductive", "dedicated", "dignified", "empowered", "excellent", "favorite", "gratitude", "happiness", "heartfelt", "impartial", "judicious", "memorable", "organised", "practical", "proactive", "promising", "rapturous", "ravishing", "realistic", "refulgent", "resilient", "tenacious", "thrilling", "unselfish", "uplifting", "visionary", "whimsical", "wonderful", "deserving", "desirable", "beauteous", "beguiling", "limitless", "adaptable", "relatable", "reputable", "admirable", "affirming", 
            }
        },
        {7,
        new() {
            "appreciate", "attractive", "bewitching", "brilliance", "celebrated", "charitable", "comforting", "compassion", "confidence", "courageous", "delightful", "deliberate", "dedication", "determined", "empowering", "enchanting", "encouraged", "enthusiasm", "excitement", "friendship", "generosity", "glittering", "gratifying", "harmonious", "honorable", "hospitable", "impressive", "incredible", "innovative", "insightful", "joyfulness", "jubilantly", "methodical", "miraculous", "openminded", "optimistic", "passionate", "perceptive", "persistent", "persuasive", "phenomenal", "productive", "reasonable", "remarkable", "resilience", "satisfying", "thoughtful", "upstanding", "victorious", 
            }
        },
        {8,
        new() {
            "achievement", "adventurous", "brilliantly", "celebrative", "charismatic", "disciplined", "empathizing", "empowerment", "encouraging", "fashionable", "goodlooking", "happinesses", "hardworking", "harmonizing", "heartfeltly", "illuminates", "informative", "inspiration", "instinctive", "intelligent", "kindhearted", "magnificent", "outstanding", "picturesque", "pleasureful", "quickwitted", "resiliently", "responsible", "sensational", "thrillingly", "trustworthy", "upliftingly", "distinctive", "achievement", "adventurous", "brilliantly", "celebrative", "charismatic", "disciplined", "empathizing", "empowerment", "encouraging", "fashionable", "goodlooking", "happinesses", "hardworking", "harmonizing", "heartfeltly", 
            }
        },
        {9,
        new() {
            "accomplished", "affectionate", "appreciative", "breathtaking", "cheerfulness", "enthusiastic", "exhilarating", "goaloriented", "gratifyingly", "harmoniously", "illuminating", "inspirations", "intellectual", "miraculously", "perseverance", "professional", "satisfactory", "satisfyingly", "spellbinding", "unbelievable", "wholehearted", "rejuvenating", "accomplished", "affectionate", "appreciative", "breathtaking", "cheerfulness", "enthusiastic", "exhilarating", "goaloriented", "gratifyingly", "harmoniously", "illuminating", "inspirations", "intellectual", "miraculously", "perseverance", "professional", "satisfactory", "satisfyingly", "spellbinding", "unbelievable", "wholehearted", "rejuvenating", "accomplished", "affectionate", "appreciative", 
            }
        },
        {10,
        new() {
            "compassionate", "encouragement", "encouragingly", "heartfeltness", "inspirational", "kindheartedly", "philanthropic", "resilientness", "splendiferous", "determination", "knowledgeable", "recommendable", "compassionate", "encouragement", "encouragingly", "heartfeltness", "inspirational", "kindheartedly", "philanthropic", "resilientness", "splendiferous", "determination", "knowledgeable", "recommendable", "compassionate", "encouragement", "encouragingly", "heartfeltness", "inspirational", "kindheartedly", "philanthropic", "resilientness", "splendiferous", "determination", "knowledgeable", "recommendable", "compassionate", "encouragement", "encouragingly", "heartfeltness", "inspirational", "kindheartedly", "philanthropic", "resilientness", "splendiferous", "determination", 
            }
        },

    };

    public static Dictionary<int, List<string>> usedWords = new Dictionary<int, List<string>>
    {
        {1, new()},
        {2, new()},
        {3, new()},
        {4, new()},
        {5, new()},
        {6, new()},
        {7, new()},
        {8, new()},
        {9, new()},
        {10, new()},
    };

    public static string GetRandomWord(int difficulty)
    {
        int randomIndex = UnityEngine.Random.Range(0, unusedWords[difficulty].Count);
        // ran into a bug here after catching 79 fish
        // ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection.
        // Parameter name: index
        // System.Collections.Generic.List`1[T].get_Item (System.Int32 index) (at <6d53f1ee8ee746e4b3c819c68e33f296>:0)
        // WordRandomizer.GetRandomWord (System.Int32 difficulty) (at Assets/Scripts/WordRandomizer.cs:80)
        // WordManager.ShowWord (System.Int32 difficulty) (at Assets/Scripts/WordManager.cs:33)
        // PlayerController.Update () (at Assets/Scripts/PlayerController.cs:39)

        string randomWord = unusedWords[difficulty][randomIndex];

        unusedWords[difficulty].RemoveAt(randomIndex);
        usedWords[difficulty].Add(randomWord);

        if (unusedWords.Count == 0)
        {
            unusedWords[difficulty] = usedWords[difficulty];
            usedWords[difficulty] = new List<String>();
        }

        return randomWord;
    }
}

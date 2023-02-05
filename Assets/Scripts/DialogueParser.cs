using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueParser
{
    public static List<Dialogue> GetDialoguesForType(DialogueType type)
    {
        List<Dialogue> dialogues = new List<Dialogue>();
        switch (type)
        {
            case DialogueType.WastelandCan:
                dialogues.Add(new Dialogue("Ich", "Nice, endlich etwas zu trinken! *trinkt*"));
                return dialogues;
            case DialogueType.WastelandDoll:
                dialogues.Add(new Dialogue("Ich",
                    "Oh, die hat wohl einem kleinen Kind gehört. 'Melina' steht hier auf dem Kragen in Kinderschrift. " +
                    "Witzig, meine Schwester hieß genauso..."));
                return dialogues;
            case DialogueType.WastelandPicture:
                dialogues.Add(new Dialogue("Ich",
                    "Eine Familie wie aus dem Bilderbuch - Mutter, Vater und Kind. Ach, was ein Elend. " +
                    "Ich wünschte ich wäre bei meiner Familie..."));
                return dialogues;
            case DialogueType.WastelandSphere:
                dialogues.Add(new Dialogue("Ich", "Oh man, jetzt fange ich an auch noch Hallus zu schieben!" +
                                                  " Verdammt, was ist das?!*greift nach Kugel*"));
                return dialogues;
            case DialogueType.CityIntro:
                dialogues.Add(new Dialogue("Ich",
                    "Was, wo ,wie... *reibt die Augen* Bin ich in einem schlechten Film gelandet? " +
                    "Die Hallus schieben echt zu krass! Ich bin wieder daheim! *den Tränen nahe* Das muss wohl " +
                    "irgendwas mit dieser Kugel zu tun haben...*liest Datum auf einer Anzeige* " +
                    "Es ist der 13.01.2050. Aber das ist der Tag, der... Das darf wohl nicht wahr sein, " +
                    "muss ich jetzt die Krise abwenden oder so?"));
                return dialogues;
            case DialogueType.CityChoices:
            {
                List<ChoiceType> choices = new List<ChoiceType>
                {
                    ChoiceType.People,
                    ChoiceType.Chill,
                    ChoiceType.Investigate
                };
                dialogues.Add(new Dialogue("Ich", "Was soll ich tun?", choices: choices));
                return dialogues;
            }
            case DialogueType.CityPeopleDialogue1:
                dialogues.Add(new Dialogue("NPC", "Doom day, bitte was? Ach du redest nur Blödsinn!"));
                return dialogues;
            case DialogueType.CityPeopleDialogue2:
                dialogues.Add(new Dialogue("NPC", "Hahaha ich lache mich schlapp. " +
                                                  "Komm, geh andere Leute nerven mit deiner Apokalypse!"));
                return dialogues;
            case DialogueType.CityPeopleDialogue3:
                dialogues.Add(new Dialogue("NPC", "Es ist so schon Kurz vor 12. Mach jemanden anderen Angst mit deinen Lügen!"));
                return dialogues;
            case DialogueType.CityPeopleDialogue4:
                dialogues.Add(new Dialogue("NPC", "Klimakrise, Atomkrieg, bla bla bla ... Ich kanns nicht mehr hören!"));
                return dialogues;
            case DialogueType.CityPlayerResult:
                dialogues.Add(new Dialogue("Ich",  "Das war wohl nichts. Kein Wunder, dass keiner mir glaubt. " +
                                                   "Ich sehe aus und rieche wie das Innere eines Abwasserkanals..."));
                return dialogues;
            case DialogueType.RiverIntro:
                dialogues.Add(new Dialogue("Ich", "Ach wie schön. Wie sehr habe ich diesen Ort nur vermisst! *hicks* " +
                                                  "Wenigstens ist bald alles vorbei und dann bin ich bei meinen Liebsten..."));
                return dialogues;
            case DialogueType.ControlRoomIntro:
                dialogues.Add(new Dialogue("Ich", "Ahhh, was ist denn hier los?! Ich denke ich sollte nicht hier sein."));
                return dialogues;
            case DialogueType.ControlRoomChoices: // TODO remove?
            {
                List<ChoiceType> choices = new List<ChoiceType>
                {
                    ChoiceType.People,
                    ChoiceType.LookAround
                };
                dialogues.Add(new Dialogue("Ich", "Was soll ich tun?", choices: choices));
                return dialogues;
            }
            case DialogueType.ControlRoom:
            {
                dialogues.Add(new Dialogue("", "Auf dem Monitor siehst du eine blinkende Warnung: " +
                                               "'Energiezufuhr der Kühlung überprüfen! Kritischer Zustand.'"));
                dialogues.Add(new Dialogue("Ich", "Hmm scheinbar stimmt was nicht im Elektrizitätswerk."));
                List<ChoiceType> choices = new List<ChoiceType>
                {
                    ChoiceType.People,
                    ChoiceType.LookAround
                };
                dialogues.Add(new Dialogue("Ich", "Was soll ich tun?",  "", choices));
                return dialogues; 
            }
            case DialogueType.ReactorIntro:
                dialogues.Add(new Dialogue("Ich", "Oh oh, ich glaube das 'Betreten verboten'-Schild hing nicht umsonst da. *Kugel pulsiert stärker und fliegt auf den Reaktor zu*"));
                return dialogues;
            case DialogueType.Powerhouse:
                dialogues.Add(new Dialogue("NPC", "Ach verdammt nochmal, sieh dir das an. Irgend so ein Typ" +
                                                  " im roten Pulli hat hier alles zu Nichte gemacht und ist weggerannt. " +
                                                  "Erst kleben die sich auf die Autobahn und jetzt das!  Was für ein blödes Pack! " +
                                                  "Verschwinde lieber, hier ist alles drauf und dran hochzugehen!"));
                return dialogues;
            case DialogueType.HouseChoicesOne:
            {
                List<ChoiceType> choices = new List<ChoiceType>
                {
                    ChoiceType.LookAround
                };
                dialogues.Add(new Dialogue("Ich", "Was soll ich tun?", choices: choices));
                return dialogues;
            }
            case DialogueType.FoundAxt:
                dialogues.Add(new Dialogue("Ich", "Das ist wohl die Tatwaffe. Vielleicht sollte ich sie " +
                                                  "zur Sicherheit lieber bei mir halten..."));
                return dialogues;
            case DialogueType.NotFoundAxt:
                dialogues.Add(new Dialogue("BadGuy", "Du schon wieder! Was zur Hölle tust du in meinem Haus?! " +
                                                  "Verschwinde! Sofort! Sonst kannst du was erleben..."));
                return dialogues;
            case DialogueType.ConfrontOne:
                dialogues.Add(new Dialogue("BadGuy", "Du schon wieder! Was zur Hölle tust du in meinem Haus?! " +
                                                     "Du weißt schon viel zu viel! *greift zur Axt und tötet dich*"));
                return dialogues;
            case DialogueType.HouseChoicesTwo:
            {
                List<ChoiceType> choices = new List<ChoiceType>
                {
                    ChoiceType.LookAround
                };
                dialogues.Add(new Dialogue("Ich", "Was soll ich tun?", choices: choices));
                return dialogues;
            }
            case DialogueType.ConfrontTwo:
                dialogues.Add(new Dialogue("Ich", "Sag schon! Warum willst du ... ähh ... wolltest du die Welt zerstören?!"));
                dialogues.Add(new Dialogue("BadGuy", "Ach wenn nicht jetzt, dann in einem Jahr oder zwei oder " +
                                                  "5. spielt das eine Rolle?! Die Welt geht so oder so vor die Hunde. " +
                                                  "Ich wollte uns allen einen gefallen tun ... vor allem IHR ... *läuft in Tränen davon*"));
                return dialogues;
            case DialogueType.Kill:
                dialogues.Add(new Dialogue("Ich", "Geh zurück! Sonst... Du weißt nicht was du angerichtet hast ... " +
                                                  "äh ... anrichten wirst! Du weißt nicht wie es ist,alleine auf dieser " +
                                                  "gottverdammten Welt zu sein! Ahhhhh... *tötet Typ mit Axt*"));
                return dialogues;
        }

        return dialogues;
    }
}

public enum DialogueType
{
    WastelandCan,
    WastelandDoll,
    WastelandPicture,
    WastelandSphere,
    CityIntro,
    CityChoices,
    CityPeopleDialogue1,
    CityPeopleDialogue2,
    CityPeopleDialogue3,
    CityPeopleDialogue4,
    CityPlayerResult,
    RiverIntro,
    ControlRoomIntro,
    ControlRoomChoices,
    ControlRoom,
    ReactorIntro,
    Powerhouse,
    HouseChoicesOne,
    FoundAxt,
    NotFoundAxt,
    ConfrontOne,
    ConfrontTwo,
    HouseChoicesTwo,
    Kill
}

public enum ChoiceType
{
    People,
    PeopleAfterEStation,
    Chill,
    Investigate,
    LookAround
}

public static class ChoiceParser
{
    public static string GetTextForChoiceType(ChoiceType type)
    {
        switch (type)
        {
            case ChoiceType.People:
                return "Leute warnen";
            case ChoiceType.PeopleAfterEStation:
                return "Leute warnen";
            case ChoiceType.Chill:
                return "Tag geniessen";
            case ChoiceType.Investigate:
                return "Ursache suchen";
            case ChoiceType.LookAround:
                return "Umsehen";
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    
    public static string GetSceneForChoiceType(ChoiceType type)
    {
        switch (type)
        {
            case ChoiceType.People:
                return "03_City_1";
            case ChoiceType.PeopleAfterEStation:
                return "03_City_2";
            case ChoiceType.Chill:
                return "04_Fluss";
            case ChoiceType.Investigate:
                return "05_Steuerraum";
            case ChoiceType.LookAround:
                return "05_Reaktor";
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}

public enum Speaker
{
    Player,
    Npc1City,
    Npc2City,
    Npc3City,
    Npc4City,
    Electrician,
    BadGuy,
    None
}

public class Dialogue
{
    public string Speaker;
    public string Text;
    public string Subtext;
    public List<ChoiceType> Choices;

    public Dialogue(string speaker, string text, string subtext = null, List<ChoiceType> choices = null)
    {
        this.Speaker = speaker;
        this.Text = text;
        this.Subtext = subtext;
        this.Choices = choices;
    }
}

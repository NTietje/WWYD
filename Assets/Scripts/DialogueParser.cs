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
                                                  " Verdammt, was ist das?! *greift nach Kugel*"));
                return dialogues;
            case DialogueType.CityIntro:
            {
                dialogues.Add(new Dialogue("Ich",
                    "Was, wo ,wie... *reibt die Augen* Bin ich in einem schlechten Film gelandet? " +
                    "Die Hallus schieben echt zu krass! Ich bin wieder daheim! *den Tränen nahe* Das muss wohl " +
                    "irgendwas mit dieser Kugel zu tun haben..."));
                dialogues.Add(new Dialogue("Ich",
                    "*liest Datum auf einer Anzeige* Es ist der 13.01.2050. Aber das ist der Tag, der... " +
                    "Das darf wohl nicht wahr sein, muss ich jetzt die Krise abwenden oder so?"));
                List<ChoiceType> choices = new List<ChoiceType>
                {
                    ChoiceType.StayWarnPeople,
                    ChoiceType.Chill,
                    ChoiceType.InvestigateAKW
                };
                dialogues.Add(new Dialogue("Ich", "Was soll ich tun?", choices: choices));
                return dialogues;
            }

            case DialogueType.CityChoices2:
            {
                List<ChoiceType> choices = new List<ChoiceType>
                {
                    ChoiceType.StayWarnPeople,
                    ChoiceType.StayTalkBadGuy,
                    ChoiceType.InvestigateAKW
                };
                dialogues.Add(new Dialogue("Ich", "Was soll ich tun?", choices: choices));
                return dialogues;
            }

            case DialogueType.CityChoices:
            {
                dialogues.Add(new Dialogue("Ich",
                    "Das war wohl nichts. Kein Wunder, dass keiner mir glaubt. Ich sehe aus und rieche wie das Innere eines Abwasserkanals..."));
                List<ChoiceType> choices = new List<ChoiceType>
                {
                    ChoiceType.Chill,
                    ChoiceType.InvestigateAKW
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
                                                  "Schön, dass dieser Albtraum gleich vorbei ist..."));
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
                    ChoiceType.BackToCity,
                    ChoiceType.LookAround,
                    ChoiceType.EWerk
                };
                dialogues.Add(new Dialogue("Ich", "Was soll ich tun?",  "", choices));
                return dialogues; 
            }
            case DialogueType.ReactorIntro:
                dialogues.Add(new Dialogue("Ich", "Oh oh, ich glaube das 'Betreten verboten'-Schild hing nicht umsonst da."));
                return dialogues;
            case DialogueType.Powerhouse:
                dialogues.Add(new Dialogue("NPC", "Ach verdammt nochmal, sieh dir das an. Irgend so ein Typ" +
                                                  " im roten Pulli hat hier alles zunichte gemacht und ist weggerannt. " +
                                                  "Erst kleben die sich auf die Autobahn und jetzt das!  Was für ein blödes Pack!"));
                dialogues.Add(new Dialogue("NPC", "Verschwinde lieber, hier ist alles drauf und dran hochzugehen!"));
                return dialogues;
            case DialogueType.City2BadGuy:
                dialogues.Add(new Dialogue("Ich", "Hey du! Du warst am E-Werk und hast ... ähh ... wirst die Leitungen zum alten Reaktor kappen. Warum tust du das?"));
                dialogues.Add(new Dialogue("Guy", "Ähh, nein was redest du da? Was werde ich tun ... oder getan haben werden?! Du bist doch verrückt! *rennt weg*"));
                dialogues.Add(new Dialogue("Ich", "Schon seeehr sus! Wenn er nichts zu verbergen hat, wieso läuft er dann von mir weg? Ich sollte ihm folgen..."));
                
                return dialogues;
            case DialogueType.HouseChoicesOne:
            {
                dialogues.Add(new Dialogue("Guy", "Du schon wieder! Was zur Hölle tust du in meinem Haus?! " +
                                                  "Verschwinde! Sofort! Sonst kannst du was erleben..."));
                List<ChoiceType> choices = new List<ChoiceType>
                {
                    ChoiceType.End2,
                    ChoiceType.ConfrontBadGuy
                };

                dialogues.Add(new Dialogue("Ich", "Was soll ich tun?", choices: choices));
                return dialogues;
            }
            case DialogueType.Zeitung:
                dialogues.Add(new Dialogue("", "Ein Zeitungsartikel mit der Überschrift: " +
                                               "'Verletzte und Tote nach Klimademo-Ausschreitungen'"));
                dialogues.Add(new Dialogue("Ich", "Hier ist ein Frauenname einer Toten eingekreist, ob sie ihm wohl nahe stand? " +
                                                    "Das würde zumindest ein Motiv ergeben... "));

                return dialogues;
            case DialogueType.Stromplan:
                dialogues.Add(new Dialogue("Ich", "Das sind doch die Strompläne zum E-Werk! Also scheint er doch der Verantwortliche zu sein. " +
                                                    "Wie ist er bloß da rangekommen?! Das Ganze kann er doch wohl nicht alleine geplant haben..."));
                
                return dialogues;
            case DialogueType.FoundAxt:
                dialogues.Add(new Dialogue("Ich", "Das ist wohl die Tatwaffe. Vielleicht sollte ich sie " +
                                                  "zur Sicherheit lieber bei mir halten..."));
                return dialogues;
            case DialogueType.NotFoundAxt:
                dialogues.Add(new Dialogue("Guy", "Du schon wieder! Was zur Hölle tust du in meinem Haus?! " +
                                                  "Verschwinde! Sofort! Sonst kannst du was erleben..."));
                /* return dialogues;
            case DialogueType.ConfrontOne:
                dialogues.Add(new Dialogue("BadGuy", "Du schon wieder! Was zur Hölle tust du in meinem Haus?! " +
                                                     "Du weißt schon viel zu viel! *greift zur Axt und tötet dich*")); */
            /*     return dialogues;
            case DialogueType.HouseChoicesTwo:
            {
                List<ChoiceType> choices = new List<ChoiceType>
                {
                    ChoiceType.LookAround
                };
                dialogues.Add(new Dialogue("Ich", "Was soll ich tun?", choices: choices));
                return dialogues;
            } */
                return dialogues;
            case DialogueType.ConfrontTwo:
                dialogues.Add(new Dialogue("Ich", "Sag schon! Warum willst du ... ähh ... wolltest du die Welt zerstören?!"));
                dialogues.Add(new Dialogue("Guy", "Ach wenn nicht jetzt, dann in einem Jahr oder zwei oder " +
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
    City2BadGuy,
    HouseChoicesOne,
    FoundAxt,
    NotFoundAxt,
    //ConfrontOne,
    ConfrontTwo,
    // HouseChoicesTwo,
    Kill,
    Stromplan,
    Zeitung,
    CityChoices2

}

public enum ChoiceType
{
    StayWarnPeople,
    People,
    PeopleAfterEStation,
    Chill,
    InvestigateAKW,
    LookAround,
    BackToCity,
    EWerk,
    End2,
    StayTalkBadGuy,
    ConfrontBadGuy
}

public static class ChoiceParser
{
    public static string GetTextForChoiceType(ChoiceType type)
    {
        switch (type)
        {
            case ChoiceType.StayWarnPeople:
                return "Leute warnen";
            case ChoiceType.People:
                return "Leute warnen";
            case ChoiceType.PeopleAfterEStation:
                return "Leute warnen";
                case ChoiceType.BackToCity:
                return "Zurück zur Stadt";
            case ChoiceType.Chill:
                return "Tag geniessen";
            case ChoiceType.InvestigateAKW:
                return "Zum AKW gehen";
            case ChoiceType.LookAround:
                return "Umsehen";
            case ChoiceType.EWerk:
                return "Zum E-Werk gehen";
            case ChoiceType.End2:
                return "Typ mit Axt töten";
            case ChoiceType.StayTalkBadGuy:
                return "Roter-Pulli-Typ suchen";
            case ChoiceType.ConfrontBadGuy:
                return "Typ zur Rede stellen";
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    
    public static string GetSceneForChoiceType(ChoiceType type)
    {
        switch (type)
        {
            case ChoiceType.StayWarnPeople:
                return "NONE";
            case ChoiceType.StayTalkBadGuy:
                return "NONE";
            case ChoiceType.People:
                return "03_City_1";
                case ChoiceType.BackToCity:
                return "03_City_1";
            case ChoiceType.PeopleAfterEStation:
                return "03_City_3";
            case ChoiceType.Chill:
                return "04_Fluss";
            case ChoiceType.InvestigateAKW:
                return "05_Steuerraum";
            case ChoiceType.LookAround:
                return "05_Reaktor";
            case ChoiceType.EWerk:
                return "06_E-Werk";
            case ChoiceType.End2:
                return "09_Ende_2";
            case ChoiceType.ConfrontBadGuy:
                return "NONE";

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
    Guy,
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

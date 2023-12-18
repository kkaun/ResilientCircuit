using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueIntroTexts
{

    private const float dialogSpeedMultplier = 0.08f;

    public static List<KeyValuePair<string, float>> playerStartMonologue; //Start
    public static List<KeyValuePair<string, float>> atm2ToFixMonologue;
    public static List<KeyValuePair<string, float>> atm1MonologueMonsterDead;
    public static List<KeyValuePair<string, float>> atm2FixedMonologue; //Final

    public static List<KeyValuePair<string, float>> playerMonsterDialogue1;
    public static List<KeyValuePair<string, float>> playerMonsterDialogue2;

    public static List<KeyValuePair<string, float>> playerHumansDialogue1;
    public static List<KeyValuePair<string, float>> playerHumansDialogue2;
    

    public static string[] playerSidePhase1Actions; //To talk with monster or to kill it
    public static string[] playerSidePhase2Actions; //To spare creeps or to kill them


    public static string playerDialogTitle = "Robot:";
    public static string monsterDialogTitle = "Monster:";

    public static string humanMaleDialogTitle = "Male Human:";
    public static string humanFemaleDialogTitle = "Female Human:";

    public static string switchDialogSideToPlayer = "/Robot";
    public static string switchDialogSideToMonster = "/Monster";

    public static string switchDialogSideToHumanMale = "/HumanMale";
    public static string switchDialogSideToHumanFemale = "/HumanFemale";

    public static string endDialogMarker = "-";

    private const float l1 = 1.0f * dialogSpeedMultplier;
    private const float l2 = 2.0f * dialogSpeedMultplier;
    private const float l3 = 3.0f * dialogSpeedMultplier;
    private const float l4 = 4.0f * dialogSpeedMultplier;
    private const float l5 = 5.0f * dialogSpeedMultplier;
    private const float l6 = 6.0f * dialogSpeedMultplier;
    private const float l7 = 7.0f * dialogSpeedMultplier;
    private const float l8 = 8.0f * dialogSpeedMultplier;
    private const float l9 = 9.0f * dialogSpeedMultplier;
    private const float l10 = 10.0f * dialogSpeedMultplier;

    static KeyValuePair<string, float> replic(string text, float timeLength)
    {
        return new KeyValuePair<string, float>(text, timeLength);
    }

    static DialogueIntroTexts()
    {
        playerStartMonologue = new List<KeyValuePair<string, float>>
        {
            replic(switchDialogSideToPlayer, 0.05f),
            replic("Okay... Techinally, my own operational unit woke up.", l5),
            replic("Not Okay: Memory cores are slightly damaged.", l4),
            replic("What year is it? I have a thick dust layer on my physical body.", l5),
            replic("And this case... Something heavy inside. I cannot even let it go off my hands.",l6),
            replic("Seems like I've been modded with a high-priority script to deliver it... But what is the destination?", l8),
            replic("Need to move. My memory cores recover faster while moving.", l6),
            replic(endDialogMarker, 0f),
        };

        playerSidePhase1Actions = new string[] {
            "Try to recover a language pack from the memory core and talk to the Monster",
            "Smash Monster in its chest with my case. It looks weird and suspicious!",
        };

        playerSidePhase2Actions = new string[] {
            "Leave mini-crabs alone by pretending that my sensors are malfunctional",
            "They're useless parasites and should be destroyed! Smash them with my case",
        };

        playerMonsterDialogue1 = new List<KeyValuePair<string, float>>
        {
            replic(switchDialogSideToPlayer, 0.2f),
            replic("Pardon me, dear gentlebeing... What year is it?", l3),
            replic("I don't want to be impolite, but let me identify you as a 270-year old Farlonian Crab. ", l7),
            replic("Last time I had my RAM fully operational, the entrance to this megacity was totally restricted for your kind.", l8),

            replic(switchDialogSideToMonster, 0.2f),
            replic("Are you... able to speak my language normally?", l3),
            replic("... obviously, the answer is yes - unless you're a voice in my head.", l5),
            replic("Sorry. I see that your mind is not doing so well eiter. I was just preparing to defend this ATM for good.", l8),
            replic("Sadly, they seem to be the only source of the internet, since the orbital ray strikes made all observable humans extremely dumb.", l9),
            replic("It was like 49 or 50 years ago... Sincerely, I don't remember exactly.", l6),
            replic("All possible mentions about it were wiped out of the web by spacers.", l6),
            replic("We monsters are lucky that these shiny fellas still have their atomic mini-reactors running.", l7),
            replic("Without internet and proper updates of neural interfaces, all the humans left on this planet are useless...", l8),
            replic("Currently, 2nd generation after the strikes can operate with only a few words: they haven't got any neural interfaces at all...", l9),
            replic("Their dumb parents only taught them how to gather some junkfood and survive... poorly.", l6),
            replic("As far as I'm concerned, they really need something. Unfortunately, I'm far from a fluent dumb human language speaker.", l8),
            replic("I don't harm them, though: I hope that one day they'll make some kind of evolution round, again.", l7),
            replic("To be honest, I thought I'm the olny sentient being left here and felt totally alone for a very long time.", l7),
            replic("The year is 2034, by the way. At least, the news from distant space colonies say that. God bless the internet, truly.", l8),
            replic("And hey... Are we good? You seem to be a nice guy.", l5),

            replic(switchDialogSideToPlayer, 0.2f),
            replic("Sure, mister Crab. Thank you for the information. If you need something in exchange, just let me know.", l8),
            replic("Now, pardon me: I'll take a walk through the district and reflect on your words. My memory cores recover faster while moving.", l9),

            replic(switchDialogSideToMonster, 0.2f),
            replic("No worries, pal! I'm kinda fine - and I'm always here, if you'll need me.", l5),
            replic("Should come back to the ATM for my endless-web-scrolling business: I have a decent addiction after all those years, you know.", l9),
            replic("Just one more thing. There is a bunch of mini-crabs down the street. I care about them from time to time.", l9),
            replic("Please don't touch them, if you don't mind. They're not sentient, just like humans nowadays...", l7),
            replic("But they'll surely ignore you if you won't bother their simple business.", l5),

            replic(endDialogMarker, 0.2f),
        };

        playerHumansDialogue1 = new List<KeyValuePair<string, float>>
        {
            replic(switchDialogSideToPlayer, 0.2f),
            replic("Greetings to a couple of humans.", l3),
            replic("I remember your kind as probably the closest to the most recent AGI robots' generation... in terms of intelligence, at least.", l9),
            replic("But, as far as I presume, since my last interaction with your kind, a couple of things may've been changed.", l8),
            replic("How are you functioning? Do you require any assistance?", l5),

            replic(switchDialogSideToHumanMale, 0.2f),
            replic("Uwuwuwuwuwu!", l3),
            replic(switchDialogSideToHumanFemale, 0.2f),
            replic("Awa! Inturneta!?", l2),
            replic("Telephoona? Tikitoooka?", l3),

            replic(switchDialogSideToPlayer, 0.2f),
            replic("Seems like... a lot of changes for a quick comprehension.", l6),
            replic("Do you need an access to the internet? I'm aware that some of the remaining ATMs can provide it these days.", l8),

            replic(switchDialogSideToHumanMale, 0.2f),
            replic("Aaaa TeeVeee! TeeVee... no worka!", l5),
            replic(switchDialogSideToHumanFemale, 0.2f),
            replic("One-a! Cartoona! Rounda, rounda!", l5),

            replic(switchDialogSideToPlayer, 0.2f),
            replic("So... that probably means that some ATM nearby is malfunctioning or maybe stays in energy-saving mode.", l8),
            replic("It's actually a nice idea to find and fix it.", l6),
            replic("Stay safe, humans. I will try to provide you some internet to reduce your sufferings.", l7),
            replic("I'll be back, most likely. Need to move. My memory cores recover faster while moving.", l7),

            replic(endDialogMarker, 0.2f),
        };

        playerMonsterDialogue2 = new List<KeyValuePair<string, float>>
        {
            replic(switchDialogSideToPlayer, 0.2f),
            replic(switchDialogSideToMonster, 0.2f),
            replic("Hey mate! How are you doing? Found something interesting? By the way, I've just finished 138th season of Santa Plutonia. That one was nice!", l10),

            replic(switchDialogSideToPlayer, 0.2f),
            replic("Hello again, dear (and luckily intelligent) Crab.", l4),
            replic("I've met two people not far from here. They... don't look like anyone from humans I've met in the past.", l8),

            replic(switchDialogSideToMonster, 0.2f),
            replic("Told you, buddy. They're all the same now. At least, they're as peaceful as dumb.", l7),

            replic(switchDialogSideToPlayer, 0.2f),
            replic("Mister Crab, I'd like to help their suffering. They require an internet connection badly, as far as I saw.", l8),
            replic("I can presume that like they have some kind of genetic memory behavior closely relied to the internet.", l8),
            replic("Looks like... their ancestors' bond to the internet may be a curse and a key to their healing as the same time.", l9),

            replic(switchDialogSideToMonster, 0.2f),
            replic("Well... maybe you're right. Couple of months ago I've read a story in Jupiter Crab Daily.", l7),
            replic("It was about a bunch of humans from Earth that 'miraculously evolutionized from ape tribe to cosmonauts', as they personally said,", l9),
            replic("while being arranged by journalists in some rural Mars cosmoport.", l6),
            replic("They told that they've found a source of stable internet and met there some open-minded, philathrophic AGI.", l8),
            replic("Sound like a jackpot: making a really poweful AI fiend in our age. But, you could be better at that as I am, he-he.", l9),
            replic("So... in, like, 20 weeks they were mentally ready to repair an old dusty space fregate and... well, to fly to the nearest human colony planet!", l10),
            replic("Listen, I dont usually believe in such miracles. But... let's say that story shifted my vision of the world a bit.", l8),
            replic("I can try to manage my internet addiction for a while to provide this ATM to those humans for a short time.", l8),

            replic(switchDialogSideToPlayer, 0.2f),
            replic("Mister Crab, I have another idea. Can I borrow an ATM mini-reactor instead?", l6),
            replic("I would make an effort to use its power for the ATM repair, as well as to open the quarantine gates near humans' shelter.", l9),
            replic("Meanwhile, the battery of your ATM should be enough for you to continue your internet-surfing.", l7),

            replic(switchDialogSideToMonster, 0.2f),
            replic("Well, that one sounds even better! To be honest, I wasn't sure if I'm ready to split my ATM with someone else.", l8),
            replic("Here, take the mini-reactor. I'll be fine for a few days without it. Do your best, pal. Good luck!", l7),

            replic(endDialogMarker, 0.2f),
        };


        atm2ToFixMonologue = new List<KeyValuePair<string, float>>
        {
            replic(switchDialogSideToPlayer, 0.2f),
            replic("Okay... This one looks exactly like the ATM that Monster Crab was so passionate about.", l6),
            replic("Not Okay: Its mini-reactor seems to be damaged. Device stays in energy-saving mode and shows one old, short, cycled advertisement.", l9),
            replic("It seems like I see some kind of irrational reason to help those humans. Therefore, I have to find a new power element for this ATM.", l9),
            replic("Not Okay, again: These quarantine gates are locked with some passcode. To be able to move furher, I have to find a way to open them.", l9),
            replic(endDialogMarker, 0f),
        };

        atm1MonologueMonsterDead = new List<KeyValuePair<string, float>>
        {
            replic(switchDialogSideToPlayer, 0.2f),
            replic("Okay... This ATM has an internet connection. That's why Monster Crab was so passionate about it.", l7),
            replic("Its mini-reactor seems to be fully-functional and definitely will become a valuable asset.", l7),
            replic("Besides, it's a perfect energy source for my intergrity recovery module. Need to take it out gently...", l8),
            replic("... Ready. Integrity level is maximized! I should move forward.", l5),
            replic(endDialogMarker, 0f),
        };

        atm2FixedMonologue = new List<KeyValuePair<string, float>>
        {
            replic(switchDialogSideToPlayer, 0.2f),
            replic("[Loud to humans] Dear humans, I've fixed the internet source! You may evolutionize now, hopefully. Welcome to the web and good luck!", l10),

            replic(switchDialogSideToHumanMale, 0.2f),
            replic("[Loud from behind] Uwuwuwuwuwu!", l3),
            replic(switchDialogSideToHumanFemale, 0.2f),
            replic("[Loud from behind] Awa! Inturneta!?", l2),
            replic("[Loud from behind] Needa eeeet, for see!", l3),

            replic(switchDialogSideToPlayer, 0.2f),
            replic("[Silently] Okay, seems like for any mental effort they need to eat first. As far as I remember, it was always like that.", l9),
            replic("[Silently] Now, I need to fix those gates. Their network interface should be accessible through the ATM OS terminal...", l8),
            replic("[Silently] Done. Now, with the gates open, I can continue the movement. My memory cores recover faster while moving.", l8),
            replic("[Silently] The case... I need to find a hint about my destination.", l8),

            replic(endDialogMarker, 0.2f),
        };

    }
}

//Inital level has plot occasions dictated by player choices and, therefore, can be re-played several times
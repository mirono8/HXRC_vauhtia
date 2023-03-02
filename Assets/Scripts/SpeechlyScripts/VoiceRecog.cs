using UnityEngine;
using Speechly.SLUClient;
using TMPro;
using System.Linq;

public class VoiceRecog : MonoBehaviour
{
    private SpeechlyClient client;
    public TMP_Text TranscriptText;
    public Transform target;

    public AIBooleanHandler bools;

    [SerializeField]
    public AI mummo;

    void Start()
    {
        client = MicToSpeechly.Instance.SpeechlyClient;
        client.OnSegmentChange += (segment) =>
        {
            UnityEngine.Debug.Log(segment.ToString());
            TranscriptText.text = segment.ToString(
                (intent) => "intent: " + $"{intent}\n" ,
                (words, entityType) => $"{words}",
                "."
                );

            string intent = segment.intent.intent;



            //kaikki keissit suoritetaan muissa scripteiss�
            if (segment.isFinal && mummo.isListening && !TaskList._taskListInstance.taskList[mummo.tracker.doingNow].t_isCompleted) //ilman segment.isFinal kaikki komennot looppaa
            {
                AnalyzeSegment(segment);

                switch (intent)
                {
                    case "ota":
                        // tänne ota lusikka johon check jos sanottiin laita paksunnos, voi ottaa ilmankin
                        if (bools.IsThisTrue("Suodatinpussi"))
                        {
                            bools.NullBools();
                            InitByIntent.InitOtaLaita(mummo, "suodatinpussi", "Pöytä1");
                            mummo.KahviDo(0, 0); //Ota suodatinpussi kaapista 
                            break;
                        }
                        if (bools.IsThisTrue("Kahvikuppi"))
                        {
                            bools.NullBools();
                            InitByIntent.InitOtaLaita(mummo, "kahvikuppi", "Pöytä2");
                            mummo.KahviDo(0, 11); //Ota kahvikuppi kaapista
                            break;
                        }
                        if (bools.IsThisTrue("Kahvinpurut"))
                        {
                            Debug.Log("haloohaloohaloo");
                            bools.NullBools();

                            InitByIntent.InitOtaLaita(mummo, "kahvinpurut", "Pöytä3");
                            mummo.KahviDo(0, 2); //Ota kahvinpurut kaapista
                            break;
                        }
                        if (bools.IsThisTrue("Vesikannu"))
                        {
                            bools.NullBools();
                            InitByIntent.InitOtaLaita(mummo, "vesikannu", "Pöytä4");
                            mummo.KahviDo(0, 4); //Ota vesikannu kaapista
                            break;
                        }
                        if (bools.IsThisTrue("Lusikka"))
                        {
                            if (bools.IsThisTrue("Paksunnos"))
                            {
                                if (bools.ToolLearned("Paksunnos"))
                                {
                                    bools.NullBools();
                                   /* GameObject iPak;   //// uus setuptools aitoolsissa vvvvv
                                    iPak = mummo.aiTools.SetUpTool("Paksunnos", mummo.dropTargets.Find(target => target.name == "LeftHand").target.transform);
                                    mummo.interactThis = iPak;*/
                                    InitByIntent.InitOtaLaita(mummo, "lusikka", "Pöytä1");
                                    mummo.ToolsDo(0);
                                    break;
                                }
                                else
                                {
                                    //Show me how
                                    UnityEngine.Debug.Log("mummo ei osaa paksunnosta");
                                    bools.NullBools();
                                    break;
                                }
                            }
                            else
                            {
                                Debug.Log("Meni paksunnos not true vaik sanoit paksunnos");
                                bools.NullBools();
                                InitByIntent.InitOtaLaita(mummo, "lusikka", "Pöytä1");
                                mummo.GeneralDo(2,false); //Ota lusikka kaapista 
                                break;
                            }
                        }
                        break;


                    case "laita":
                        if (bools.IsThisTrue("Suodatinpussi"))
                        {
                            if (bools.IsThisTrue("Kahvinpurut") && !bools.IsThisTrue("Kahvinkeitin"))
                            {
                                bools.NullBools();
                                InitByIntent.InitOtaLaita(mummo, "kahvinpurut", "Pöytä3");   //vaiha interactiksi, kahvipurut -> interactwith suodatinpussi !!!!!
                                InitByIntent.InitInteract(mummo, "SuodatinpussiInteract", false);
                                mummo.KahviDo(1, 3); //Laita purut suodatinpussiin
                                break;
                            }
                            else if (bools.IsThisTrue("Kahvinkeitin"))
                            {
                                bools.NullBools();
                                InitByIntent.InitOtaLaita(mummo, "suodatinpussi", "Suppilo");
                                mummo.KahviDo(1, 1); //Laita suodatinpussi kahvinkeittimeen
                                break;
                            }
                        }
                        break;

                    case "avaa":
                        if (bools.IsThisTrue("Vesihana"))
                        {
                            bools.NullBools();

                            mummo.GeneralDo(0, InitByIntent.InitInteract(mummo, "Vesihana", true)); //Avaa vesihana, ei step
                            break;
                        }
                        else if (bools.IsThisTrue("Kansi"))
                        {
                            bools.NullBools();

                            mummo.GeneralDo(0, InitByIntent.InitInteract(mummo, "Kansi", true)); //Avaa kansi, ei step
                            break;
                        }
                        break;

                    case "tayta":
                        if (bools.IsThisTrue("Vesikannu"))
                        {

                            bools.NullBools();
                            // mummo.interactThis = mummo.interactTargets.vesihana;
                            InitByIntent.InitInteract(mummo, "Vesihana", true);
                            InitByIntent.InitOtaLaita(mummo, "vesikannu", "Pöytä4");
                            mummo.KahviDo(1, 5); //T�yt� vesikannu vedell�
                            break;
                        }
                        else if (bools.IsThisTrue("Kahvinkeitin"))
                        {
                            bools.NullBools();
                            InitByIntent.InitInteract(mummo, "Vesisäiliö", false);
                            InitByIntent.InitOtaLaita(mummo, "vesikannu", "Pöytä4");
                            mummo.KahviDo(1, 6); //kaadetaan vesi kahvinkeittimeen
                        }
                        /* else
                         {
                             if (kahviTrue)
                             {
                                 NullBools();
                                 InitByIntent.InitInteract(mummo, "KaadaP�yd�lle_lol", false);
                                 InitByIntent.InitOtaLaita(mummo, "Kahvipannu", "P�yt�");
                                 mummo.GeneralDo(1, false);
                                 break;
                             }
                             else if (vesiTrue)
                             {
                                 UnityEngine.Debug.Log("Vesi huti");
                                 NullBools();
                                 InitByIntent.InitInteract(mummo, "KaadaP�yd�lle_lol", false);
                                 InitByIntent.InitOtaLaita(mummo, "Vesikannu", "P�yt�");
                                 mummo.GeneralDo(1, false);
                                 break;
                             }
                         }*/
                        break;

                    case "kaada":
                        if (bools.IsThisTrue("KahvinKaato"))
                        {
                            Debug.Log("kaadetaan kahvi mukiin");
                            bools.NullBools();
                            InitByIntent.InitInteract(mummo, "KahvikuppiInteract", false);
                            InitByIntent.InitOtaLaita(mummo, "kahvipannu", "PannunPaikka");
                            //  InitByIntent.InitOtaLaita(mummo, "")
                            mummo.KahviDo(1, 12); //T�yt� kahvikuppi kahvilla
                            break;
                        }
                        break;

                    case "huti":
                        if (bools.IsThisTrue("KahvinKaato"))
                        {
                            bools.NullBools();
                            InitByIntent.InitInteract(mummo, "TahraSpot", false);
                            InitByIntent.InitOtaLaita(mummo, "kahvipannu", "PannunPaikka");
                            mummo.GeneralDo(1, false);
                            break;
                        }
                        else if (bools.IsThisTrue("VedenKaato"))
                        {
                            UnityEngine.Debug.Log("Vesi huti");
                            bools.NullBools();
                            InitByIntent.InitInteract(mummo, "TahraSpot", false);
                            InitByIntent.InitOtaLaita(mummo, "vesikannu", "Pöytä4");
                            mummo.GeneralDo(1, false);
                            break;
                        }
                        break;

                    case "sulje":
                        if (bools.IsThisTrue("Kansi"))
                        {
                            bools.NullBools();
                            InitByIntent.InitInteract(mummo, "Kansi", false);
                            mummo.KahviDo(2, 7); //Sulje keittimen kansi
                            break;
                        }

                        if (bools.IsThisTrue("Vesihana"))
                        {
                            bools.NullBools();

                            mummo.GeneralDo(0, InitByIntent.InitInteract(mummo, "Vesihana", false)); //Sulje vesihana, ei step
                            break;
                        }
                        break;

                    case "tarkista":
                        if (bools.IsThisTrue("Virtajohto"))
                        {
                            bools.NullBools();
                            InitByIntent.InitOtaLaita(mummo, "virtajohto", "Pistorasia");
                            InitByIntent.InitInteract(mummo, "VirtajohtoInteract", false);
                            mummo.KahviDo(1, 8); //Laita johto pistorasiaan   
                            break;
                        }
                        break;

                    case "paina":

                        if (bools.IsThisTrue("Virtakytkin"))
                        {
                            bools.NullBools();
                            InitByIntent.InitInteract(mummo, "Virtakytkin", false);
                            mummo.KahviDo(3, 9); //Paina virtakytkint�
                            break;
                        }
                        break;

                    case "sekoita":
                        if (bools.IsThisTrue("Lusikka"))
                        {
                            bools.NullBools();
                            InitByIntent.InitOtaLaita(mummo, "lusikka", "LusikkaMukissa");
                            mummo.KahviDo(1, 14); //Sekoita kahvi lusikalla
                            break;
                        }
                        break;

                    case "lisää":
                        if (bools.IsThisTrue("Maito"))
                        {
                            bools.NullBools();
                            break;
                        }
                        if (bools.IsThisTrue("Sokeri"))
                        {
                            bools.NullBools();
                            break;
                        }
                        break;

                    case "odota":
                        bools.NullBools();
                        mummo.KahviDo(4, 10); //Odoteta kahvin tippumista                    
                        break;

                    case "apuvaline":
                        if (bools.IsThisTrue("Paksunnos"))
                        {
                            if (bools.ToolLearned("Paksunnos"))
                            {
                                bools.NullBools();
                                break;
                            }
                            else
                                Debug.Log("Tool not learned");break;
                        }
                        if (bools.IsThisTrue("Liukuestealusta"))
                        {
                            if (bools.ToolLearned("Liukuestealusta"))
                            {
                                bools.NullBools();
                                break;
                            }
                            else
                                Debug.Log("Tool not learned"); break;
                        }
                        break;

                    default: bools.NullBools(); mummo.InstructionMiss(0); break;
                }
            }
        };
    }

    public void AnalyzeSegment(Segment segment)
    {
        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "suodatinpussi").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            //katsotaan l�ytyyk� tietty entity lauseesta
            bools.SetTrue("Suodatinpussi");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "kahvinkeitin").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("Kahvinkeitin");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "kahvinpuru").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("Kahvinpurut");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "vesikannu").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("Vesikannu");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "vesihana").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("Vesihana");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "kahvikuppi").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("Kahvikuppi");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "kansi").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("Kansi");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "johto").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("Virtajohto");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "virtakytkin").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("Virtakytkin");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "pistorasia").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("Pistorasia");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "lusikka").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("Lusikka");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "kahvi").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("KahvinKaato");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "vesi").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("VedenKaato");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "maito").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("Maito");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "sokeri").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("Sokeri");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "paksunnos").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("Paksunnos");

        if (segment.entities.Select(entry => entry.Value).ToList().Where(entity => entity.type == "liukueste").Select(entity => entity.value).Reverse().ToArray().FirstOrDefault() != null)
            bools.SetTrue("Liukueste");
        //odota ei ole entity vaan intent joten ei ole t��ll� :)
    }

    void Update()
    {

        client = MicToSpeechly.Instance.SpeechlyClient;
        if (Input.GetMouseButton(0))
        {
            if (!client.IsActive && client.IsReady)
            {
                _ = client.Start();
                UnityEngine.Debug.Log("client start");
            }
        }
        else
        {
            if (client.IsActive && client.IsReady)
            {
                _ = client.Stop();
                UnityEngine.Debug.Log("client stop");
            }
        }
    }
}
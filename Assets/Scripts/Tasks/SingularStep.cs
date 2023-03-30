using Photon.Voice;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using static OVRPlugin;

public class SingularStep : MonoBehaviour
{
    private TaskTracker tracker;
    private AI mummo;

    public TaskTargetsToList targetsToList;

    [System.Serializable]
    public class Singular
    {
        [SerializeField]
        public int stepIndex; //make sure to match this with steplist step id

        [SerializeField]
        public Transform grab;

        [SerializeField]
        public Transform drop;

        [SerializeField]
        public GameObject interact;

        
        public Singular(Transform _grab, Transform _drop, GameObject _interact)
        {
            //stepIndex = _myIndex;
            grab = _grab;
            drop = _drop;
            interact = _interact;
        }


    }

    public List<Singular> singulars;

    private void Start()
    {
        tracker = FindObjectOfType<TaskTracker>();
        mummo = FindObjectOfType<AI>();

    }

    
    public void TryDoStepByIndex(int i)
    {

        
    }

    public void PrepTask(string grab, string put, string interact, bool notOnce)
    {
        InitByIntent.InitOtaLaita(mummo, grab, put);

        if (interact != null)
            InitByIntent.InitInteract(mummo, interact, notOnce);

        


    }



    //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ ALL THAT IS USELESS ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^//






    //------------------------------------------- METHODS FOR STEPS WITHOUT VOICE RECOGNITION :) ----------------------------------------------------//




    public void TakeFilter()
    {
        InitByIntent.InitOtaLaita(mummo, "suodatinpussi", "pöytä1");
        mummo.KahviDo(0, 0); //Ota suodatinpussi kaapista 
    }

    public void TakeCoffeeCup()
    {
        InitByIntent.InitOtaLaita(mummo, "kahvikuppi", "pöytä2");
        mummo.KahviDo(0, 11); //Ota kahvikuppi kaapista
    }

    public void TakeGroundCoffee()
    {
        InitByIntent.InitOtaLaita(mummo, "kahvinpurut", "pöytä3");
        mummo.KahviDo(0, 2); //Ota kahvinpurut kaapista
    }

    public void TakeWaterJug()
    {
        InitByIntent.InitOtaLaita(mummo, "vesikannu", "pöytä4");
        mummo.KahviDo(0, 4); //Ota vesikannu kaapista
    }

    public void MeasureCoffee()
    {
        if (mummo.CheckThisFirst(mummo.interactTargets.interactTargets.Find(target => target.name == "kansi").target.GetComponent<Triggerable>()))
        {
            InitByIntent.InitOtaLaita(mummo, "kahvinpurut", "pöytä3");   //vaiha interactiksi, kahvipurut -> interactwith suodatinpussi !!!!!  tää laittaa kahvin näkymään // tää onki ok
            InitByIntent.InitInteract(mummo, "suodatinpussiInteract", false);
            mummo.KahviDo(1, 3); //Laita purut suodatinpussiin
        }
        else
        {
            Debug.Log("Kansi on kiinni");
            mummo.mummoDialog.DontUnderstand();
        }
    }

    public void FilterToCoffeeMaker()
    {
        if (mummo.CheckThisFirst(mummo.interactTargets.interactTargets.Find(target => target.name == "kansi").target.GetComponent<Triggerable>()))
        {
            InitByIntent.InitOtaLaita(mummo, "suodatinpussi", "suppilo");
            InitByIntent.InitInteract(mummo, "suodatinpussiInteract", false);
            mummo.KahviDo(1, 1); //Laita suodatinpussi kahvinkeittimeen
        }
        else
        {
            Debug.Log("Kansi on kiinni");
            mummo.mummoDialog.DontUnderstand();
        }
    }

    public void OpenTap()
    {
        mummo.GeneralDo(0, InitByIntent.InitInteract(mummo, "vesihana", true)); //Avaa vesihana, ei step
    }

    public void OpenLid()
    {
            mummo.GeneralDo(0, InitByIntent.InitInteract(mummo, "kansi", true)); //Avaa kansi, ei step
    }

    public bool CloseLid(out bool result)
    {
        var kansi = mummo.interactTargets.interactTargets.Where(obj => obj.name == "kansi").Select(obj => obj.target).First();

        if (kansi.GetComponent<Triggerable>().isOpen)
        {
            InitByIntent.InitInteract(mummo, "kansi", false);
            mummo.KahviDo(2, 7); //Sulje keittimen kansi
            result = true;
            return result;
        }
        else
        {
            mummo.mummoDialog.DontUnderstand();
            result = false;
            return result;
        }
    }

    public void CloseTap()
    {
        mummo.GeneralDo(0, InitByIntent.InitInteract(mummo, "vesihana", true)); //Sulje vesihana, ei step
    }

    public void FillJug()
    {
        InitByIntent.InitInteract(mummo, "vesihana", true);
        InitByIntent.InitOtaLaita(mummo, "vesikannu", "pöytä4");
        mummo.KahviDo(1, 5); //T�yt� vesikannu vedell�
    }

    public void FillCoffeeMaker()
    {
        if (mummo.CheckThisFirst(mummo.interactTargets.interactTargets.Find(target => target.name == "kansi").target.GetComponent<Triggerable>()))
        {
            InitByIntent.InitInteract(mummo, "vesisäiliö", false);
            InitByIntent.InitOtaLaita(mummo, "vesikannu", "pöytä4");
            mummo.KahviDo(1, 6); //kaadetaan vesi kahvinkeittimeen
        }
        else
        { 
            mummo.mummoDialog.DontUnderstand();
        }
    }

    public void PourCoffee()
    {
        InitByIntent.InitInteract(mummo, "kahvikuppiInteract", false);
        InitByIntent.InitOtaLaita(mummo, "kahvipannu", "pannunPaikka");
        //  InitByIntent.InitOtaLaita(mummo, "")
        mummo.KahviDo(1, 12); 
    }

   public void PlugPlug()
    {
        InitByIntent.InitOtaLaita(mummo, "virtajohto", "pistorasia");
        InitByIntent.InitInteract(mummo, "virtajohtoInteract", false);
        mummo.KahviDo(1, 8); //Laita johto pistorasiaan   
    }

    public void StartCoffeeMachine()
    {
        InitByIntent.InitInteract(mummo, "virtakytkin", false);
        mummo.KahviDo(3, 9); //Paina virtakytkint�
    }

    public void StirCoffee()
    {
        InitByIntent.InitOtaLaita(mummo, "lusikka", "lusikkaMukissa");
        mummo.KahviDo(1, 14); //Sekoita kahvi lusikalla
    }

    public void AddStuff()
    {
        int i = UnityEngine.Random.Range(0, 2);
        if (i == 0)
        {
            InitByIntent.InitOtaLaita(mummo, "maito", "pöytä5");
            mummo.KahviDo(1, 13);
        }
        else
        {
            InitByIntent.InitOtaLaita(mummo, "sokeri", "pöytä6");
            InitByIntent.InitInteract(mummo, "kahvikuppiInteract", false);
            mummo.GeneralDo(3, false);
        }
    }
}

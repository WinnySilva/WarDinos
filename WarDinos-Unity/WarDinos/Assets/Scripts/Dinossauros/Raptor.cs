using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;

public class Raptor : Dinossauro {

    public Raptor (){

        base.custo = 30;
        base.abilityCost = 200;
        base.alcance_ataque = 1;
        base.ataque = 10;
        base.velocidadeAtaque = 1.0;
        base.velocidade_deslocamento = 4;
        base.vida = 50;

        base.custoAttrAtaque = 1;
        base.custoAttrVelocidadeAtaque = 1;
        base.custoAttrVida = 1;
        base.custoAttrVelocidadeDeslocamento = 1;


		base.dinoType = GroupController.DinoType.RAPTOR;
        
        //MAX STATS AND ATTRIBUTE VALUES
        base.MAX_ALCANCE_ATAQUE = 1;
        base.MAX_ATAQUE = 20;
        base.MAX_VELOCIDADE_ATAQUE = 0.75;
        base.MAX_VELOCIDADE_DESLOCAMENTO = 8;
        base.MAX_VIDA = 100;
        base.MAX_ATTR_VIDA = 10;
        base.MAX_ATTR_ATAQUE = 5;
        base.MAX_ATTR_VEL_ATQ = 10;
        base.MAX_ATTR_VEL_DES = 4;

        base.ataque_upg = 1;
        base.velocidadeAtaque_upg = -0.025;
        base.velocidade_deslocamento_upg = 1;
        base.vida_upg = 5;
        //ID's and number of slots occupied by this kind of dino.
        base.playerID = -1;
        base.nSlot = 1;



    }
    private void Awake()
    {
		logg = new LoggerMongo (this.GetType() );
		logg.grupoID = this.GetInstanceID ();
		logg.attachedObj = this;
		logg.acao ="INICIANDO";
		logg.msg ="INICIANDO DINO";
		logg.writeLog ();
    }

    #region implemented abstract members of Dinossauro

    public override void Habilidade()
    {
		int n_raptors = 0; // just need the allies group, so i can implement this shit...
		/**
		 * Needed: Number of raptors on the group.
		 * 1 Raptor = No bonus damage.
		 * 2 Raptors = 50% bonus damage.
		 * 3 Raptors = 75% Bonus damage.
		 * 4 Raptors = 100% Bonus damage.
		*/
		foreach (Dinossauro d in Gc.DinosDinossauro) {
            if(d != null)
            {
                if (d.DinoType == base.DinoType)
                    ++n_raptors;
            }
		} 
		//POWER-UP TIME!!
		if (n_raptors > 1)
			base.ataque = base.ataque * n_raptors / 4;
		logg.grupoID = this.GetInstanceID ();
		logg.acao ="HABILIDADE";
		logg.writeLog ();

		//throw new System.NotImplementedException ();
	}

    public override bool Atacar(GroupController gp)
    {
        int realataque = ataque;
        Gc = gp;
        Dinossauro dTarget = null;
        int menorVida = -1;


        foreach (Dinossauro d in gp.DinosDinossauro)
        {
            if (d != null && (d.Vida < menorVida || menorVida == -1))
            {
                dTarget = d;
                menorVida = d.Vida;
            }
        }
		logg.dinossauroID = GetInstanceID ();
		logg.acao ="ATAQUE";
        if (menorVida != -1)
        {
            if (habilidadeOn)
            {
                Habilidade();
            }
            dTarget.Vida = dTarget.Vida - (ataque - Random.Range(0, ataque / 2) );
            ataque = realataque;
			string msg;
			msg =(GetInstanceID() + "Attacked with " + ataque + " dmg. Target was " + dTarget + "which is now with " + dTarget.Vida + "life");
			Debug.Log (msg);
			logg.msg = msg;
			logg.writeLog ();
			return true;
        }
        else
        {
			string msg;
			msg=(GetInstanceID() + "Attacked but there were no target");
			Debug.Log (msg);
			logg.msg = msg;
            return false;
        }

    }
    #endregion
}

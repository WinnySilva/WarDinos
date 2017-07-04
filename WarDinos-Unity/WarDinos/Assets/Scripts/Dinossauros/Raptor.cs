using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raptor : Dinossauro {

    public Raptor (){

        base.custo = 10;
        base.abilityCost = 200;
        base.alcance_ataque = 1;
        base.ataque = 5;
        base.velocidadeAtaque = 1;
        base.velocidade_deslocamento = 4;
        base.vida = 200;

        base.custoAttrAtaque = 1;
        base.custoAttrVelocidadeAtaque = 1;
        base.custoAttrVida = 1;
        base.custoAttrVelocidadeDeslocamento = 1;

        base.dinoType = Dinossauro.DinoTypes.RAPTOR;
        
        //MAX STATS AND ATTRIBUTE VALUES
        base.MAX_ALCANCE_ATAQUE = 1;
        base.MAX_ATAQUE = 10;
        base.MAX_VELOCIDADE_ATAQUE = 0.5;
        base.MAX_VELOCIDADE_DESLOCAMENTO = 8;
        base.MAX_VIDA = 400;
        base.MAX_ATTR_VIDA = 10;
        base.MAX_ATTR_ATAQUE = 5;
        base.MAX_ATTR_VEL_ATQ = 10;
        base.MAX_ATTR_VEL_DES = 4;

        base.ataque_upg = 1;
        base.velocidadeAtaque_upg = -0.05;
        base.velocidade_deslocamento_upg = 1;
        base.vida_upg = 20;
        //ID's and number of slots occupied by this kind of dino.
        base.playerID = -1;
        base.nSlot = 1;
    }

	void Awake(){
		base.alcance_ataque =1;
		base.ataque=10;
		base.velocidadeAtaque=15;
		base.velocidade_deslocamento=15;
		base.vida=200;
		base.custoAttrAtaque=1;
		base.custoAttrVelocidadeAtaque=1;
		base.custoAttrVida=1;
		base.dinoType= Dinossauro.DinoTypes.RAPTOR;
		base.MAX_ALCANCE_ATAQUE=1;
		base.MAX_ATAQUE=20;
		base.MAX_VELOCIDADE_ATAQUE=30;
		base.MAX_VELOCIDADE_DESLOCAMENTO=30;
		base.MAX_VIDA=400;
		base.playerID=-1;
		base.nSlot =1;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region implemented abstract members of Dinossauro

    public override void Habilidade(GroupController gp)
    {
		int n_raptors = 0; // just need the allies group, so i can implement this shit...
		/**
		 * Needed: Number of raptors on the group.
		 * 1 Raptor = No bonus damage.
		 * 2 Raptors = 50% bonus damage.
		 * 3 Raptors = 75% Bonus damage.
		 * 4 Raptors = 100% Bonus damage.
		*/
		foreach (Dinossauro d in gp.DinosDinossauro) {
			if (d.DinoType == base.DinoType)
				++n_raptors;
		} 
		//POWER-UP TIME!!
		if (n_raptors > 1)
			base.ataque = base.ataque * n_raptors / 4;
		
		//throw new System.NotImplementedException ();
	}

	#endregion
}

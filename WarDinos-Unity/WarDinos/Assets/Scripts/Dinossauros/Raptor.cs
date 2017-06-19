using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raptor : Dinossauro {

	void Awake(){
		base.alcance_ataque =1;
		base.ataque=5;
		base.velocidadeAtaque=1;
		base.velocidade_deslocamento=1;
		base.vida=200;
		base.custoAttrAtaque=1;
		base.custoAttrVelocidadeAtaque=1;
		base.custoAttrVida=1;
		base.dinoType= Dinossauro.DinoTypes.ESTEGOSSAURO;
		base.MAX_ALCANCE_ATAQUE=1;
		base.MAX_ATAQUE=1;
		base.MAX_VELOCIDADE_ATAQUE=1;
		base.MAX_VELOCIDADE_DESLOCAMENTO=1;
		base.MAX_VIDA=200;
		base.playerID=-1;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region implemented abstract members of Dinossauro

	public override void Habilidade ()
	{
		/**
		 * Needed: Number of raptors on the group.
		 * 1 Raptor = No bonus damage.
		 * 2 Raptors = 50% bonus damage.
		 * 3 Raptors = 75% Bonus damage.
		 * 4 Raptors = 100% Bonus damage.
		*/
		int n_raptors = 1; // just need the allies group, so i can implement this shit... 
		if (n_raptors > 1)
			base.ataque = base.ataque * n_raptors / 4;
		
		//throw new System.NotImplementedException ();
	}

	#endregion
}

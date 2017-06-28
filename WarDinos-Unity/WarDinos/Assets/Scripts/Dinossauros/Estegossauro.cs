using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estegossauro : Dinossauro {

	void Awake(){
		base.alcance_ataque =1;
		base.ataque=50;
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
		base.nSlot =2;

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
		 * Area damage, all units of the enemy group suffers the damage.
		 * Lol, just replicate the damage for each enemy unit.
		 * The problem is, i don't have the enemy units vector.
		 * Easy to implement.
		*/


		//throw new System.NotImplementedException ();
	}

	#endregion
}

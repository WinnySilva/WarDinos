using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triceratopo : Dinossauro {

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
		 * Deals 2x his damage if the enemy group is composed of two units
		 * Deals 3x his damage if the enemy group is composed of three units
		 * Deals 4x his damage if the enemy group is composed of four units
		 * I think we should nerf this motherfucker o.O
		 * Took me 2 secons to implement that...
		*/
		int nDinos = 1; //should be the actual number of dinosaurs on the group, but we don't have a function that does that.
		base.ataque = base.ataque * nDinos;
		//throw new System.NotImplementedException ();
	}
	#endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pterodactilo : Dinossauro {
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
		base.nSlot=1;

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
		 * Need to know which lane this dino is at.
		 * Alex... plz gimme methods
		 * Let's change this ability please... it sucks.
		*/

		Random rnd = new Random();
		throw new System.NotImplementedException ();
	}

	#endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pterodactilo : Dinossauro {
	public Pterodactilo (){
        base.custo = 10;
        base.alcance_ataque =5;
		base.ataque=15;
		base.velocidadeAtaque=5;
		base.velocidade_deslocamento=20;
		base.vida=150;
		base.custoAttrAtaque=1;
		base.custoAttrVelocidadeAtaque=1;
		base.custoAttrVida=1;
		base.dinoType= Dinossauro.DinoTypes.PTERODACTILO;
		base.MAX_ALCANCE_ATAQUE=5;
		base.MAX_ATAQUE=30;
		base.MAX_VELOCIDADE_ATAQUE=10;
		base.MAX_VELOCIDADE_DESLOCAMENTO=40;
		base.MAX_VIDA=300;
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

	public override void Habilidade(GroupController allies, GroupController enemies)
	{
		/**
		 * Need to know which lane this dino is at.
		 * Alex... plz gimme methods
		 * Let's change this ability please... it sucks.
		*/

		//Random rnd = new Random();
		throw new System.NotImplementedException ();
	}

	#endregion
}

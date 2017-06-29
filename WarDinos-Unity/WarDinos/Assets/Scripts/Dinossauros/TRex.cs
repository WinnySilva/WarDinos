using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRex : Dinossauro {
	void Awake(){
		base.alcance_ataque =1;
		base.ataque=30;
		base.velocidadeAtaque=5;
		base.velocidade_deslocamento=5;
		base.vida=750;
		base.custoAttrAtaque=1;
		base.custoAttrVelocidadeAtaque=1;
		base.custoAttrVida=1;
		base.dinoType= Dinossauro.DinoTypes.TREX;
		base.MAX_ALCANCE_ATAQUE=1;
		base.MAX_ATAQUE=60;
		base.MAX_VELOCIDADE_ATAQUE=10;
		base.MAX_VELOCIDADE_DESLOCAMENTO=10;
		base.MAX_VIDA=1500;
		base.playerID=-1;
		base.nSlot=4;
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
		 * regenerate life when a unit die.
		 * regenerate 10% of the total health.
		 * again, i need the enemy unit vector...
		*/
		if( true/*enemy dieded :v*/ && base.vida < base.MAX_VIDA){
			if ((int) base.vida + 20 > base.MAX_VIDA)
				base.vida = base.MAX_VIDA;
			else {
				base.vida = base.vida + 20;
			}
		}

		//throw new System.NotImplementedException ();
	}

	#endregion
}

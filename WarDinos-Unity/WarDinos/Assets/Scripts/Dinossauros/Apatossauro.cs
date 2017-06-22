using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apatossauro : Dinossauro {

	void Awake(){
		base.alcance_ataque =1;
		base.ataque=5;
		base.velocidadeAtaque=1;
		base.velocidade_deslocamento=1;
		base.vida=200;
		base.custoAttrAtaque=1;
		base.custoAttrVelocidadeAtaque=1;
		base.custoAttrVida=1;
		base.dinoType= Dinossauro.DinoTypes.APATOSSAURO;
		base.MAX_ALCANCE_ATAQUE=1;
		base.MAX_ATAQUE=1;
		base.MAX_VELOCIDADE_ATAQUE=1;
		base.MAX_VELOCIDADE_DESLOCAMENTO=1;
		base.MAX_VIDA=200;
		base.playerID=-1;
		base.nSlot = 2;
	}





	#region implemented abstract members of Dinossauro
	public override void Habilidade ()
	{
		/**
		 * Again, i need the enemy unit vector so i can decrease their attack speed.
		*/
		for (int i = 0; i < 4 /*size of the group*/; i++);
			/*divide their attack speed by 2*/

		//throw new System.NotImplementedException ();
	}
	#endregion
}

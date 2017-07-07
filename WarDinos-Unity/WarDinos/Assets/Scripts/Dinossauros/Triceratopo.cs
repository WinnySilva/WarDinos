using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triceratopo : Dinossauro {

	public Triceratopo (){

        base.custo = 50;
        base.abilityCost = 100;
        base.alcance_ataque =1;
		base.ataque=10;
		base.velocidadeAtaque=2;
		base.velocidade_deslocamento=3;
		base.vida=100;

		base.custoAttrAtaque=1;
		base.custoAttrVelocidadeAtaque=1;
		base.custoAttrVida=1;
        base.custoAttrVelocidadeDeslocamento = 1;

		base.dinoType= Dinossauro.DinoTypes.TRICERATOPO;

        base.MAX_ALCANCE_ATAQUE=1;
		base.MAX_ATAQUE=30;
		base.MAX_VELOCIDADE_ATAQUE=1.75;
		base.MAX_VELOCIDADE_DESLOCAMENTO=6;
		base.MAX_VIDA=200;

        base.MAX_ATTR_VIDA = 10;
        base.MAX_ATTR_ATAQUE = 10;
        base.MAX_ATTR_VEL_ATQ = 10;
        base.MAX_ATTR_VEL_DES = 3;

        base.vida_upg = 10;
        base.ataque_upg = 2;
        base.velocidadeAtaque_upg = -0.025;
        base.velocidade_deslocamento_upg = 1;

        base.playerID=-1;
		base.nSlot = 3;

        
    }


	#region implemented abstract members of Dinossauro
	public override void Habilidade()
	{
		int nDinos = 0;
		/**
		 * Deals 2x his damage if the enemy group is composed of two units
		 * Deals 3x his damage if the enemy group is composed of three units
		 * Deals 4x his damage if the enemy group is composed of four units
		 * I think we should nerf this motherfucker o.O
		 * Took me 2 seconds to implement that...
		 * COMPLEX AS FUCK!
		*/
		foreach (Dinossauro d in Gc.enemyTargetGroup.DinosDinossauro) {
            if(d != null)
                nDinos++;
		}

		base.ataque = base.ataque * nDinos;
		//throw new System.NotImplementedException ();
	}

    public override bool Atacar(GroupController gp) {
        //dumb mode on
        //after atacking, tricera attack has to go back to it's original value.
        int realataque = ataque;

        //attack implemented by alex-sama
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
        if (menorVida != -1)
        {
            if (habilidadeOn)
            {
                //Atack * number of enemy dinosaurs
                Habilidade();
            }
            dTarget.Vida = dTarget.Vida - ataque;
            Debug.Log(GetInstanceID() + "Attacked with " + ataque + " dmg. Target was " + dTarget + "which is now with " + dTarget.Vida + "life");
            ataque = realataque;
            return true;
        }
        else
        {
            Debug.Log(GetInstanceID() + "Attacked but there were no target");
            return false;
        }
    }
	#endregion
}

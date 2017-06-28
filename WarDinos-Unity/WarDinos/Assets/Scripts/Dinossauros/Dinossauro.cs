using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Dinossauro : MonoBehaviour {

	public enum DinoTypes{APATOSSAURO=0,ESTEGOSSAURO=1,PTERODACTILO=2,RAPTOR=3,TREX=4,TRICERATOPO=5}

    public AudioSource attackSound;

	protected int vida;
	protected int ataque;
	protected double velocidadeAtaque;
	protected int velocidade_deslocamento;
	protected int alcance_ataque;

	protected int custoAttrVida;
	protected int custoAttrAtaque;
	protected int custoAttrVelocidadeAtaque;
	protected int custoAttrVelocidadeDeslocamento;

	protected DinoTypes dinoType;
	protected int playerID;
	protected int lane; // necessary only for ptero ability.

	protected int MAX_VIDA;
	protected int MAX_ATAQUE;
	protected int MAX_VELOCIDADE_ATAQUE;
	protected int MAX_VELOCIDADE_DESLOCAMENTO;
	protected int MAX_ALCANCE_ATAQUE;
	protected int MAX_ATTR_VIDA;
	protected int MAX_ATTR_ATAQUE;
	protected int MAX_ATTR_VEL_ATQ;
	protected int MAX_ATTR_VEL_DES;

	protected int nSlot=1;

	void Start(){
		Velocidade_deslocamento =1;
        
    }
	public int CustoAttrVida{
		get{
			return custoAttrVida;
		}
		set{
			if (value < 1) {
				custoAttrVida = 1;
			} else if (value > MAX_ATTR_VIDA) {
				custoAttrVida = MAX_ATTR_VIDA;
			} else {
				custoAttrVida = value;
			}
		}
	}

	public int CustoAttrAtaque{
		get{
			return custoAttrAtaque;
		}
		set{
			if (value < 1) {
				custoAttrAtaque = 1;
			} else if (value > MAX_ATTR_ATAQUE) {
				custoAttrAtaque = MAX_ATTR_ATAQUE;
			} else {
				custoAttrAtaque = value;
			}
		}
	}
	public int CustoAttrVelocidadeAtaque{
		get{
			return custoAttrVelocidadeAtaque;
		}
		set{
			if (value < 1) {
				custoAttrVelocidadeAtaque = 1;
			} else if (value > MAX_VELOCIDADE_ATAQUE) {
				custoAttrVelocidadeAtaque = MAX_VELOCIDADE_ATAQUE;
			} else {
				custoAttrVelocidadeAtaque = value;
			}
		}
	}
	public int CustoAttrVelocidadeDeslocamento{
		get{
			return custoAttrVelocidadeDeslocamento;
		}
		set{
			if (value < 1) {
				custoAttrVelocidadeDeslocamento = 1;
			} else if (value > MAX_ATTR_VEL_DES) {
				custoAttrVelocidadeDeslocamento = MAX_ATTR_VEL_DES;
			} else {
				custoAttrVelocidadeDeslocamento = value;
			}
		}
	}

	public int Alcance_ataque {
		get {
			return alcance_ataque;
		}
		set{
			
			if(value<1){
				alcance_ataque=1;
			}
			else if(value>MAX_ALCANCE_ATAQUE){
				alcance_ataque = MAX_ALCANCE_ATAQUE;
			}else
			alcance_ataque=value;

		}
	}

	public int PlayerID {
		get {
			return playerID;
		}
		set{
			playerID=value;
		}
	}

	public int Velocidade_deslocamento {
		get {
			return velocidade_deslocamento;
		}
		set{
			
			if(value<0){
				velocidade_deslocamento = 1;
			}
			else if(value>MAX_VELOCIDADE_DESLOCAMENTO){
				velocidade_deslocamento = MAX_VELOCIDADE_DESLOCAMENTO;
			}else
				velocidade_deslocamento=value;
			
		}
	}

	public int VelocidadeAtaque {
		get {
			return velocidadeAtaque;
		}
		set{
			if(value<1){
				velocidadeAtaque= 1;
			}
			else if(value>MAX_VELOCIDADE_ATAQUE ){
				velocidadeAtaque = MAX_VELOCIDADE_ATAQUE;
			}else{
				velocidadeAtaque =value;
			}
		}
	}

	public int Ataque {
		get {
			return ataque;
		}
		set{
			if(value<1){
				ataque= 1;
			}
			else if(value>MAX_ATAQUE ){
				ataque = MAX_ATAQUE;
			}else{
				ataque =value;
			}
		}
	}

	public int Vida {
		get {
			return vida;
		}
		set{
			if(value<=0){
				Die();
			}
			else if(value>MAX_VIDA ){
				vida = MAX_VIDA;
			}else{
				vida =value;
			}
		}
	}

	public int NSlot {
		get {
			return nSlot;
		}
	}
	//Vou precisar do GroupController para fazer a habilidade do Apata e Estego.
	//DinoTypes para a habilidade do Raptor.
	public abstract void Habilidade(DinoTypes types, GroupController enemies);

    // Return true if it successfully attacked OR false when there IS no target.
    public bool Atacar(GroupController gp) {
        // Select target with the shortest life 
		// wtf? Porque menor vida? Tem que ser random... E outra, comenta em pt-br, teu inglês ta fraco. KAPPA
        
		Dinossauro dTarget = null;
        int menorVida = -1;
        foreach (Dinossauro d in gp.DinosDinossauro) {
            if (d != null && (d.Vida < menorVida || menorVida == -1)) {
                dTarget = d;
                menorVida = d.Vida;
            }
        }
        if (menorVida != -1) {
            dTarget.Vida = dTarget.Vida - ataque;
            Debug.Log(GetInstanceID() + "Attacked with " + ataque + " dmg. Target was " + dTarget + "which is now with " + dTarget.Vida + "life");
            return true;
        }
        else {
            Debug.Log(GetInstanceID() + "Attacked but there were no target");
            return false;
        }
    }

    private void Die(GroupController enemies) {
        //gameObject.SetActive(false);
        //transform.position = new Vector2(999.0f, 999.0f);
		/**
		 * Antes do apatassauro desaparecer, os valores das velocidades de ataque dos dinossauros inimigos devem ser restaurados.
		*/
		if(this.dinoType == DinoTypes.APATOSSAURO){
			foreach (Dinossauro d in enemies.DinosDinossauro) {
				d.VelocidadeAtaque = d.VelocidadeAtaque * 2;
			}
		}
        Destroy(gameObject);
    }


	public void CopyAttr(Dinossauro dino){
		this.vida= dino.vida;
		this.ataque= dino.ataque;
		this.velocidadeAtaque= dino.velocidadeAtaque;
		this.velocidade_deslocamento= dino.velocidade_deslocamento;
		this.alcance_ataque= dino.alcance_ataque;

		this.custoAttrVida= dino.custoAttrVida;
		this.custoAttrAtaque= dino.custoAttrAtaque;
		this.custoAttrVelocidadeAtaque= dino.custoAttrVelocidadeAtaque;

		this.dinoType = dino.dinoType;

		this.MAX_VIDA = dino.MAX_VIDA;
		this.MAX_ATAQUE= dino.MAX_ATAQUE;
		this.MAX_VELOCIDADE_ATAQUE= dino.MAX_VELOCIDADE_ATAQUE;
		this.MAX_VELOCIDADE_DESLOCAMENTO= dino.MAX_VELOCIDADE_DESLOCAMENTO;
		this.MAX_ALCANCE_ATAQUE= dino.MAX_ALCANCE_ATAQUE;
	}



}

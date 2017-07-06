using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attributes{ATK, VIDA, VEL_ATK, VEL_DES, HAB}

public class Player : MonoBehaviour {
    static int MAX_RECURSOS = 1000;

    private int vida = 1000;
    private int recursos = 300;

    // Objetos que sao os dinossauros a serem instanciados para os grupos do jogador
    public GameObject goVelociraptor;
    public GameObject goEstegossauro;
    public GameObject goApatossauro;
    public GameObject goPterodactilo;
    public GameObject goTriceratopo;
    public GameObject goTrex;

    private Dinossauro[] goDinos = new Dinossauro[7];

    void Start()
    {
        goDinos[(int)GroupController.DinoType.APATOSSAURO] = goApatossauro.GetComponent<Apatossauro>();
        goDinos[(int)GroupController.DinoType.ESTEGOSSAURO] = goEstegossauro.GetComponent<Estegossauro>();
        goDinos[(int)GroupController.DinoType.PTERODACTILO] = goPterodactilo.GetComponent<Pterodactilo>();
        goDinos[(int)GroupController.DinoType.RAPTOR] = goVelociraptor.GetComponent<Raptor>();
        goDinos[(int)GroupController.DinoType.TREX] = goTrex.GetComponent<TRex>();
        goDinos[(int)GroupController.DinoType.TRICERATOPO] = goTriceratopo.GetComponent<Triceratopo>();
        goDinos[(int)GroupController.DinoType.NONE] = null;
    }

    public int Vida {
        get {
            return vida;
        }
    }
    // Reduz a vida por value e retorna false se jogador chegou a zero vida
    public bool reduzirVida (int value) {
        vida -= value;
        if (vida <= 0)
            return false;
        else
            return true;
    }

    public int Recursos {
        get {
            return recursos;
        }
    }
    // Reduz os recuros e retorna true se houver mais ou igual recursos que value
    // Nao reduz recursos e retorna false caso contrario
    public bool reduzirRecursos (int value) {
        if (value <= recursos) {
            recursos -= value;
            return true;
        }
        else
            return false;
    }
    
    public void incrementarRecursos (int value) {
        if (recursos < MAX_RECURSOS) {
            recursos += value;
            if (recursos > MAX_RECURSOS) {
                recursos = MAX_RECURSOS;
            }
        }
    }

    // Faz o upgrade. Retorna 1 se ocorreu ok;
    // Retorna -1; se não ou
    public int Upgrade (GroupController.DinoType dino, Attributes attr) {
        //TODO: Implementar o que precisa na classe Dinossauro pra poder terminar esse metodo
        // Terminar = Aumentar a quantidade certa
        // = Ter um nivel maximo pra cada atributo


        if (attr == Attributes.ATK) {
            if (reduzirRecursos(goDinos[(int)dino].CustoAttrAtaque)) {
                goDinos[(int)dino].Ataque += goDinos[(int)dino].Ataque_upg;
            }
            else return -1;
        }
        else if (attr == Attributes.VIDA) {
            if (reduzirRecursos(goDinos[(int)dino].CustoAttrVida)) {
                goDinos[(int)dino].Vida += goDinos[(int)dino].Vida_upg;
            }
            else return -1;
        }
        else if (attr == Attributes.VEL_ATK) {
            if (reduzirRecursos(goDinos[(int)dino].CustoAttrVelocidadeAtaque)) {
                goDinos[(int)dino].VelocidadeAtaque += goDinos[(int)dino].VelocidadeAtaque_upg;
            }
            else return -1;
        }
        else if (attr == Attributes.VEL_DES) {
            if (reduzirRecursos(goDinos[(int)dino].CustoAttrVelocidadeDeslocamento)) {
                goDinos[(int)dino].Velocidade_deslocamento += goDinos[(int)dino].Velocidade_deslocamento_upg;
            }
            else return -1;
        }
        
        return 1;
    }
}

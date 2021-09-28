using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Info : MonoBehaviour
{
    [Header("Essencial")]
    public GameObject modelo;
    [HideInInspector]
    public Animator animator;
    
    [Header("Basico")]
    public string nome = "sem nome";

    [Header("Vida")]
    [Range(1, 35)]
    public int vidaMaxima = 1;

    [Range(1, 35)]
    public int vidaAtual = 1;

    [Range(0, 10)]
    public int armadura = 0;

    [Header("Mana")]
    [Range(0, 10)]
    public int manaMaxima = 0;

    [Range(0, 10)]
    public int manaAtual = 0;

    [Header("Velocidade")]
    [Range(1, 10)]
    public float velocidade = 1f;

    [Range(100f, 720f)]
    public float velocidadeDeGiro = 720f;




    void Awake()
    {
        animator = modelo.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

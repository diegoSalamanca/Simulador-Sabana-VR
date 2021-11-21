using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityJSON;

//[JSONObject(ObjectOptions.TupleFormat)]
public class QuizzDataObject 
{
    public int estudiante;
	public string rol;
	public string genero;
	public int estado;
	public int empatia;
	public int tacto;
	public string comunicacion;
	public int contacto_visual;
	public int tiempo_de_prueba;
	
	public Historia historia = new Historia();
}


public class Historia
{
	public string titulo;
	public string rol;
	public string nombre;
	public List<SubHistoria> historia  = new List<SubHistoria>();
}


public class SubHistoria
{
	public string seccion;
	public List<Valores> valores = new List<Valores>();
}

public class Valores
{
	public string nombre;
	public bool valor;
}

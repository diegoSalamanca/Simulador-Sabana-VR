using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using BestHTTP;
using BestHTTP.Authentication;
using UnityJSON;


public class ApiManager : MonoBehaviour
{ 
    string BaseUrl ="https://api.simuladorsabana.com";
    string Secret = "?secret=lkads%C3%B1smfjsdk%C3%B1lj%25Askl3eLsamksd.adksfmds83mAN!2mas";


    static public ApiManager Instance = null;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void TryLogin(string idSiga)
    {
        HTTPRequest request = new HTTPRequest(new Uri(BaseUrl + "/v1/estudiante/"+idSiga+ Secret), HTTPMethods.Get, FindObjectOfType<RolDatosButtonsSelector>().LoginResponse);        
        request.Send();
    }

    public void SaveQuizzData(QuizzDataObject dataObject)
    {
        var jsonData = dataObject.ToJSONString();

        print(jsonData);

        HTTPRequest request = new HTTPRequest(new Uri(BaseUrl + "/v1/examen" + Secret), HTTPMethods.Post, FindObjectOfType<PatientSpeakerController>().EndApplication);
        request.SetHeader("Content-Type", "application/json");
        byte[] encodedPayload = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.RawData = encodedPayload;
        request.Send();       
    }

    public void TestSaveQuizzData()
    {
        var testData = new QuizzDataObject();

        testData.estudiante = 12349;
        testData.rol = "Medico";
        testData.genero = "Mujer";
        testData.estado = 80;
        testData.empatia = 80;
        testData.tacto = 80;
        testData.comunicacion = "buena";
        testData.contacto_visual = 80;
        testData.tiempo_de_prueba = 8000;

        testData.historia.titulo = "Listado de preguntas";
        testData.historia.rol = "Medico";
        testData.historia.nombre = "Señor Pruebas";

        var subhistoria = new SubHistoria();
        testData.historia.historia.Add(subhistoria);       

        subhistoria.seccion = "Saludo/Presentación";

        var saludo = new Valores();
        var presentacion = new Valores();
        var identificacion = new Valores();


        saludo.nombre = "test1";
        saludo.valor = true;

        presentacion.nombre = "test1";
        presentacion.valor = true;

        identificacion.nombre = "test1";
        identificacion.valor = true;

        testData.historia.historia[0].valores.AddRange (new List<Valores>() { saludo, presentacion, identificacion });

        var jsonData = testData.ToJSONString();

        print(jsonData);

        HTTPRequest request = new HTTPRequest(new Uri(BaseUrl + "/v1/examen" + Secret), HTTPMethods.Post, TestResponse);
        request.SetHeader("Content-Type", "application/json");
        byte[] encodedPayload = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.RawData = encodedPayload;
        request.Send();
    }

    public void TestResponse(HTTPRequest request, HTTPResponse response)
    {
        print("test Response Code = " + response.StatusCode);
        print("test Response = "+  response.DataAsText );
    }

}

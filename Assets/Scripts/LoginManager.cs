using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using BestHTTP;
using BestHTTP.Authentication;

public class LoginManager : MonoBehaviour
{ 
    static string BaseUrl ="https://api.simuladorsabana.com";
    static string Secret = "?secret=lkads%C3%B1smfjsdk%C3%B1lj%25Askl3eLsamksd.adksfmds83mAN!2mas";  
    
    static public void TryLogin(string idSiga)
    {
        HTTPRequest request = new HTTPRequest(new Uri(BaseUrl + "/v1/estudiante/"+idSiga+ Secret), HTTPMethods.Get, FindObjectOfType<RolDatosButtonsSelector>().LoginResponse);        
        request.Send();
    }   
}

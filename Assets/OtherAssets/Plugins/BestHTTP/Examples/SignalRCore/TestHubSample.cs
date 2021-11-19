#if !BESTHTTP_DISABLE_SIGNALR_CORE

using System;
using UnityEngine;
using BestHTTP.SignalRCore;
using BestHTTP.SignalRCore.Encoders;

public class TestHubSample : MonoBehaviour
{
    private string baseURL = "https://genesys-multiplayer-lobby-svc-dev.azurewebsites.net";

    private string _path = "/multiplayer-lobby";

    // Instance of the HubConnection
    HubConnection hub;

    protected void Start()
    {
        OnConnectButton();
    }

    void OnDestroy()
    {
        if (hub != null)
            hub.StartClose();
    }

    public void OnConnectButton()
    {
        IProtocol protocol = null;
#if BESTHTTP_SIGNALR_CORE_ENABLE_GAMEDEVWARE_MESSAGEPACK
            protocol = new MessagePackProtocol();
#else
        protocol = new JsonProtocol(new LitJsonEncoder());
#endif
        HubOptions hubOptions = new HubOptions()
        {
            SkipNegotiation = true,
            PreferedTransport = TransportTypes.WebSocket
        };
        Debug.Log(baseURL + this._path);
        // Crete the HubConnection
        hub = new HubConnection(new Uri(baseURL + this._path), protocol, hubOptions);

        // Optionally add an authenticator
        //hub.AuthenticationProvider = new BestHTTP.SignalRCore.Authentication.HeaderAuthenticator("<generated jwt token goes here>");

        hub.On("ReceiveMessage", (string arg, string arg2) => Debug.Log("Mensaje recibido: " + arg + arg2));
        // Subscribe to hub events
        hub.OnConnected += Hub_OnConnected;
        hub.OnError += Hub_OnError;
        hub.OnClosed += Hub_OnClosed;

        hub.OnTransportEvent += (hub, transport, ev) => Debug.Log("Transport:");// AddText(string.Format("Transport(<color=green>{0}</color>) event: <color=green>{1}</color>", transport.TransportType, ev));

         // Set up server callable functions
        // hub.On<Person>("Person", (person) => AddText(string.Format("On '<color=green>Person</color>': '<color=yellow>{0}</color>'", person)).AddLeftPadding(20));
        // hub.On<Person, Person>("TwoPersons", (person1, person2) => AddText(string.Format("On '<color=green>TwoPersons</color>': '<color=yellow>{0}</color>', '<color=yellow>{1}</color>'", person1, person2)).AddLeftPadding(20));

        // And finally start to connect to the server
        hub.StartConnect();

    }

    /// <summary>
    /// GUI button callback
    /// </summary>
    public void OnCloseButton()
    {
        if (this.hub != null)
        {
            this.hub.StartClose();
        }
    }

    public void SendMessageBasic()
    {
        hub.Invoke<string>("SendMessage", "IDCLIENTE", "MENSAJE")
    .OnSuccess(ret => Debug.Log("Mensaje Enviado: "))//AddText(string.Format("'<color=green>NoParam</color>' returned: '<color=yellow>{0}</color>'", ret)).AddLeftPadding(20))
    .OnError(error => Debug.Log("Mensaje Error: "));//AddText(string.Format("'<color=green>NoParam</color>' error: '<color=red>{0}</color>'", error)).AddLeftPadding(20));
    }

    /// <summary>
    /// This callback is called when the plugin is connected to the server successfully. Messages can be sent to the server after this point.
    /// </summary>
    private void Hub_OnConnected(HubConnection hub)
    {

        //  SetButtons(false, true);
        //   AddText(string.Format("Hub Connected with <color=green>{0}</color> transport using the <color=green>{1}</color> encoder.", hub.Transport.TransportType.ToString(), hub.Protocol.Name));
        Debug.Log("Connected by; " + hub.Protocol.Name);
        hub.Send("SendMessage", "my message");

        // Call a parameterless function. We expect a string return value.
        /*   hub.Invoke<string>("SendMessage", "IDCLIENTE", "MENSAJE")
               .OnSuccess(ret => AddText(string.Format("'<color=green>NoParam</color>' returned: '<color=yellow>{0}</color>'", ret)).AddLeftPadding(20))
               .OnError(error => AddText(string.Format("'<color=green>NoParam</color>' error: '<color=red>{0}</color>'", error)).AddLeftPadding(20));*/

        /*
                    // Call a server function with a string param. We expect no return value.
                    hub.Send("Send", "my message");

                    // Call a parameterless function. We expect a string return value.
                    hub.Invoke<string>("NoParam")
                        .OnSuccess(ret => AddText(string.Format("'<color=green>NoParam</color>' returned: '<color=yellow>{0}</color>'", ret)).AddLeftPadding(20))
                        .OnError(error => AddText(string.Format("'<color=green>NoParam</color>' error: '<color=red>{0}</color>'", error)).AddLeftPadding(20));

                    // Call a function on the server to add two numbers. OnSuccess will be called with the result and OnError if there's an error.
                    hub.Invoke<int>("Add", 10, 20)
                        .OnSuccess(result => AddText(string.Format("'<color=green>Add(10, 20)</color>' returned: '<color=yellow>{0}</color>'", result)).AddLeftPadding(20))
                        .OnError(error => AddText(string.Format("'<color=green>Add(10, 20)</color>' error: '<color=red>{0}</color>'", error)).AddLeftPadding(20));

                    hub.Invoke<int?>("NullableTest", 10)
                        .OnSuccess(result => AddText(string.Format("'<color=green>NullableTest(10)</color>' returned: '<color=yellow>{0}</color>'", result)).AddLeftPadding(20))
                        .OnError(error => AddText(string.Format("'<color=green>NullableTest(10)</color>' error: '<color=red>{0}</color>'", error)).AddLeftPadding(20));

                    // Call a function that will return a Person object constructed from the function's parameters.
                    hub.Invoke<Person>("GetPerson", "Mr. Smith", 26)
                        .OnSuccess(result => AddText(string.Format("'<color=green>GetPerson(\"Mr. Smith\", 26)</color>' returned: '<color=yellow>{0}</color>'", result)).AddLeftPadding(20))
                        .OnError(error => AddText(string.Format("'<color=green>GetPerson(\"Mr. Smith\", 26)</color>' error: '<color=red>{0}</color>'", error)).AddLeftPadding(20));

                    // To test errors/exceptions this call always throws an exception on the server side resulting in an OnError call.
                    // OnError expected here!
                    hub.Invoke<int>("SingleResultFailure", 10, 20)
                        .OnSuccess(result => AddText(string.Format("'<color=green>SingleResultFailure(10, 20)</color>' returned: '<color=yellow>{0}</color>'", result)).AddLeftPadding(20))
                        .OnError(error => AddText(string.Format("'<color=green>SingleResultFailure(10, 20)</color>' error: '<color=red>{0}</color>'", error)).AddLeftPadding(20));

                    // This call demonstrates IEnumerable<> functions, result will be the yielded numbers.
                    hub.Invoke<int[]>("Batched", 10)
                        .OnSuccess(result => AddText(string.Format("'<color=green>Batched(10)</color>' returned items: '<color=yellow>{0}</color>'", result.Length)).AddLeftPadding(20))
                        .OnError(error => AddText(string.Format("'<color=green>Batched(10)</color>' error: '<color=red>{0}</color>'", error)).AddLeftPadding(20));

                    // OnItem is called for a streaming request for every items returned by the server. OnSuccess will still be called with all the items.
                    hub.GetDownStreamController<int>("ObservableCounter", 10, 1000)
                        .OnItem(result => AddText(string.Format("'<color=green>ObservableCounter(10, 1000)</color>' OnItem: '<color=yellow>{0}</color>'", result)).AddLeftPadding(20))
                        .OnSuccess(result => AddText("'<color=green>ObservableCounter(10, 1000)</color>' OnSuccess.").AddLeftPadding(20))
                        .OnError(error => AddText(string.Format("'<color=green>ObservableCounter(10, 1000)</color>' error: '<color=red>{0}</color>'", error)).AddLeftPadding(20));

                    // A stream request can be cancelled any time.
                    var controller = hub.GetDownStreamController<int>("ChannelCounter", 10, 1000);

                    controller.OnItem(result => AddText(string.Format("'<color=green>ChannelCounter(10, 1000)</color>' OnItem: '<color=yellow>{0}</color>'", result)).AddLeftPadding(20))
                              .OnSuccess(result => AddText("'<color=green>ChannelCounter(10, 1000)</color>' OnSuccess.").AddLeftPadding(20))
                              .OnError(error => AddText(string.Format("'<color=green>ChannelCounter(10, 1000)</color>' error: '<color=red>{0}</color>'", error)).AddLeftPadding(20));

                    // a stream can be cancelled by calling the controller's Cancel method
                    controller.Cancel();

                    // This call will stream strongly typed objects
                    hub.GetDownStreamController<Person>("GetRandomPersons", 20, 2000)
                        .OnItem(result => AddText(string.Format("'<color=green>GetRandomPersons(20, 1000)</color>' OnItem: '<color=yellow>{0}</color>'", result)).AddLeftPadding(20))
                        .OnSuccess(result => AddText("'<color=green>GetRandomPersons(20, 1000)</color>' OnSuccess.").AddLeftPadding(20));
        */
    }

    /// <summary>
    /// This is called when the hub is closed after a StartClose() call.
    /// </summary>
    private void Hub_OnClosed(HubConnection hub)
    {
        // SetButtons(true, false);
        Debug.Log("Disconnect");
        // AddText("Hub Closed");
    }

    /// <summary>
    /// Called when an unrecoverable error happen. After this event the hub will not send or receive any messages.
    /// </summary>
    private void Hub_OnError(HubConnection hub, string error)
    {
        Debug.Log(hub.Protocol.Connection);
        Debug.LogError(error);
        // SetButtons(true, false);
        //  AddText(string.Format("Hub Error: <color=red>{0}</color>", error));
    }
}


#endif

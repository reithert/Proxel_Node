using System;
using System.IO;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR.Client;


class Proxel_Node {
        public static HubConnection Connection = new HubConnectionBuilder()
        .WithUrl("https://proxel-server.herokuapp.com/nodehub").Build();

        //         public static HubConnection Connection = new HubConnectionBuilder()
        // .WithUrl("https://localhost:7261/nodehub").Build();   

    static void Main(string[] args)
    {


        if(args.Length < 2) {
            Console.WriteLine("Invalid number of arguments.");
            Console.WriteLine("Ex: prxel windows frodo");
            return;
        }

        Proxel_Node.Connect(args[0], args[1]);

        Processer proc = new Processer();


        Proxel_Node.Connection.On<string>("ExecuteRequest", message => {
            proc.executeCommand(message);
            // Response res = new Response(proc.executeCommand(message));
            Proxel_Node.SendOutput("COMMAND RECEIVED");
        });


        TaskCompletionSource<object> taskSource = new TaskCompletionSource<object>();

        Proxel_Node.Connection.Closed += (error) => {
            Console.WriteLine("Connection Closed.");
            taskSource.SetResult(null);
            return Task.CompletedTask;
        };

        Console.WriteLine("Connected To Server.");
        Console.WriteLine("Listening...");

        taskSource.Task.Wait();

    }

    private static async void SendOutput(string? message) {
        await Proxel_Node.Connection.InvokeAsync("SaveOutput", message);
    }

    private static async void Connect(string os, string hostname)
    {
        await Proxel_Node.Connection.StartAsync();
        await Proxel_Node.Connection.InvokeAsync("ConnectToServer", new SystemInfo(os, hostname));
    }
}



using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.SignalR.Client;

public class Processer {

    private Process shell;
    private StreamWriter inputStream;
    private StringBuilder output;
    public Processer() {
        output = new StringBuilder();

        shell = new Process();
        shell.StartInfo.FileName = "powershell.exe";
        shell.StartInfo.UseShellExecute = false;
        shell.StartInfo.RedirectStandardOutput = true;
        shell.StartInfo.RedirectStandardInput = true;
        shell.OutputDataReceived += new DataReceivedEventHandler(async (sender, e) =>
        {
            // Prepend line numbers to each line of the output.
            if (!String.IsNullOrEmpty(e.Data))
            {
                await Proxel_Node.Connection.InvokeAsync("PrintMessage", e.Data);
            }
        });
        
        shell.Start();

        inputStream = shell.StandardInput;

        shell.BeginOutputReadLine();

    }

    ~Processer() {
        shell.Dispose();
        inputStream.Dispose();
    }

    public void executeCommand(string command) {
        // output.Clear();
        inputStream.WriteLine(command);
        // inputStream.Flush();
        // Thread.Sleep(500);
        // shell.CancelOutputRead();
        // return output.ToString();
    }
}
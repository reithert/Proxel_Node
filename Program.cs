using System;
using System.IO;
using System.Diagnostics;


Console.WriteLine("Enter Command: ");
string command = Console.ReadLine();
Console.WriteLine(command);

using (Process process = new Process()) {
    process.StartInfo.FileName = "powershell.exe";
    process.StartInfo.Arguments = command;
    process.StartInfo.UseShellExecute = false;
    process.StartInfo.RedirectStandardOutput = true;
    process.Start();
    
    string output = process.StandardOutput.ReadToEnd();
    File.WriteAllText("output.txt", output);
}

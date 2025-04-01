using System;
using NAudio.Wave;

class Program
{
    static void Main()
    {
        string audioFilePath = @"C:\Users\RC_Student_lab\Documents\CyberSecurityBot\Asset\untitled.wav";

        PlayAudio(audioFilePath);

        Console.WriteLine("Enter your name: ");
        string userName = Console.ReadLine();
        WelcomeUser(userName);

        StartConversation();
    }

    static void PlayAudio(string audioFilePath)
    {
        try
        {
            if (!System.IO.File.Exists(audioFilePath))
            {
                Console.WriteLine("Audio file not found.");
                return;
            }

            using (var audioFile = new AudioFileReader(audioFilePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();

                // Wait for the playback to finish asynchronously
                outputDevice.PlaybackStopped += (sender, args) =>
                {
                    Console.WriteLine("Audio playback finished.");
                };

                // Wait until playback is finished
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(100); // Add a small delay to avoid maxing out CPU usage
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error playing audio: " + ex.Message);
        }
    }

    static void WelcomeUser(string userName)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        string asciiArt = @"
__          __  _                            _       
\ \        / / | |                          | |      
 \ \  /\  / /__| |__   __ _ _ __   __ _ _ __| |_ ___ 
  \ \/  \/ / _ \ '_ \ / _` | '_ \ / _` | '__| __/ _ \
   \  /\  /  __/ | | | (_| | | | | (_| | |  | ||  __/
    \/  \/ \___|_| |_|\__,_|_| |_|\__,_|_|   \__\___|";

        Console.WriteLine(asciiArt);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Hello {userName}, welcome to the Cybersecurity Awareness Bot!");
    }

    static void StartConversation()
    {
        Console.WriteLine("You can ask me anything you want to know about cybersecurity");
        Console.WriteLine("'For example: How to keep your password safe?', 'What is phishing?', etc...");

        string input;
        while ((input = Console.ReadLine()) != "exit")
        {
            HandleUserInput(input);
        }
    }

    static void HandleUserInput(string input)
    {
        if (input.Contains("password"))
        {
            Console.WriteLine("It's important to use strong passwords and change them regularly.");
        }
        else if (input.Contains("phishing"))
        {
            Console.WriteLine("Phishing is when cybercriminals try to trick you into giving away personal information.");
        }
        else
        {
            Console.WriteLine("I didn't quite understand that. Could you rephrase?");
        }
    }
}

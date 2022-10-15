using Newtonsoft.Json;
using Spectre.Console;
using StardewValley.Mod.Extractor.Model;

string pathToUserFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string defaultStorePath = $@"{pathToUserFolder}\Documents\StardewValleyModsExtracted";
string defaultModFolderPath = @"C:\Program Files (x86)\Steam\steamapps\common\Stardew Valley\Mods";
Rule title = new Rule("[red]Stardew Valley Mod Extractor[/]").LeftAligned();
string nexusmodsLink = "https://www.nexusmods.com/stardewvalley/mods/";


AnsiConsole.Write(title);
string pathToMods = AnsiConsole.Ask<string>("Path to Stardew Valley mod folder",defaultModFolderPath);
string pathToStore = AnsiConsole.Ask<string>("Where should the list be saved to?",defaultStorePath);

DirectoryInfo modDir = new DirectoryInfo(pathToMods);
DirectoryInfo storeDir = new DirectoryInfo(pathToStore);

using (StreamWriter streamWriter = File.CreateText(@$"{pathToStore}\modlist-{DateTime.Now.ToString("dd-MM-yyyy--HH-mm-ss")}.txt"))
{
             
foreach (var dir in modDir.GetDirectories())
{
    var files = dir.GetFiles();
    
    if (files.Any(info => info.Name.Contains("manifest")))
    {
        string manifestpath = files.Where(info => info.Name.Contains("manifest")).Select(info => info.FullName).First();
         var manifest =  JsonConvert.DeserializeObject<Manifest>(File.ReadAllText(manifestpath));
       streamWriter.WriteLine($"Name:{manifest.Name}");
       streamWriter.WriteLine($"Description:{manifest.Description}");
       if (manifest.UpdateKeys != null)
       {
           if (manifest.UpdateKeys.Count != 0)
           {
               if (manifest.UpdateKeys.First().Contains("Nexus:"))
               {
                   streamWriter.WriteLine($"Link:{nexusmodsLink}{manifest.UpdateKeys?.First().Remove(0,6)}");
               }
               else
               {
                   streamWriter.WriteLine($"Link:{nexusmodsLink}{manifest.UpdateKeys?.First()}");

               }
           }
       }
       else
       {
           streamWriter.WriteLine($"Link: No link found");
       }
       streamWriter.WriteLine($"=================================================================================================================================");
    }
}

}


Console.ReadKey();
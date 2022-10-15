namespace StardewValley.Mod.Extractor.Model;

   

public class Manifest
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public string MinimumApiVersion { get; set; }
        public string Description { get; set; }
        public string UniqueID { get; set; }
        public string EntryDll { get; set; }
        public List<Dependency> Dependencies { get; set; }
        public List<string> UpdateKeys { get; set; }
    }


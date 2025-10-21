/*
exports a .dat file into a JSON data and model binary data
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MachinePacker
{
    internal class MachinePacker
    {
        //takes a HSDRaw file and exports the machine data
        public static void ExportIntoJSON(ref HSDRaw.HSDRawFile machineWholeData, string exportDir)
        {
            //prints the root node
            Console.WriteLine("Root: " + machineWholeData.Roots[0].Name);
            HSDRaw.AirRide.Vc.KAR_vcDataStar machineData = (HSDRaw.AirRide.Vc.KAR_vcDataStar)machineWholeData.Roots[0].Data;

            //dumps the machine atts
            JObject obj = JObject.Parse(JsonConvert.SerializeObject(machineData.VehicleAttributes, Newtonsoft.Json.Formatting.Indented));
            obj.Remove("_s"); obj.Remove("TrimmedSize"); //remove excess properties we don't need

            //goes through the keys and rename them to a "Prop" + index
            JObject renamed = new JObject();

            int index = 0;
            foreach (var prop in obj.Properties())
            {
                renamed[$"Prop{index}"] = prop.Value;
                index++;
            }

            File.WriteAllText(exportDir + "/runtimeMachineData.macveh", renamed.ToString());

            //dumps the handling atts
            obj = JObject.Parse(JsonConvert.SerializeObject(machineData.HandlingAttributes, Newtonsoft.Json.Formatting.Indented));
            obj.Remove("_s"); obj.Remove("TrimmedSize"); //remove excess properties we don't need

            //goes through the keys and rename them to a "Prop" + index
            renamed = new JObject();

            index = 0;
            foreach (var prop in obj.Properties())
            {
                renamed[$"Prop{index}"] = prop.Value;
                index++;
            }

            File.WriteAllText(exportDir + "/runtimeMachineData.machand", renamed.ToString());

        }

        //takes the JSON stat files and imports the machine data into the HSDRaw file
        public static void ImportIntoHSDRaw(ref HSDRaw.HSDRawFile machineWholeData, string JSONDir)
        {
            //prints the root node
            Console.WriteLine("Root: " + machineWholeData.Roots[0].Name);
            HSDRaw.AirRide.Vc.KAR_vcDataStar machineData = (HSDRaw.AirRide.Vc.KAR_vcDataStar)machineWholeData.Roots[0].Data;

            //imports the machine atts
            HSDRaw.AirRide.Vc.KAR_vcAttributes vehAtt = ((HSDRaw.AirRide.Vc.KAR_vcDataStar)machineWholeData.Roots[0].Data).VehicleAttributes;
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(JSONDir + "/runtimeMachineData.macveh"));

            if (dict == null)
                return;

            foreach (PropertyInfo prop in vehAtt.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanWrite && dict.TryGetValue(prop.Name, out object? value))
                {
                    // Convert the value to the correct property type
                    object? typedValue = Convert.ChangeType(value, prop.PropertyType);
                    prop.SetValue(vehAtt, typedValue);
                }
            }

            // ((HSDRaw.AirRide.Vc.KAR_vcDataStar)machineWholeData.Roots[0].Data).VehicleAttributes = 
            //     JsonConvert.DeserializeObject<HSDRaw.AirRide.Vc.KAR_vcAttributes>(File.ReadAllText(JSONDir + "/runtimeMachineData.macveh"), );

            //imports the handling atts
            HSDRaw.AirRide.Vc.KAR_vcHandlingAttributes handleAtt = ((HSDRaw.AirRide.Vc.KAR_vcDataStar)machineWholeData.Roots[0].Data).HandlingAttributes;
            dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(JSONDir + "/runtimeMachineData.machand"));

            if (dict == null)
                return;

            foreach (PropertyInfo prop in handleAtt.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanWrite && dict.TryGetValue(prop.Name, out object? value))
                {
                    // Convert the value to the correct property type
                    object? typedValue = Convert.ChangeType(value, prop.PropertyType);
                    prop.SetValue(handleAtt, typedValue);
                }
            }

            // ((HSDRaw.AirRide.Vc.KAR_vcDataStar)machineWholeData.Roots[0].Data).HandlingAttributes = 
            //      JsonConvert.DeserializeObject<HSDRaw.AirRide.Vc.KAR_vcHandlingAttributes>(File.ReadAllText(JSONDir + "/runtimeMachineData.machand"));

        }
    }
}


/*

The Machine packer is the interopt tool between the GUI and the HSDRaw data.
This CLI tool can unpack the stats and binary data of a Machine.dat file into raw JSON.
This JSON data can then be read and edited in the GUI.
Once editing is finished it can be repacked into a .dat file.
*/

namespace MachinePacker
{
    class Program
    {
        static void Main(string[] arguments)
        {
            //lists the arguments
            string CLI_ARGUMENT_FLAG_DAT_FILEPATH = "-dat";
            string CLI_ARGUMENT_DESC_DAT_FILEPATH = "The dat filepath, used for both exported from, and exported as.";

            string CLI_ARGUMENT_FLAG_JSON_DIR = "-dir";
            string CLI_ARGUMENT_DESC_JSON_DIR = "The directory for where JSON data is exported and imported.";

            string CLI_ARGUMENT_FLAG_PACK = "-pack";
            string CLI_ARGUMENT_DESC_PACK = "Packs the JSON data into the existing Dat and makes a new Dat file";

            //default functionality is to dump the machine data

            //the data of the params
            string datFP = "";
            string dirFP = "";
            string packFP = ""; //if has stuff we are packing, if not, we are exporting

            //parses the arguments
            for (int i = 0; i < arguments.Length; i++)
            {
                if (arguments[i] == CLI_ARGUMENT_FLAG_DAT_FILEPATH && i + 1 < arguments.Length)
                {
                    i++;
                    datFP = arguments[i];
                }

                else if (arguments[i] == CLI_ARGUMENT_FLAG_JSON_DIR && i + 1 < arguments.Length)
                {
                    i++;
                    dirFP = arguments[i];
                }

                if (arguments[i] == CLI_ARGUMENT_FLAG_PACK && i + 1 < arguments.Length)
                {
                    i++;
                    packFP = arguments[i];
                }
            }

            //if we're dumping the machine data
            if (packFP == "")
            {
                Console.WriteLine($"Exporting \"{datFP}\" into \"{dirFP}\".");

                HSDRaw.HSDRawFile machine = new HSDRaw.HSDRawFile(datFP);
                MachinePacker.ExportIntoJSON(ref machine, dirFP);
            }

            //if we're packing the machine data
            else
            {
                Console.WriteLine($"Importing from \"{dirFP}\" into \"{packFP}\".");

                HSDRaw.HSDRawFile machine = new HSDRaw.HSDRawFile(datFP);
                MachinePacker.ImportIntoHSDRaw(ref machine, dirFP);
                machine.Save(packFP, true, true, true);
            }
        }
    }
}




///*
//exports a .dat file into a JSON data and model binary data
//*/

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml;
//using System.Xml.Linq;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//namespace MachinePacker
//{
//    internal class MachinePacker
//    {
//        //takes a HSDRaw file and exports the machine data
//        public static void ExportIntoJSON(ref HSDRaw.HSDRawFile machineWholeData, string exportDir)
//        {
//            //prints the root node
//            Console.WriteLine("Root: " + machineWholeData.Roots[0].Name);
//            HSDRaw.AirRide.Vc.KAR_vcDataStar machineData = (HSDRaw.AirRide.Vc.KAR_vcDataStar)machineWholeData.Roots[0].Data;

//            //dumps the machine atts
//            JObject obj = JObject.Parse(JsonConvert.SerializeObject(machineData.VehicleAttributes, Newtonsoft.Json.Formatting.Indented));
//            obj.Remove("_s"); obj.Remove("TrimmedSize"); //remove excess properties we don't need

//            //goes through the keys and rename them to a "Prop" + index
//            JObject renamed = new JObject();

//            int index = 0;
//            foreach (var prop in obj.Properties())
//            {
//                renamed[$"Prop{index}"] = prop.Value;
//                index++;
//            }

//            File.WriteAllText(exportDir + "/runtimeMachineData.macveh", renamed.ToString());

//            //dumps the handling atts
//            obj = JObject.Parse(JsonConvert.SerializeObject(machineData.HandlingAttributes, Newtonsoft.Json.Formatting.Indented));
//            obj.Remove("_s"); obj.Remove("TrimmedSize"); //remove excess properties we don't need

//            //goes through the keys and rename them to a "Prop" + index
//            renamed = new JObject();

//            index = 0;
//            foreach (var prop in obj.Properties())
//            {
//                renamed[$"Prop{index}"] = prop.Value;
//                index++;
//            }

//            File.WriteAllText(exportDir + "/runtimeMachineData.machand", renamed.ToString());

//        }

//        //takes the JSON stat files and imports the machine data into the HSDRaw file
//        public static void ImportIntoHSDRaw(ref HSDRaw.HSDRawFile machineWholeData, string JSONDir)
//        {
//            //prints the root node
//            Console.WriteLine("Root: " + machineWholeData.Roots[0].Name);
//            HSDRaw.AirRide.Vc.KAR_vcDataStar machineData = (HSDRaw.AirRide.Vc.KAR_vcDataStar)machineWholeData.Roots[0].Data;

//            //imports the machine atts
//            HSDRaw.AirRide.Vc.KAR_vcAttributes vehAtt = ((HSDRaw.AirRide.Vc.KAR_vcDataStar)machineWholeData.Roots[0].Data).VehicleAttributes;
//            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(JSONDir + "/runtimeMachineData.macveh"));

//            if (dict == null)
//                return;

//            foreach (PropertyInfo prop in vehAtt.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
//            {
//                if (prop.CanWrite && dict.TryGetValue(prop.Name, out object? value))
//                {
//                    // Convert the value to the correct property type
//                    object? typedValue = Convert.ChangeType(value, prop.PropertyType);
//                    prop.SetValue(vehAtt, typedValue);
//                }
//            }

//            //imports the handling atts
//            HSDRaw.AirRide.Vc.KAR_vcHandlingAttributes handleAtt = ((HSDRaw.AirRide.Vc.KAR_vcDataStar)machineWholeData.Roots[0].Data).HandlingAttributes;
//            dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(JSONDir + "/runtimeMachineData.machand"));

//            if (dict == null)
//                return;

//            foreach (PropertyInfo prop in handleAtt.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
//            {
//                if (prop.CanWrite && dict.TryGetValue(prop.Name, out object? value))
//                {
//                    // Convert the value to the correct property type
//                    object? typedValue = Convert.ChangeType(value, prop.PropertyType);
//                    prop.SetValue(handleAtt, typedValue);
//                }
//            }
//        }
//    }
//}

/*

The Machine packer is the interopt tool between the GUI and the HSDRaw data.
This CLI tool can unpack the stats and binary data of a Machine.dat file into raw JSON.
This JSON data can then be read and edited in the GUI.
Once editing is finished it can be repacked into a .dat file.
*/

//namespace MachinePacker
//{
//    class Program
//    {
//        static void Main(string[] arguments)
//        {
//            //lists the arguments
//            string CLI_ARGUMENT_FLAG_DAT_FILEPATH = "-dat";
//            string CLI_ARGUMENT_DESC_DAT_FILEPATH = "The dat filepath, used for both exported from, and exported as.";

//            string CLI_ARGUMENT_FLAG_JSON_DIR = "-dir";
//            string CLI_ARGUMENT_DESC_JSON_DIR = "The directory for where JSON data is exported and imported.";

//            string CLI_ARGUMENT_FLAG_PACK = "-pack";
//            string CLI_ARGUMENT_DESC_PACK = "Packs the JSON data into the existing Dat and makes a new Dat file";

//            //default functionality is to dump the machine data

//            //the data of the params
//            string datFP = "";
//            string dirFP = "";
//            string packFP = ""; //if has stuff we are packing, if not, we are exporting

//            //parses the arguments
//            for (int i = 0; i < arguments.Length; i++)
//            {
//                if (arguments[i] == CLI_ARGUMENT_FLAG_DAT_FILEPATH && i + 1 < arguments.Length)
//                {
//                    i++;
//                    datFP = arguments[i];
//                }

//                else if (arguments[i] == CLI_ARGUMENT_FLAG_JSON_DIR && i + 1 < arguments.Length)
//                {
//                    i++;
//                    dirFP = arguments[i];
//                }

//                if (arguments[i] == CLI_ARGUMENT_FLAG_PACK && i + 1 < arguments.Length)
//                {
//                    i++;
//                    packFP = arguments[i];
//                }
//            }

//            //if we're dumping the machine data
//            if (packFP == "")
//            {
//                Console.WriteLine($"Exporting \"{datFP}\" into \"{dirFP}\".");

//                HSDRaw.HSDRawFile machine = new HSDRaw.HSDRawFile(datFP);
//                MachinePacker.ExportIntoJSON(ref machine, dirFP);
//            }

//            //if we're packing the machine data
//            else
//            {
//                Console.WriteLine($"Importing from \"{dirFP}\" into \"{packFP}\".");

//                HSDRaw.HSDRawFile machine = new HSDRaw.HSDRawFile(datFP);
//                MachinePacker.ImportIntoHSDRaw(ref machine, dirFP);
//                machine.Save(packFP, true, true, true);
//            }
//        }
//    }
//}

using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using DearImPlot;
using System.Runtime.CompilerServices;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

using static ImGuiNET.ImGuiNative;

namespace ImGuiNET
{
    class Program
    {
        private static Sdl2Window _window;
        private static GraphicsDevice _gd;
        private static CommandList _cl;
        private static ImGuiController _controller;
        // private static MemoryEditor _memoryEditor;

        // UI state
        private static float _f = 0.0f;
        private static int _counter = 0;
        private static int _dragInt = 0;
        private static Vector3 _clearColor = new Vector3(0.45f, 0.55f, 0.6f);
        private static bool _showImGuiDemoWindow = true;
        private static bool _showAnotherWindow = false;
        private static bool _showMemoryEditor = false;
        private static byte[] _memoryEditorData;
        private static uint s_tab_bar_flags = (uint)ImGuiTabBarFlags.Reorderable;
        static bool[] s_opened = { true, true, true, true }; // Persistent user state

        static void SetThing(out float i, float val) { i = val; }

        static void Main(string[] args)
        {
            // Create window, GraphicsDevice, and all resources necessary for the demo.
            VeldridStartup.CreateWindowAndGraphicsDevice(
                new WindowCreateInfo(50, 50, 1280, 720, WindowState.Normal, "ImGui.NET Sample Program"),
                new GraphicsDeviceOptions(true, null, true, ResourceBindingModel.Improved, true, true),
                out _window,
                out _gd);
            _window.Resized += () =>
            {
                _gd.MainSwapchain.Resize((uint)_window.Width, (uint)_window.Height);
                _controller.WindowResized(_window.Width, _window.Height);
            };
            _cl = _gd.ResourceFactory.CreateCommandList();
            _controller = new ImGuiController(_gd, _gd.MainSwapchain.Framebuffer.OutputDescription, _window.Width, _window.Height);
            // _memoryEditor = new MemoryEditor();
            Random random = new Random();
            _memoryEditorData = Enumerable.Range(0, 1024).Select(i => (byte)random.Next(255)).ToArray();

            var stopwatch = Stopwatch.StartNew();
            float deltaTime = 0f;
            // Main application loop
            while (_window.Exists)
            {
                deltaTime = stopwatch.ElapsedTicks / (float)Stopwatch.Frequency;
                stopwatch.Restart();
                InputSnapshot snapshot = _window.PumpEvents();
                if (!_window.Exists) { break; }
                _controller.Update(deltaTime, snapshot); // Feed the input events to our ImGui controller, which passes them through to ImGui.

                SubmitUI();

                _cl.Begin();
                _cl.SetFramebuffer(_gd.MainSwapchain.Framebuffer);
                _cl.ClearColorTarget(0, new RgbaFloat(_clearColor.X, _clearColor.Y, _clearColor.Z, 1f));
                _controller.Render(_gd, _cl);
                _cl.End();
                _gd.SubmitCommands(_cl);
                _gd.SwapBuffers(_gd.MainSwapchain);
            }

            // Clean up Veldrid resources
            _gd.WaitForIdle();
            _controller.Dispose();
            _cl.Dispose();
            _gd.Dispose();
        }

        private static unsafe void SubmitUI()
        {
            // Demo code adapted from the official Dear ImGui demo program:
            // https://github.com/ocornut/imgui/blob/master/examples/example_win32_directx11/main.cpp#L172

            // 1. Show a simple window.
            // Tip: if we don't call ImGui.Begin(string) / ImGui.End() the widgets automatically appears in a window called "Debug".
            {
                ImGui.Text("");
                ImGui.Text(string.Empty);
                ImGui.Text("Hello, world!");                                        // Display some text (you can use a format string too)
                ImGui.SliderFloat("float", ref _f, 0, 1, _f.ToString("0.000"));  // Edit 1 float using a slider from 0.0f to 1.0f    
                //ImGui.ColorEdit3("clear color", ref _clearColor);                   // Edit 3 floats representing a color

                ImGui.Text($"Mouse position: {ImGui.GetMousePos()}");

                ImGui.Checkbox("ImGui Demo Window", ref _showImGuiDemoWindow);                 // Edit bools storing our windows open/close state
                ImGui.Checkbox("Another Window", ref _showAnotherWindow);
                ImGui.Checkbox("Memory Editor", ref _showMemoryEditor);
                if (ImGui.Button("Button"))                                         // Buttons return true when clicked (NB: most widgets return true when edited/activated)
                    _counter++;
                ImGui.SameLine(0, -1);
                ImGui.Text($"counter = {_counter}");

                ImGui.DragInt("Draggable Int", ref _dragInt);

                float framerate = ImGui.GetIO().Framerate;
                ImGui.Text($"Application average {1000.0f / framerate:0.##} ms/frame ({framerate:0.#} FPS)");
            }

            // 2. Show another simple window. In most cases you will use an explicit Begin/End pair to name your windows.
            if (_showAnotherWindow)
            {
                ImGui.Begin("Another Window", ref _showAnotherWindow);
                ImGui.Text("Hello from another window!");
                if (ImGui.Button("Close Me"))
                    _showAnotherWindow = false;
                ImGui.End();
            }

            // 3. Show the ImGui demo window. Most of the sample code is in ImGui.ShowDemoWindow(). Read its code to learn more about Dear ImGui!
            if (_showImGuiDemoWindow)
            {
                // Normally user code doesn't need/want to call this because positions are saved in .ini file anyway.
                // Here we just want to make the demo initial state a bit more friendly!
                ImGui.SetNextWindowPos(new Vector2(650, 20), ImGuiCond.FirstUseEver);
                ImGui.ShowDemoWindow(ref _showImGuiDemoWindow);
            }

            if (ImGui.TreeNode("Tabs"))
            {
                if (ImGui.TreeNode("Basic"))
                {
                    ImGuiTabBarFlags tab_bar_flags = ImGuiTabBarFlags.None;
                    if (ImGui.BeginTabBar("MyTabBar", tab_bar_flags))
                    {
                        if (ImGui.BeginTabItem("Avocado"))
                        {
                            ImGui.Text("This is the Avocado tab!\nblah blah blah blah blah");
                            ImGui.EndTabItem();
                        }
                        if (ImGui.BeginTabItem("Broccoli"))
                        {
                            ImGui.Text("This is the Broccoli tab!\nblah blah blah blah blah");
                            ImGui.EndTabItem();
                        }
                        if (ImGui.BeginTabItem("Cucumber"))
                        {
                            ImGui.Text("This is the Cucumber tab!\nblah blah blah blah blah");
                            ImGui.EndTabItem();
                        }
                        ImGui.EndTabBar();
                    }
                    ImGui.Separator();
                    ImGui.TreePop();
                }

                if (ImGui.TreeNode("Advanced & Close Button"))
                {
                    // Expose a couple of the available flags. In most cases you may just call BeginTabBar() with no flags (0).
                    ImGui.CheckboxFlags("ImGuiTabBarFlags_Reorderable", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.Reorderable);
                    ImGui.CheckboxFlags("ImGuiTabBarFlags_AutoSelectNewTabs", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.AutoSelectNewTabs);
                    ImGui.CheckboxFlags("ImGuiTabBarFlags_NoCloseWithMiddleMouseButton", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.NoCloseWithMiddleMouseButton);
                    if ((s_tab_bar_flags & (uint)ImGuiTabBarFlags.FittingPolicyMask) == 0)
                        s_tab_bar_flags |= (uint)ImGuiTabBarFlags.FittingPolicyDefault;
                    if (ImGui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyResizeDown", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.FittingPolicyResizeDown))
                        s_tab_bar_flags &= ~((uint)ImGuiTabBarFlags.FittingPolicyMask ^ (uint)ImGuiTabBarFlags.FittingPolicyResizeDown);
                    if (ImGui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyScroll", ref s_tab_bar_flags, (uint)ImGuiTabBarFlags.FittingPolicyScroll))
                        s_tab_bar_flags &= ~((uint)ImGuiTabBarFlags.FittingPolicyMask ^ (uint)ImGuiTabBarFlags.FittingPolicyScroll);

                    // Tab Bar
                    string[] names = { "Artichoke", "Beetroot", "Celery", "Daikon" };

                    for (int n = 0; n < s_opened.Length; n++)
                    {
                        if (n > 0) { ImGui.SameLine(); }
                        ImGui.Checkbox(names[n], ref s_opened[n]);
                    }

                    // Passing a bool* to BeginTabItem() is similar to passing one to Begin(): the underlying bool will be set to false when the tab is closed.
                    if (ImGui.BeginTabBar("MyTabBar", (ImGuiTabBarFlags)s_tab_bar_flags))
                    {
                        for (int n = 0; n < s_opened.Length; n++)
                            if (s_opened[n] && ImGui.BeginTabItem(names[n], ref s_opened[n]))
                            {
                                ImGui.Text($"This is the {names[n]} tab!");
                                if ((n & 1) != 0)
                                    ImGui.Text("I am an odd tab.");
                                ImGui.EndTabItem();
                            }
                        ImGui.EndTabBar();
                    }
                    ImGui.Separator();
                    ImGui.TreePop();
                }
                ImGui.TreePop();
            }

            ImGuiIOPtr io = ImGui.GetIO();
            SetThing(out io.DeltaTime, 2f);

            if (_showMemoryEditor)
            {
                ImGui.Text("Memory editor currently supported.");
                // _memoryEditor.Draw("Memory Editor", _memoryEditorData, _memoryEditorData.Length);
            }

            // ReadOnlySpan<char> and .NET Standard 2.0 tests
            TestStringParameterOnDotNetStandard.Text(); // String overloads should always be available.

            // On .NET Standard 2.1 or greater, you can use ReadOnlySpan<char> instead of string to prevent allocations.
            long allocBytesStringStart = GC.GetAllocatedBytesForCurrentThread();
            ImGui.Text($"Hello, world {Random.Shared.Next(100)}!");
            long allocBytesStringEnd = GC.GetAllocatedBytesForCurrentThread() - allocBytesStringStart;
            Console.WriteLine("GC (string): " + allocBytesStringEnd);

            long allocBytesSpanStart = GC.GetAllocatedBytesForCurrentThread();
            ImGui.Text($"Hello, world {Random.Shared.Next(100)}!".AsSpan()); // Note that this call will STILL allocate memory due to string interpolation, but you can prevent that from happening by using an InterpolatedStringHandler.
            long allocBytesSpanEnd = GC.GetAllocatedBytesForCurrentThread() - allocBytesSpanStart;
            Console.WriteLine("GC (span): " + allocBytesSpanEnd);

            ImGui.Text("Empty span:");
            ImGui.SameLine();
            ImGui.GetWindowDrawList().AddText(ImGui.GetCursorScreenPos(), uint.MaxValue, ReadOnlySpan<char>.Empty);
            ImGui.NewLine();
            ImGui.GetWindowDrawList().AddText(ImGui.GetCursorScreenPos(), uint.MaxValue, $"{ImGui.CalcTextSize("h")}");
            ImGui.NewLine();
            ImGui.TextUnformatted("TextUnformatted now passes end ptr but isn't cut off");
        }
    }
}
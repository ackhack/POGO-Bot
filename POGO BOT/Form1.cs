using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpAdbClient;
using Tesseract;

// check if arena is working
// get pixels for color check
// raids
// make ui actually useable monkaW

namespace POGO_BOT
{
    public partial class Form1 : Form
    {
        #region Variables
        private readonly AdbClient cli = new AdbClient();
        private DeviceData device;
        private int nPics = 0;
        private Bitmap currBitmap;
        private bool running = false;
        private int nMons = 0;
        private int nStops = 0;
        private string adbpath;
        private readonly bool canInit = false;
        private bool canRun = false;

        private int initButton = 0;

        private Point CloseButton;
        private Point RandomRocketClick;
        private Point DodgePokemon;
        private Point PokemonCaughtOK;
        private Point PokemonCaughtStrips;
        private Point PokemonCaughtTransfer;
        private Point PokemonCaughtTransferOK;
        private Point ArenaOpenStop;
        private Point OpenItembag;
        private Point SellFirstItem;
        private Point SellItemPlus;
        private Point SellItemOK;
        private Point ClickAreaTL;
        private Point ClickAreaBR;
        #endregion

        #region Generic
        public Form1()
        {
            InitializeComponent();

            if (!InitADBPath())
                return;

            if (!InitADBServer())
                return;

            if (!InitADBDevice())
                return;

            Show();

            if (Directory.Exists("tmp"))
                Directory.Delete("tmp", true);

#if DEBUG

            if (Debug())
            {
                UpdateStatus("Testing Completed");
                return;
            }
#endif
            canInit = true;
        }

        private bool InitADBPath()
        {
            try
            {
#if DEBUG
                adbpath = @"C:\ADB\adb.exe";
#endif
                if (adbpath == null)
                {
                    OpenFileDialog fd = new OpenFileDialog
                    {
                        Filter = "adb.exe|adb.exe"
                    };
                    fd.ShowDialog();
                    adbpath = fd.FileName;
                }
                return true;
            }
            catch (Exception)
            {
                UpdateStatus("ADB Path not valid");
                return false;
            }
        }
        private bool InitADBServer()
        {
            AdbServer server = new AdbServer();
            server.StartServer(adbpath, restartServerIfNewer: true);
            if (!server.GetStatus().IsRunning)
            {
                UpdateStatus("Couldnt start ADB Server");
                return false;
            }
            return true;
        }
        private bool InitADBDevice()
        {
            try
            {
#if DEBUG
                Process MyProcess = new Process
                {
                    StartInfo = new ProcessStartInfo("cmd", "/c adb connect 192.168.178.109:5555")
                    {
                        UseShellExecute = false
                    }
                };

                if (!MyProcess.Start())
                    UpdateStatus("Connection not successful");
                MyProcess.WaitForExit();
                device = cli.GetDevices()[0];
#endif

                if (cli.GetDevices().Count == 0)
                {
                    UpdateStatus("Please connect a Device via ADB");
                    return false;
                }
                if (device == null)
                    device = cli.GetDevices().First();

                return device != null;
            }
            catch (Exception)
            {
                UpdateStatus("Couldnt find a ADB Device");
                return false;
            }


        }
        private void BtnPic_Click(object sender, EventArgs e)
        {
            btnInit.Enabled = false;
            if (!canRun)
            {
                MessageBox.Show("Something did not work in Setup, please restart the Program");
                return;
            }

            running = !running;
            UpdateStatus("WORKING: " + running);
            if (running)
                ScanMap();
        }

        private void BtnInit_Click(object sender, EventArgs e)
        {
            if (!canInit)
            {
                MessageBox.Show("Something did not work in Setup, please restart the Program");
                return;
            }

            string size = ExecCommand("wm size");
            Match match = Regex.Match(size, @".* (\d+)x(\d+).*");
            int width = int.Parse(match.Groups[1].ToString());
            int height = int.Parse(match.Groups[2].ToString());
            UpdateStatus("Found Size: " + width + " " + height);
            MessageBox.Show("There will be Instructions on Screen," +
                " follow the Instructions on Screen, when the Program doesnt respond, wiggle a bit, so there are enough Inputs");
            btninstruct.Visible = true;
            btnRedo.Visible = true;
            lblInstruct.Visible = true;
            lblRelease.Visible = true;
            lblInstruct.Text = "Please start a Pokemon Fight \nand press the Flee Button";
            StartListeningTouch();
        }
        private void Btninstruct_Click(object sender, EventArgs e)
        {
            Point ret = ReadLastTouch();
            if (ret.X == -1)
            {
                StartListeningTouch();
                return;
            }

            switch (initButton)
            {
                case 0:
                    DodgePokemon = ret;
                    lblInstruct.Text = "Next catch a Pokemon\n and press the \nOK Button when it shows the XP";
                    initButton++;
                    break;

                case 1:
                    PokemonCaughtOK = ret;
                    lblInstruct.Text = "Next press on the 3 Strips in the \nBottom Right";
                    initButton++;
                    break;

                case 2:
                    PokemonCaughtStrips = ret;
                    lblInstruct.Text = "Next click on Transfer";
                    initButton++;
                    break;

                case 3:
                    PokemonCaughtTransfer = ret;
                    lblInstruct.Text = "Next press the OK Button when Transfering";
                    initButton++;
                    break;

                case 4:
                    PokemonCaughtTransferOK = ret;
                    lblInstruct.Text = "Next click on an Arena -> \ntop right to open pokestop";
                    initButton++;
                    break;

                case 5:
                    ArenaOpenStop = ret;
                    lblInstruct.Text = "Next click on the white part of \nthe Pokeball in the Bottom-Middle";
                    initButton++;
                    break;

                case 6:
                    CloseButton = ret;
                    lblInstruct.Text = "Next click in the Top Half \nof the Screen to Skip Team Rocket";
                    initButton++;
                    break;

                case 7:
                    RandomRocketClick = ret;
                    lblInstruct.Text = "Next Press on the Items Button";
                    initButton++;
                    break;

                case 8:
                    OpenItembag = ret;
                    lblInstruct.Text = "Next click on the Top Half \nof the first TrashCan";
                    initButton++;
                    break;

                case 9:
                    SellFirstItem = ret;
                    lblInstruct.Text = "Next click on the Plus \nwhen Tossing Items";
                    initButton++;
                    break;

                case 10:
                    SellItemPlus = ret;
                    lblInstruct.Text = "Next click on the OK button \nwhen Tossing Items";
                    initButton++;
                    break;

                case 11:
                    SellItemOK = ret;
                    //run init proccess
                    canRun = true;
                    btnPic.Enabled = true;
                    lblRelease.Visible = false;
                    lblInstruct.Visible = false;
                    btnRedo.Visible = false;
                    btninstruct.Visible = false;
                    UpdateStatus("Finished Button init");
                    return;
            }
            StartListeningTouch();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            lblRelease.Text = "Waiting for new Input";
            StartListeningTouch();
            UpdateStatus("Restarted TouchInput");
        }

        ConsoleOutputReceiver receiver;

        private void StartListeningTouch()
        {
            receiver = new ConsoleOutputReceiver();
            Thread listener = new Thread(TouchListening);
            object[] arr = new object[2];
            arr[0] = receiver;
            arr[1] = device;
            listener.Start(arr);
            listener.Join();
            lblRelease.Text = "Finished Reading Touch";
        }

        private void TouchListening(object rec)
        {
            object[] arr = (object[])rec;
            ConsoleOutputReceiver receiver = (ConsoleOutputReceiver)arr[0];
            DeviceData device = (DeviceData)arr[1];

            cli.ExecuteRemoteCommand("getevent -ltqc 100", device, receiver);
        }

        private Point ReadLastTouch()
        {
            lblRelease.Text = "Ready for new Input";
            string patternX = @"^\[.*\] \/dev\/input\/event1: EV_ABS       ABS_MT_POSITION_X    ([^ ]+)";
            string patternY = @"^\[.*\] \/dev\/input\/event1: EV_ABS       ABS_MT_POSITION_Y    ([^ ]+)";

            string output = receiver.ToString();
            receiver = null;

            MatchCollection matchX = Regex.Matches(output, patternX, RegexOptions.Multiline);
            MatchCollection matchY = Regex.Matches(output, patternY, RegexOptions.Multiline);

            if (matchX.Count == 0 || matchY.Count == 0)
            {
                MessageBox.Show("Please retry, Couldnt find some Coords");
                return new Point(-1, -1);
            }

            string hexX = matchX[matchX.Count-1].Groups[1].ToString();
            string hexY = matchY[matchY.Count - 1].Groups[1].ToString();

            int intX = Convert.ToInt32(hexX, 16);
            int intY = Convert.ToInt32(hexY, 16);

            UpdateStatus("Found Point: " + intX + " " + intY);

            return new Point(intX, intY);
        }

        private string ExecCommand(string com)
        {
            if (com.StartsWith("adb "))
                com = com.Substring(4);
            if (com.StartsWith("shell "))
                com = com.Substring(6);
            var receiver = new ConsoleOutputReceiver();
            cli.ExecuteRemoteCommand(com, device, receiver);
            return receiver.ToString();
        }

        [DebuggerStepThrough]
        private void TapAt(Point p)
        {
            TapAt(p.X, p.Y);
        }
        [DebuggerStepThrough]
        private void TapAt(int x, int y)
        {
            ExecCommand("input tap " + x + " " + y);
        }
        [DebuggerStepThrough]
        private void Swipe(Point start, Point end, int duration = 1500)
        {
            Swipe(start.X, start.Y, end.X, end.Y, duration);
        }
        [DebuggerStepThrough]
        private void Swipe(int startx, int starty, int endx, int endy, int duration = 1500)
        {
            ExecCommand("input swipe " + startx + " " + starty + " " + endx + " " + endy + " " + duration);
        }

        [DebuggerStepThrough]
        private void UpdateStatus(string str)
        {
            System.Diagnostics.Debug.WriteLine(str);
        }
        #endregion

        #region Handler
        private async Task HandlePokeStop()
        {
            Swipe(10, 1000, 500, 1000, 150);
            await Task.Delay(500);
            TapAt(CloseButton);
            await Task.Delay(1000);
            await TossItems();
        }

        private async Task HandleRocketStop()
        {
            Swipe(10, 1000, 500, 1000, 150);
            await Task.Delay(500);
            TapAt(CloseButton);
            await Task.Delay(5000);
            for (int i = 0; i < 5; i++)
            {
                UpdateStatus("CLICKED ROCKET");
                TapAt(RandomRocketClick);
                await Task.Delay(1000);
            }

            UpdateStatus("FINISHED ROCKET");
            TapAt(CloseButton);
            await Task.Delay(1000);
            TapAt(CloseButton);
            await Task.Delay(1000);
            TapAt(CloseButton);
            await Task.Delay(1000);
            await TossItems();
        }

        private async Task HandlePokemon(bool first = true)
        {
            Swipe(480, 1880, 500, 1980, 78);
            Swipe(500, 1980, 505, 1400, 120);

            await Task.Delay(14_000);
            TakeScreenShot();

            Color clr = currBitmap.GetPixel(150, 1500);
            if (clr.R > 230 & clr.G > 230 && clr.B > 230)
            {
                await HandleCaughtPokemon();
            }
            else
            {
                if (first)
                {
                    await HandlePokemon(false);
                }
                else
                {
                    TapAt(DodgePokemon);
                    await Task.Delay(500);
                }
            }
        }

        private async Task HandleCaughtPokemon()
        {
            nMons++;
            lblMons.Text = nMons.ToString();
            TapAt(PokemonCaughtOK);
            await Task.Delay(1000);
            TapAt(PokemonCaughtStrips);
            await Task.Delay(200);
            TapAt(PokemonCaughtTransfer);
            await Task.Delay(200);
            TapAt(PokemonCaughtTransferOK);
            await Task.Delay(5000);
        }

        private async Task HandleArena()
        {
            TapAt(ArenaOpenStop);
            await Task.Delay(1000);
            Swipe(10, 1000, 500, 1000, 150);
            await Task.Delay(500);
            TapAt(CloseButton);
            await Task.Delay(1000);
            TapAt(CloseButton);
            await Task.Delay(1000);
            await TossItems();
        }

        private async Task HandleBallon()
        {
            await Task.Delay(5000);
            for (int i = 0; i < 5; i++)
            {
                UpdateStatus("CLICKED ROCKET");
                TapAt(RandomRocketClick);
                await Task.Delay(1000);
            }

            UpdateStatus("FINISHED ROCKET");
            TapAt(CloseButton);
            await Task.Delay(1000);
        }

        private async Task HandleMap()
        {
            UpdateStatus("LOOKING AT MAP");
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                int x = rnd.Next(ClickAreaTL.X, ClickAreaBR.X);
                int y = rnd.Next(ClickAreaTL.Y, ClickAreaBR.Y);
                TapAt(x, y);
                await Task.Delay(50);
            }
            UpdateStatus("FINISHED RANDOM CLICKS");
            await Task.Delay(500);
        }

        private async Task TossItems()
        {
            if (!cbToss.Checked)
            {
                return;
            }

            UpdateStatus("TOSSING");

            //Open Items
            TapAt(CloseButton);
            await Task.Delay(500);
            TapAt(OpenItembag);
            await Task.Delay(500);

            #region Potions

            string patternpotion = @".*Potion.*";
            List<string> list = GetTextList();
            int nPotions = 0;

            list.ForEach(item =>
            {
                if (Regex.IsMatch(item, patternpotion))
                    nPotions++;
            });

            if (nPotions > 1)
            {
                bool foundpotion = false;

                for (int j = 0; j < nPotions - 1; j++)
                {
                    UpdateStatus("TOSSING Potion" + j);
                    foundpotion = false;
                    if (list.Any(item =>
                    {
                        bool ret = Regex.IsMatch(item, patternpotion);
                        return ret;
                    }))
                    {
                        foundpotion = true;
                        TapAt(SellFirstItem);
                        for (int i = 0; i < 5; i++)
                        {
                            TapAt(SellItemPlus);
                        }
                        TapAt(SellItemOK);
                    }
                    if (!foundpotion)
                        break;

                    await Task.Delay(2000);
                }
                UpdateStatus("FINISHED POTION");
            }
            else
            {
                UpdateStatus("NO POTION");
            }


            #endregion

            UpdateStatus("SWIPING TO BERRYS");
            await SwipeToBerrys();

            #region Berrys

            list = GetTextList();
            bool foundberry = false;
            string patternBerries = @".*Berry.*";
            int nBerries = 0;

            list.ForEach(item =>
            {
                if (Regex.IsMatch(item, patternBerries))
                    nBerries++;
            });

            if (nBerries > 2)
            {
                Color clr = currBitmap.GetPixel(160, 520);
                if (clr.R <= clr.B || clr.R <= clr.G)
                {
                    Swipe(400, 2100, 400, 2030, 500);
                    clr = currBitmap.GetPixel(160, 620);
                    if (clr.R <= clr.B || clr.R <= clr.G)
                        return;
                }

                for (int k = 0; k < nBerries - 2; k++)
                {
                    UpdateStatus("TOSSING Berry" + k);
                    if (list.Any(item => Regex.IsMatch(item, patternBerries)))
                    {
                        foundberry = true;
                        TapAt(SellFirstItem);
                        for (int i = 0; i < 10; i++)
                        {
                            TapAt(SellItemPlus);
                        }
                        TapAt(SellItemOK);
                    }
                    if (!foundberry)
                        break;

                    await Task.Delay(2000);
                }
            }
            #endregion

            //Close Menu
            UpdateStatus("END TOSSING");
            await Task.Delay(500);
            TapAt(CloseButton);
            await Task.Delay(500);
        }

        private List<string> GetTextList()
        {
            //New Screenshot
            TakeScreenShot();

            //Get Text from Screenshot
            List<string> list = new List<string>();

            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                //using (Page page = TesseractDrawingExtensions.Process(engine, currBitmap))
                using (Page page = engine.Process(Pix.LoadFromFile("tmp\\" + (nPics - 1).ToString() + ".png")))
                {
                    using (var iter = page.GetIterator())
                    {
                        iter.Begin();
                        do
                        {
                            do
                            {
                                do
                                {
                                    list.Add(iter.GetText(PageIteratorLevel.TextLine));
                                } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                            } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                        } while (iter.Next(PageIteratorLevel.Block));
                    }
                }
            }
            return list;
        }

        private async Task SwipeToBerrys()
        {
            Swipe(400, 2100, 400, 790, 2000);
            await Task.Delay(500);
            Swipe(400, 2100, 400, 790, 2000);
            await Task.Delay(500);
            Swipe(400, 2100, 400, 790, 2000);
            await Task.Delay(500);
            Swipe(400, 2100, 400, 790, 2000);
            await Task.Delay(500);
            Swipe(400, 2100, 400, 790, 2000);
            await Task.Delay(500);
            Swipe(400, 2100, 400, 1438, 3900);
        }

        #endregion

        #region Loop
        private async void ScanMap()
        {
            if (TakeScreenShot())
            {
                if (!await ScanScreenShot())
                {
                    await Task.Delay(2000);
                }
                else
                {
                    MoveCam();
                }
            }
            if (running)
                ScanMap();
            else
                UpdateStatus("STOPPED");
        }

        private void MoveCam()
        {
            UpdateStatus("MOVING CAMERA");
            Swipe(100, 1000, 500, 1000, 500);
        }

        private bool TakeScreenShot()
        {
            Process MyProcess = new Process
            {
                StartInfo = new ProcessStartInfo("cmd")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = "/c start /min /wait GET_SCREENSHOT.bat " + nPics
                }
            };

            if (!MyProcess.Start())
            {
                return false;
            }
            MyProcess.WaitForExit();

            if (File.Exists(@"tmp\" + nPics + ".png"))
            {
                currBitmap = new Bitmap(@"tmp\" + nPics + ".png");
                Image old = pbResult.Image;
                pbResult.Image = currBitmap;
                if (old != null)
                    old.Dispose();

                pbResult.SizeMode = PictureBoxSizeMode.StretchImage;
                nPics++;
                try
                {
                    File.Delete("tmp\\" + (nPics - 2).ToString() + ".png");
                }
                catch (Exception) { }
                return true;
            }
            return false;
        }

        private async Task<bool> ScanScreenShot()
        {
            Color clr = currBitmap.GetPixel(144, 2000);

            //PokeStops
            if (clr.R > 65 && clr.R < 80 && clr.G > 65 && clr.G < 80 && clr.B > 65 && clr.B < 80)
            {
                UpdateStatus("PROB POKESTOP ROCKET");
                await HandleRocketStop();
                nStops++;
                lblStops.Text = nStops.ToString();
                return true;
            }

            if (clr.B > clr.R && clr.B > clr.G)
            {
                UpdateStatus("PROB POKESTOP");
                await HandlePokeStop();
                nStops++;
                lblStops.Text = nStops.ToString();
                return true;
            }

            //Arena
            if (clr.R > 240 && clr.G > 165 && clr.G < 200 && clr.B > 130 && clr.B < 200)
            {
                UpdateStatus("PROB ARENA");
                await HandleArena();
                nStops++;
                lblStops.Text = nStops.ToString();
                return true;
            }

            clr = currBitmap.GetPixel(920, 1860);

            //Pokemon
            if (clr.R > clr.B && clr.R > clr.G)
            {
                UpdateStatus("PROB POKEMON IN FIGHT");
                await HandlePokemon();
                return true;
            }

            clr = currBitmap.GetPixel(540, 2000);

            //Ballon Grunt
            if ((clr.R < 50 && clr.G < 50 && clr.B < 50))
            {
                UpdateStatus("PROB BALOON");
                await HandleBallon();
                return true;
            }

            //Any Notification esc
            if (clr.Equals(Color.FromArgb(28, 135, 150)))
            {
                UpdateStatus("PROB NOTIFICATION");
                TapAt(CloseButton);
                await Task.Delay(1000);
                return true;
            }

            //On Map
            await HandleMap();

            return false;
        }

        #endregion

        #region dev
        private bool Debug()
        {
            //Default Values for A2 Lite (1080x2280)
            CloseButton = new Point(530, 2000);
            RandomRocketClick = new Point(500, 1250);
            DodgePokemon = new Point(90, 250);
            PokemonCaughtOK = new Point(500, 1500);
            PokemonCaughtStrips = new Point(900, 1950);
            PokemonCaughtTransfer = new Point(720, 1760);
            PokemonCaughtTransferOK = new Point(500, 1250);
            ArenaOpenStop = new Point(950, 300);
            OpenItembag = new Point(850, 1800);
            SellFirstItem = new Point(950, 450);
            SellItemPlus = new Point(800, 1000);
            SellItemOK = new Point(500, 1350);
            ClickAreaTL = new Point(360, 1150);
            ClickAreaBR = new Point(750, 1600);

            canRun = true;
            btnPic.Enabled = true;
            return Testing();
        }

        //true if Programm should stop afterwards
        private bool Testing()
        {
            return false;
        }

        private void PnlColor_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(pnlColor.BackColor.ToString());
        }

        #endregion
    }
}

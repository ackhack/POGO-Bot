using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpAdbClient;
using Tesseract;

namespace POGO_BOT
{
    public partial class Form1 : Form
    {
        readonly AdbClient cli = new AdbClient();
        readonly DeviceData device;
        private int nPics = 0;
        private Bitmap currBitmap;
        private bool running = false;
        private int nMons = 0;
        private int nStops = 0;

        #region Generic
        public Form1()
        {
            InitializeComponent();
            AdbServer server = new AdbServer();
            server.StartServer(@"C:\ADB\adb.exe", restartServerIfNewer: false);
            device = cli.GetDevices().First();

#if DEBUG
            ExecCommand("tcpip 5555");
            ExecCommand("connect 192.168.178.109:5555");
            device = cli.GetDevices()[0];
#endif

            if (Directory.Exists("tmp"))
                Directory.Delete("tmp", true);
        }

        private void BtnPic_Click(object sender, EventArgs e)
        {
            //_ = TossItems();
            //return;
            running = !running;
            Debug.WriteLine("WORKING: " + running);
            if (running)
                ScanMap();
        }

        [DebuggerStepThrough]
        private string ExecCommand(string com)
        {
            var receiver = new ConsoleOutputReceiver();
            cli.ExecuteRemoteCommand(com, device, receiver);
            return receiver.ToString();
        }
        #endregion

        #region Handler
        private async Task SpinPokeStop()
        {
            ExecCommand("input swipe 10 1000 500 1000 150");
            await Task.Delay(500);
            ExecCommand("input tap 530 1950");
            await Task.Delay(1000);
            await TossItems();
        }

        private async Task HandleRocketStop()
        {
            ExecCommand("input swipe 10 1000 500 1000 150");
            await Task.Delay(500);
            ExecCommand("input tap 530 1950");
            await Task.Delay(5000);
            for (int i = 0; i < 5; i++)
            {
                Debug.WriteLine("Clicked Rocket");
                ExecCommand("input tap 500 1250");
                await Task.Delay(1000);
            }

            Debug.WriteLine("Finished Rocket");
            ExecCommand("input tap 530 1950");
            await Task.Delay(1000);
            ExecCommand("input tap 530 1950");
            await Task.Delay(1000);
            ExecCommand("input tap 530 1950");
            await Task.Delay(1000);
            await TossItems();
        }

        private async Task CatchPokemon(bool first = true)
        {
            ExecCommand("input swipe 480 1880 500 1980 78");
            ExecCommand("input swipe 500 1980 505 1400 120");

            await Task.Delay(14_000);
            TakeScreenShot();

            Color clr = currBitmap.GetPixel(150, 1500);
            if (clr.R > 230 & clr.G > 230 && clr.B > 230)
            {
                await CaughtPokemon();
            }
            else
            {
                if (first)
                {
                    await CatchPokemon(false);
                }
                else
                {
                    ExecCommand("input tap 90 250");
                    await Task.Delay(500);
                }
            }
        }

        private async Task CaughtPokemon()
        {
            nMons++;
            lblMons.Text = nMons.ToString();
            ExecCommand("input tap 500 1500");
            await Task.Delay(1000);
            ExecCommand("input tap 900 1950");
            await Task.Delay(200);
            ExecCommand("input tap 720 1760");
            await Task.Delay(200);
            ExecCommand("input tap 500 1250");
            await Task.Delay(5000);
        }

        private async Task HandleArena()
        {
            ExecCommand("input tap 950 300");
            await Task.Delay(1000);
            ExecCommand("input swipe 10 1000 500 1000 150");
            await Task.Delay(500);
            ExecCommand("input tap 530 1950");
            await Task.Delay(1000);
            ExecCommand("input tap 530 1950");
            await Task.Delay(1000);
            await TossItems();
        }

        private async Task HandleBallon()
        {
            await Task.Delay(5000);
            for (int i = 0; i < 5; i++)
            {
                Debug.WriteLine("Clicked Rocket");
                ExecCommand("input tap 500 1250");
                await Task.Delay(1000);
            }

            Debug.WriteLine("Finished Rocket");
            ExecCommand("input tap 530 1950");
            await Task.Delay(1000);
        }

        private async Task HandleMap()
        {
            Debug.WriteLine("Looking at Map");
            Point topleft = new Point(360, 1150);
            Point botright = new Point(750, 1600);
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                int x = rnd.Next(topleft.X, botright.X);
                int y = rnd.Next(topleft.Y, botright.Y);
                ExecCommand("input tap " + x.ToString() + " " + y.ToString());
                await Task.Delay(50);
            }
            Debug.WriteLine("Finished Random Clicks");
            await Task.Delay(500);
        }

        private async Task TossItems()
        {
            if (!checkBox1.Checked)
            {
                return;
            }

            Debug.WriteLine("TOSSING");

            //Open Items
            ExecCommand("input tap 540 1950");
            await Task.Delay(500);
            ExecCommand("input tap 850 1800");
            await Task.Delay(500);

            #region Potions

            string patternpotion = @".*Potion.*";
            List<string> list = getTextList();
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
                    Debug.WriteLine("TOSSING Potion" + j);
                    foundpotion = false;
                    if (list.Any(item =>
                    {
                        bool ret = Regex.IsMatch(item, patternpotion);
                        return ret;
                    }))
                    {
                        foundpotion = true;
                        ExecCommand("input tap 950 450");
                        for (int i = 0; i < 5; i++)
                        {
                            ExecCommand("input tap 800 1000");
                        }
                        ExecCommand("input tap 500 1350");
                    }
                    if (!foundpotion)
                        break;

                    await Task.Delay(2000);
                }
            }


            #endregion

            Debug.WriteLine("SWIPING TO BERRYS");
            ExecCommand("input swipe 400 2100 400 790 2000");
            await Task.Delay(500);
            ExecCommand("input swipe 400 2100 400 790 2000");
            await Task.Delay(500);
            ExecCommand("input swipe 400 2100 400 790 2000");
            await Task.Delay(500);
            ExecCommand("input swipe 400 2100 400 790 2000");
            await Task.Delay(500);
            ExecCommand("input swipe 400 2100 400 790 2000");
            await Task.Delay(500);
            ExecCommand("input swipe 400 2100 400 1438 3900");

            #region Berrys

            list = getTextList();
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
                    ExecCommand("input swipe 400 2100 400 2030 500");
                    clr = currBitmap.GetPixel(160, 620);
                    if (clr.R <= clr.B || clr.R <= clr.G)
                        return;
                }

                for (int k = 0; k < nBerries - 2; k++)
                {
                    Debug.WriteLine("TOSSING Berry" + k);
                    if (list.Any(item => Regex.IsMatch(item, patternBerries)))
                    {
                        foundberry = true;
                        ExecCommand("input tap 950 450");
                        for (int i = 0; i < 10; i++)
                        {
                            ExecCommand("input tap 800 1000");
                        }
                        ExecCommand("input tap 500 1350");
                    }
                    if (!foundberry)
                        break;

                    await Task.Delay(2000);
                }
            }
            #endregion

            //Close Menu
            Debug.WriteLine("END TOSSING");
            await Task.Delay(500);
            ExecCommand("input tap 540 2000");
            await Task.Delay(500);
        }

        private List<string> getTextList()
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
                    Debug.WriteLine("MOVING CAMERA");
                    ExecCommand("input swipe 100 1000 500 1000 500");
                }
            }
            if (running)
                ScanMap();
            else
                Debug.WriteLine("STOPPED");
        }

        private bool TakeScreenShot()
        {
            ProcessStartInfo ProcStartInfo = new ProcessStartInfo("cmd")
            {
                UseShellExecute = false,
                CreateNoWindow = true
            };
            Process MyProcess = new Process();
            ProcStartInfo.Arguments = "/c start /min /wait GET_SCREENSHOT.bat " + nPics;
            MyProcess.StartInfo = ProcStartInfo;
            MyProcess.Start();
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
                    File.Delete("tmp\\" + (nPics-2).ToString() + ".png");
                } catch (Exception) { }
                return true;
            }
            return false;
        }

        private async Task<bool> ScanScreenShot()
        {
            Color clr = currBitmap.GetPixel(144, 2000);

            //Arena
            if (clr.Equals(Color.FromArgb(255, 255, 255, 255)))
            {
                Debug.WriteLine("PROB ARENA");
                await HandleArena();
                nStops++;
                lblStops.Text = nStops.ToString();
                return true;
            }

            //PokeStops
            if (clr.R > 65 && clr.R < 80 && clr.G > 65 && clr.G < 80 && clr.B > 65 && clr.B < 80)
            {
                Debug.WriteLine("PROB POKESTOP ROCKET");
                await HandleRocketStop();
                nStops++;
                lblStops.Text = nStops.ToString();
                return true;
            }

            if (clr.B > clr.R && clr.B > clr.G)
            {
                Debug.WriteLine("PROB POKESTOP");
                await SpinPokeStop();
                nStops++;
                lblStops.Text = nStops.ToString();
                return true;
            }

            //Pokemon
            clr = currBitmap.GetPixel(920, 1860);
            if (clr.R > clr.B && clr.R > clr.G)
            {
                Debug.WriteLine("PROB POKEMON IN FIGHT");
                await CatchPokemon();
                return true;
            }

            //Ballon Grunt
            clr = currBitmap.GetPixel(540, 2000);
            if ((clr.R < 50 && clr.G < 50 && clr.B < 50))
            {
                Debug.WriteLine("PROB BALOON");
                await HandleBallon();
                return true;
            }

            //Any Notification esc
            if (clr.Equals(Color.FromArgb(28, 135, 150)))
            {
                Debug.WriteLine("PROB NOTIFICATION");
                ExecCommand("input tap 540 2000");
                await Task.Delay(1000);
                return true;
            }

            //On Map
            await HandleMap();

            return false;
        }

        #endregion

        #region dev

        private void PnlColor_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(pnlColor.BackColor.ToString());
        }

        #endregion
    }
}

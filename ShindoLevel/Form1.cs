using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace ShindoLevel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Root
        {
            public int l { get; set; }
            public int g { get; set; }
            public int y { get; set; }
            public int r { get; set; }
            public int e { get; set; }
            public String p { get; set; }
        }

        private readonly HttpClient client = new HttpClient();

        private async void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                var url = "https://kwatch-24h.net/EQLevel.json";
                var json = await client.GetStringAsync(url);
                var s_lv = JsonConvert.DeserializeObject<Root>(json);
                if (s_lv != null)
                {
                    var lv = s_lv.l;//←振動レベル本体
                    var green = s_lv.g;//←緑点
                    var yellow = s_lv.y;//←黄点
                    var red = s_lv.r;//←赤点
                    var eew_ = s_lv.e;//←何の内容なのかは未検証
                    var prac = s_lv.p;//←地震検知時のみ現れる。内容は数字で都道府県番号に合うようになっている。

                    label1.Text = "振動レベル:Lv"+ lv.ToString();
                }
                else
                {
                    goto onError;
                }
            }
            catch
            {
                goto onError;
            }
            return;
        onError:
            timer1.Enabled=false;
            label1.Text = "振動レベル:Lv ----";
            await Task.Delay(100);
            timer1.Enabled = true;
        }
    }
}

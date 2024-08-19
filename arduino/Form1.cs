using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
#pragma warning disable format

namespace arduino
{
    public partial class Form1 : Form
    {
        bool port;
        private TelegramBotClient botClient;
        private CancellationTokenSource cts;
        private const long id = 5003761244;
        public String MISHa;
        public bool l;
        public bool perem;
        public bool n;
        public bool nag;
        bool s;
      //  public bool l;
       // private int[] timeforday = { 0, };
#pragma warning disable format
 

        public Form1()
#pragma warning restore format
        {
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }
            StreamReader reader = new StreamReader("Resources\\output.txt");
            try
            {
                string[] strings = reader.ReadToEnd().Split('\n');

                textBox14.Text = strings[0];
                textBox13.Text = strings[1];
                textBox12.Text = strings[2];
                textBox11.Text = strings[3];
                textBox10.Text = strings[4];
                textBox9.Text = strings[5];
                textBox7.Text = strings[6];
                textBox8.Text = strings[7];
                image(textBox14);
                image(textBox13);
                image(textBox12);
                image(textBox11);
                image(textBox10);
                image(textBox9);
                image(textBox8);
                image(textBox7);
            }
            catch
            {
                textBox14.Text = "";
                textBox13.Text = "";
                textBox12.Text = "";
                textBox11.Text = "";
                textBox10.Text = "";
                textBox9.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
            }
            finally
            {
                reader.Close();
            }
#pragma warning disable format
#pragma warning disable format
#pragma warning disable format
            //timeforday = new int[4, 2, 1, 1];
#pragma warning restore format
#pragma warning restore format
#pragma warning restore format
      //      timeforday = { { 10, 12, 15, 18}, { 0,0,0}, { }; { } };

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!port)
            {
                serialPort1.PortName = comboBox1.SelectedItem.ToString();
                s = true;
                serialPort1.Open();
             //   Task.Run(async () => await StartSendingMessagesAsync());
                botClient = new TelegramBotClient("6714861937:AAHlcVyd0u5K2B2fZIkVCREk2bLkTIAjdAg");

                // Запуск получения обновлений
                cts = new CancellationTokenSource();
                ReceiverOptions receiverOptions = new ReceiverOptions();
                chart1.ChartAreas[0].AxisY.Maximum = 500;
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
                chart1.Series[0].XValueType = ChartValueType.DateTime;
                chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Minutes;
                chart1.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
                chart1.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddMinutes(5).ToOADate();
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.Series[1].XValueType = ChartValueType.DateTime;
                chart1.Series[2].XValueType = ChartValueType.DateTime;
                chart1.Series[3].XValueType = ChartValueType.DateTime;
                chart1.Series[5].XValueType = ChartValueType.DateTime;
                if (comboBox1.SelectedItem.ToString() == "4 раза в день")
                {
                    MISHa = DateTime.Now.AddHours(6).ToShortTimeString();
                }
                else if (comboBox1.SelectedItem.ToString() == "2 раза в день")
                {
                    MISHa = DateTime.Now.AddHours(12).ToShortTimeString();
                }
                else if (comboBox1.SelectedItem.ToString() == "1 раз в день")
                {
                    MISHa = DateTime.Now.AddHours(24).ToShortTimeString();
                }
                timer3.Enabled = true;

#pragma warning disable format
// chart1.Series[6].XValueType = ChartValueType.DateTime;

                botClient.StartReceiving(
                    HandleUpdateAsync,
                    HandleErrorAsync,
                    receiverOptions,
                    cancellationToken: cts.Token
                );
//#pragma warning restore format

                // Запуск отправки сообщений
               

                //erialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            timer1.Enabled = true;
                port = true;
                button1.Text = "Закрыть порт";

            }
            else
            {
                button1.Text = "Открыть порт";
                serialPort1.Close();
                port = false;
                s = false;
                cts?.Cancel();
                timer1.Enabled = false;
                timer3.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                       string[] data = serialPort1.ReadLine().Split(',');
                       textBox1.Text = data[0];
                       textBox2.Text = data[5];
                       textBox6.Text = data[1];
                       textBox3.Text = data[4];
                       textBox4.Text = data[2];
                       textBox5.Text = data[3];

                       if (Convert.ToInt16(data[6]) == 1) { pictureBox1.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox14.Text, cancellationToken: CancellationToken.None); }
                       else pictureBox1.Image = Image.FromFile("Resources\\green.png");
           #pragma warning disable format
                       if (Convert.ToInt16(data[7]) == 1) {pictureBox2.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox13.Text, cancellationToken: CancellationToken.None); }
           #pragma warning restore format
                       else pictureBox2.Image = Image.FromFile("Resources\\green.png");
           #pragma warning disable format
                       if (Convert.ToInt16(data[8]) == 1) {pictureBox3.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox12.Text, cancellationToken: CancellationToken.None); }
           #pragma warning restore format
                       else pictureBox3.Image = Image.FromFile("Resources\\green.png");
           #pragma warning disable format
                       if (Convert.ToInt16(data[9]) == 1) {pictureBox4.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox11.Text, cancellationToken: CancellationToken.None); }
           #pragma warning restore format
                       else pictureBox4.Image = Image.FromFile("Resources\\green.png");
           #pragma warning disable format
                       if (Convert.ToInt16(data[10]) == 1){ pictureBox5.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox10.Text, cancellationToken: CancellationToken.None); }
           #pragma warning restore format
                       else pictureBox5.Image = Image.FromFile("Resources\\green.png");
           #pragma warning disable format
                       if (Convert.ToInt16(data[11]) == 1){ pictureBox6.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox9.Text, cancellationToken: CancellationToken.None); }
           #pragma warning restore format
                       else pictureBox6.Image = Image.FromFile("Resources\\green.png");
           #pragma warning disable format
                       if (Convert.ToInt16(data[12]) == 1){ pictureBox7.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox8.Text, cancellationToken: CancellationToken.None); }
           #pragma warning restore format
                       else pictureBox7.Image = Image.FromFile("Resources\\green.png");
           #pragma warning disable format
                       if (Convert.ToInt16(data[13]) == 1){ pictureBox8.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox7.Text, cancellationToken: CancellationToken.None); }
           #pragma warning restore format
                       else pictureBox8.Image = Image.FromFile("Resources\\green.png");



                       if (Convert.ToInt32(textBox1.Text) < min1.Value)
           #pragma warning disable format
                       {
                           botClient.SendTextMessageAsync(
                               chatId: id,
                               text: "Темпиратура 1 зоны занижена",
                               cancellationToken: CancellationToken.None
                           );
                       }else if (Convert.ToInt32(textBox1.Text) > max1.Value)
           #pragma warning disable format
                           {
                           botClient.SendTextMessageAsync(
                               chatId: id,
                               text: "Темпиратура 1 зоны завышена",
                               cancellationToken: CancellationToken.None
                           );
                       }
           #pragma warning restore format
           #pragma warning restore format

                       if (Convert.ToInt32(textBox2.Text) < min2.Value)
                       {
                           botClient.SendTextMessageAsync(
                               chatId: id,
                               text: "Темпиратура 2 зоны занижена",
                               cancellationToken: CancellationToken.None
                           );
                       }
                       else if (Convert.ToInt32(textBox2.Text) > numericUpDown1.Value)
                       {
                           botClient.SendTextMessageAsync(
                               chatId: id,
                               text: "Темпиратура 2 зоны завышена",
                               cancellationToken: CancellationToken.None
                           );
                       }

                       if (Convert.ToInt32(textBox6.Text) < min3.Value)
                       {
                           botClient.SendTextMessageAsync(
                               chatId: id,
                               text: "Темпиратура 3 зоны занижена",
                               cancellationToken: CancellationToken.None
                           );
                       }
                       else if (Convert.ToInt32(textBox6.Text) > max3.Value)
                       {
                           botClient.SendTextMessageAsync(
                               chatId: id,
                               text: "Темпиратура 3 зоны завышена",
                               cancellationToken: CancellationToken.None
                           );
                       }
                       DateTime dt = DateTime.Now;

                       chart1.Series[0].Points.AddXY(dt, Convert.ToDouble(data[0]));
                       chart1.Series[1].Points.AddXY(dt, Convert.ToDouble(data[5]));
                       chart1.Series[2].Points.AddXY(dt, Convert.ToDouble(data[1]));
                       chart1.Series[3].Points.AddXY(dt, Convert.ToDouble(data[4]));
                       chart1.Series[4].Points.AddXY(dt, Convert.ToDouble(data[2]));
                       chart1.Series[5].Points.AddXY(dt, Convert.ToDouble(data[3]));
        }

#pragma warning disable format
     /*      private async Task StartSendingMessagesAsync()
                    {
           while (s)
            {
                await Task.Delay(10000);
                await SendMessage();
            }
                    }
     */
       /* async Task SendMessage()
         {
            string[] data = serialPort1.ReadLine().Split(',');
            textBox1.Text = data[0];
            textBox2.Text = data[5];
            textBox6.Text = data[1];
            textBox3.Text = data[4];
            textBox4.Text = data[2];
            textBox5.Text = data[3];

            if (Convert.ToInt16(data[6]) == 1) { pictureBox1.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox14.Text, cancellationToken: CancellationToken.None); }
            else pictureBox1.Image = Image.FromFile("Resources\\green.png");
#pragma warning disable format
            if (Convert.ToInt16(data[7]) == 1) {pictureBox2.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox13.Text, cancellationToken: CancellationToken.None); }
#pragma warning restore format
            else pictureBox2.Image = Image.FromFile("Resources\\green.png");
#pragma warning disable format
            if (Convert.ToInt16(data[8]) == 1) {pictureBox3.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox12.Text, cancellationToken: CancellationToken.None); }
#pragma warning restore format
            else pictureBox3.Image = Image.FromFile("Resources\\green.png");
#pragma warning disable format
            if (Convert.ToInt16(data[9]) == 1) {pictureBox4.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox11.Text, cancellationToken: CancellationToken.None); }
#pragma warning restore format
            else pictureBox4.Image = Image.FromFile("Resources\\green.png");
#pragma warning disable format
            if (Convert.ToInt16(data[10]) == 1){ pictureBox5.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox10.Text, cancellationToken: CancellationToken.None); }
#pragma warning restore format
            else pictureBox5.Image = Image.FromFile("Resources\\green.png");
#pragma warning disable format
            if (Convert.ToInt16(data[11]) == 1){ pictureBox6.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox9.Text, cancellationToken: CancellationToken.None); }
#pragma warning restore format
            else pictureBox6.Image = Image.FromFile("Resources\\green.png");
#pragma warning disable format
            if (Convert.ToInt16(data[12]) == 1){ pictureBox7.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox8.Text, cancellationToken: CancellationToken.None); }
#pragma warning restore format
            else pictureBox7.Image = Image.FromFile("Resources\\green.png");
#pragma warning disable format
            if (Convert.ToInt16(data[13]) == 1){ pictureBox8.Image = Image.FromFile("Resources\\red.png"); botClient.SendTextMessageAsync(chatId: id, text: "Сработал сигнал на порту под названием " + textBox7.Text, cancellationToken: CancellationToken.None); }
#pragma warning restore format
            else pictureBox8.Image = Image.FromFile("Resources\\green.png");



            if (Convert.ToInt32(textBox1.Text) < min1.Value)
#pragma warning disable format
            {
              await  botClient.SendTextMessageAsync(
                    chatId: id,
                    text: "Темпиратура 1 зоны занижена",
                    cancellationToken: CancellationToken.None
                );
            }else if (Convert.ToInt32(textBox1.Text) > max1.Value)
#pragma warning disable format
                {
              await  botClient.SendTextMessageAsync(
                    chatId: id,
                    text: "Темпиратура 1 зоны завышена",
                    cancellationToken: CancellationToken.None
                );
            }
#pragma warning restore format
#pragma warning restore format

            if (Convert.ToInt32(textBox2.Text) < min2.Value)
            {
                await botClient.SendTextMessageAsync(
                    chatId: id,
                    text: "Темпиратура 2 зоны занижена",
                    cancellationToken: CancellationToken.None
                );
            }
            else if (Convert.ToInt32(textBox2.Text) > max2.Value)
            {
                await botClient.SendTextMessageAsync(
                    chatId: id,
                    text: "Темпиратура 2 зоны завышена",
                    cancellationToken: CancellationToken.None
                );
            }

            if (Convert.ToInt32(textBox3.Text) < min3.Value)
            {
                await botClient.SendTextMessageAsync(
                    chatId: id,
                    text: "Темпиратура 3 зоны занижена",
                    cancellationToken: CancellationToken.None
                );
            }
            else if (Convert.ToInt32(textBox3.Text) > max3.Value)
            {
                await botClient.SendTextMessageAsync(
                    chatId: id,
                    text: "Темпиратура 3 зоны завышена",
                    cancellationToken: CancellationToken.None
                );
            }
            DateTime dt = DateTime.Now;

            chart1.Series[0].Points.AddXY(dt, Convert.ToDouble(data[0]));
            chart1.Series[1].Points.AddXY(dt, Convert.ToDouble(data[5]));
            chart1.Series[2].Points.AddXY(dt, Convert.ToDouble(data[1]));
            chart1.Series[3].Points.AddXY(dt, Convert.ToDouble(data[4]));
            chart1.Series[4].Points.AddXY(dt, Convert.ToDouble(data[2]));
            chart1.Series[5].Points.AddXY(dt, Convert.ToDouble(data[3]));
            int max = Math.Max(Convert.ToInt32(textBox1.Text), Math.Max(Convert.ToSByte(textBox2.Text), Convert.ToSByte(textBox6.Text)));
            int min = Math.Min(Convert.ToInt32(textBox1.Text), Math.Min(Convert.ToSByte(textBox2.Text), Convert.ToSByte(textBox6.Text)));

            if ((max - min) > 5 & !perem)
            {
                Log.Text += DateTime.Now.ToShortTimeString().ToString() + " Включен вентилятор перемешиватель воздуха\n";
                perem = true;
            }
            else
            {
                Log.Text += DateTime.Now.ToString() + "Выключен вентилятор перемешиватель воздуха\n";
                perem = false;
            }
            if (min < 20 & !nag)
            {
                Log.Text += DateTime.Now.ToString() + "Включен нагрев\n";
                nag = true;
            }
            else
            {
                Log.Text += DateTime.Now.ToString() + "Выключен нагрев\n";
                nag = !true;
            }
            if (Convert.ToInt32(textBox5.Text) < 60 & !l)
            {
                Log.Text += DateTime.Now.ToString() + "Включено освещение\n";
                l = true;
            }
            else
            {
                Log.Text += DateTime.Now.ToString() + "Выключено освещение\n";
                l = !true;
            }
            if (32 < max & !n)
            {
                Log.Text += DateTime.Now.ToString() + "Включен наружный вентилятор\n";
                n = true;
            }
            else
            {
                Log.Text += DateTime.Now.ToString() + "Выключен наружный вентилятор\n";
                n = !true;
            }
            return Task.CompletedTask;
        }*/

        private Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)

        {
            return Task.CompletedTask;

        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
#pragma warning disable format
            

            return Task.CompletedTask;
#pragma warning restore format
        }

        public PictureBox Pikcher(string name)
        {
            switch (name)
            {
                case "textBox14":
                    return pictureBox1;
                case "textBox13": return pictureBox2;
                case "textBox12": return pictureBox3;
                case "textBox11": return pictureBox4;
                case "textBox10": return pictureBox5;
                case "textBox9": return pictureBox6;
                case "textBox8": return pictureBox7;
                case "textBox7": return pictureBox8;
                default: return pictureBox1;
            }



        }

#pragma warning disable format
        

        


        private void Form1_Load(object sender, EventArgs e)
#pragma warning restore format
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void min3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void min2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void min1_ValueChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            SaveTextToFile(textBox14.Text + "\r\n" + textBox13.Text + "\r\n" + textBox12.Text + "\r\n" + textBox11.Text + "\r\n" + textBox10.Text + "\r\n" + textBox9.Text + "\r\n" + textBox8.Text + "\r\n" + textBox7.Text);
        }

        private void SaveTextToFile(string text)
        {
            // Укажите путь к файлу, в который нужно записать текст
            string filePath = @"Resources\\output.txt";

            try
            {
                // Открываем файл и записываем текст
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    writer.WriteLine(text);
                }

                MessageBox.Show("Текст успешно сохранен в файл.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении текста: " + ex.Message);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            textBox14.Enabled = true;
            textBox13.Enabled = true;
            textBox12.Enabled = true;
            textBox11.Enabled = true;
            textBox10.Enabled = true;
            textBox9.Enabled = true;
            textBox8.Enabled = true;
            textBox7.Enabled = true;
        }

        private void textBox14_Leave(object sender, EventArgs e)
        {
            //#pragma warning disable format
            if (((TextBox)sender).Text == "\r\r" || ((TextBox)sender).Text == "")
            {
                Pikcher(((TextBox)sender).Name).Image = Image.FromFile("Resources\\null.png");
            }
            else
            {
                Pikcher(((TextBox)sender).Name).Image = Image.FromFile("Resources\\green.png");
            }
#pragma warning restore format
        }

        private void image(TextBox T)
        {
            if (T.Text == "\r\r")
            {
                Pikcher(T.Name).Image = Image.FromFile("Resources\\null.png");
            }
            else
            {
                Pikcher(T.Name).Image = Image.FromFile("Resources\\green.png");
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
#pragma warning disable format
        {
            string user = toolStripComboBox1.SelectedItem.ToString();
            if (user == "каждый час")
            {
                string text = "🌡 Темпиратура верхниго датчика = " + textBox1.Text + "°С\n🌡 Темпиратура нижнего датчика = " + textBox2.Text + "°С\n🌡 Темпиратура средного датчика = " + textBox6.Text + "°С";
                text = text + "\nУровень co2  = " + textBox4.Text;
                text = text + "\nУровень освещёности  = " + textBox5.Text;
                text = text + "\n☁️ Влажность = " + textBox3.Text + "%";
                botClient.SendTextMessageAsync(
                        chatId: id,
                        text: text,
                        cancellationToken: CancellationToken.None
                    );
            }
            else if (user == "4 раза в день" & DateTime.Now.ToShortDateString() == MISHa)
            {
                MISHa = DateTime.Now.AddHours(6).ToShortDateString();
                string text = "🌡 Темпиратура верхниго датчика = " + textBox1.Text + "°С\n🌡 Темпиратура нижнего датчика = " + textBox2.Text + "°С\n🌡 Темпиратура средного датчика = " + textBox6.Text + "°С";
                text = text + "\nУровень co2  = " + textBox4.Text;
                text = text + "\nУровень освещёности  = " + textBox5.Text;
                text = text + "\n☁️ Влажность = " + textBox3.Text + "%";
                botClient.SendTextMessageAsync(
                        chatId: id,
                        text: text,
                        cancellationToken: CancellationToken.None
                    );
            }
            else if (user == "2 раза в день" & DateTime.Now.ToShortDateString() == MISHa)
            {
                MISHa = DateTime.Now.AddHours(12).ToShortDateString();
                string text = "🌡 Темпиратура верхниго датчика = " + textBox1.Text + "°С\n🌡 Темпиратура нижнего датчика = " + textBox2.Text + "°С\n🌡 Темпиратура средного датчика = " + textBox6.Text + "°С";
                text = text + "\nУровень co2  = " + textBox4.Text;
                text = text + "\nУровень освещёности  = " + textBox5.Text;
                text = text + "\n☁️ Влажность = " + textBox3.Text + "%";
                botClient.SendTextMessageAsync(
                        chatId: id,
                        text: text,
                        cancellationToken: CancellationToken.None
                    );
            }
            else if (user == "1 раз в день" & DateTime.Now.ToShortDateString() == MISHa)
            {
                MISHa = DateTime.Now.AddHours(24).ToShortDateString();
                string text = "🌡 Темпиратура верхниго датчика = " + textBox1.Text + "°С\n🌡 Темпиратура нижнего датчика = " + textBox2.Text + "°С\n🌡 Темпиратура средного датчика = " + textBox6.Text + "°С";
                text = text + "\nУровень co2  = " + textBox4.Text;
                text = text + "\nУровень освещёности  = " + textBox5.Text;
                text = text + "\n☁️ Влажность = " + textBox3.Text + "%";
                botClient.SendTextMessageAsync(
                        chatId: id,
                        text: text,
                        cancellationToken: CancellationToken.None
                    );
            }

                }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }
#pragma warning restore format
    }
}
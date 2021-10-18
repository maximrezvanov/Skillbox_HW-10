using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace HW_10
{
    class TelegramMessageClient
    {
        private static MainWindow window;
        public ObservableCollection<MessageLog> BotMessageLog { get; set; }
        private static TelegramBotClient botClient;
        private static string token = "2045254140:AAG5R_d9MnWQxF3xwF0ZSDfUvLUA_0f7YDk";
        private static string fPath = "files";
        private static List<string> filesName;
        private static string msg;

        private static ReplyKeyboardMarkup rkm = new ReplyKeyboardMarkup();
        private static List<KeyboardButton[]> rows = new List<KeyboardButton[]>();
        private static List<KeyboardButton> cols = new List<KeyboardButton>();

      /* static void Main(string[] args)
        {
            #region testProxy

            // var proxy = new WebProxy()
            // {
            //     Address = new Uri($"Http://185.142.174.46:3629"),
            //     UseDefaultCredentials = false
            //      
            // };
            // var httpClientHandler = new HttpClientHandler() {Proxy = proxy};
            // HttpClient hc = new HttpClient(httpClientHandler);
            //
            // botClient = new TelegramBotClient(token, hc);
            //

            #endregion

            CreateDir();

            

            GetBotStatus();
            Console.ReadLine();
        }
      */

        private async void MessageListener(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: user: {e.Message.Chat.FirstName}, " +
                              $"chat: {e.Message.Chat.Id}, message: {e.Message.Text}, type {e.Message.Type.ToString()}");
            GetFileName();

            switch (e.Message.Type)
            {
                case MessageType.Document:
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "document received and saved");
                    Console.WriteLine($"File name {e.Message.Document.FileName}");
                    Console.WriteLine($"File size {e.Message.Document.FileSize}");
                    Console.WriteLine($"File Id {e.Message.Document.FileId}");
                    Download(e.Message.Document.FileId, e.Message.Document.FileName);
                    break;
                case MessageType.Photo:
                    await botClient.SendTextMessageAsync(e.Message.Chat, "photo received and saved");
                    foreach (var pic in e.Message.Photo)
                    {
                        Console.WriteLine($"File id: {pic.FileId}");
                        Console.WriteLine($"File size: {pic.FileSize}");
                        Console.WriteLine($"Width: {pic.Width}");
                        Console.WriteLine($"Height: {pic.Height}\n");
                    }

                    Download(e.Message.Photo[2].FileId, $"{e.Message.Photo[2].FileId}.jpg");
                    break;
                case MessageType.Sticker:
                    await botClient.SendTextMessageAsync(e.Message.Chat, "sticker received");
                    Console.WriteLine($"ID {e.Message.Sticker.FileId}");
                    await botClient.SendStickerAsync(e.Message.Chat,
                        "https://tlgrm.ru/_/stickers/697/ba1/697ba160-9c77-3b1a-9d97-86a9ce75ff4d/192/78.webp");
                    break;
                case MessageType.Audio:
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "sound received and saved");
                    Console.WriteLine($"File name {e.Message.Audio.FileName}");
                    Console.WriteLine($"File size {e.Message.Audio.FileSize}");
                    Console.WriteLine($"File Id {e.Message.Audio.FileId}");
                    Download(e.Message.Audio.FileId, e.Message.Audio.FileName);
                    break;
            }

            if (e.Message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, $"Hi, {e.Message.Chat.FirstName}");
            }

            if (e.Message.Type == MessageType.Text && e.Message.Text != "/start" && e.Message.Text != "/load" &&
                e.Message.Text == "smile")
            {
                msg = e.Message.Text;
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, $"text received");
            }

            if (e.Message.Text == "/load")
            {
                cols.Clear();
                rows.Clear();
                
                for (var ind = 0; ind < filesName.Count; ind++)
                {
                    var newButton = new KeyboardButton(filesName[ind]);
                    cols.Add(newButton);
                    if (ind % 3 != 0) continue;
                    rows.Add(cols.ToArray());
                    cols = new List<KeyboardButton>();
                }

                rkm.Keyboard = rows.ToArray();
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Choose", replyMarkup: rkm);
                Console.WriteLine("filesName.Count" + filesName.Count);
            }

            if (e.Message.Text == "smile")
            {
                await botClient.SendStickerAsync(e.Message.Chat,
                    "https://tlgrm.ru/_/stickers/cbe/e09/cbee092b-2911-4290-b015-f8eb4f6c7ec4/11.webp");
            }

            if (filesName.Contains(e.Message.Text) && e.Message.Text != "/start" && e.Message.Text != "/load")
            {
                await using (Stream stream = File.OpenRead($"{fPath}/{e.Message.Text}"))
                {
                    await botClient.SendDocumentAsync(
                        chatId: e.Message.Chat.Id,
                        document: new InputOnlineFile(content: stream, fileName: e.Message.Text)
                    );
                }
            }

            window.Dispatcher.Invoke(() =>
            {
                BotMessageLog.Add(new MessageLog(DateTime.Now.ToLongTimeString(),
                    e.Message.Chat.FirstName, e.Message.Chat.Id, e.Message.Text));
            });
        }

        private static async void Download(string fileId, string filePath)
        {
            var file = await botClient.GetFileAsync(fileId);
            using (FileStream fs = new FileStream($"{fPath}/{filePath}", FileMode.Create))
            {
                await botClient.DownloadFileAsync(file.FilePath, fs);
            }
        }

        private static void GetFileName()
        {
            if (Directory.Exists(fPath))
            {
                filesName = Directory.GetFiles(fPath).ToList();

                for (int i = 0; i < filesName.Count; i++)
                {
                    filesName[i] = filesName[i].Remove(0, 6);
                    if (filesName.Where(x => x == filesName[i]).Count() > 1)
                    {
                        filesName.Remove(filesName[i]);
                    }
                }
            }
        }

        private static void CreateDir()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(fPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        public TelegramMessageClient(MainWindow w, 
            string token = "2045254140:AAG5R_d9MnWQxF3xwF0ZSDfUvLUA_0f7YDk")
        {
            CreateDir();

            BotMessageLog = new ObservableCollection<MessageLog>();
            window = w;
            botClient = new TelegramBotClient(token);
            botClient.OnMessage += MessageListener;
            botClient.StartReceiving();
        }

        public void SendMessage(string text, string Id)
        {
            long id = Convert.ToInt64(Id);
            botClient.SendTextMessageAsync(id, text);
            Console.WriteLine($"Count :{BotMessageLog.Count}");
        }

        public void SaveMessages()
        {
            string json = JsonConvert.SerializeObject(BotMessageLog);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "data";                 
            sfd.DefaultExt = ".json"; 

            if (sfd.ShowDialog() == true)
            {
                string filename = sfd.FileName;

                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.WriteLine(json);
                }
            }
        }

    }
}
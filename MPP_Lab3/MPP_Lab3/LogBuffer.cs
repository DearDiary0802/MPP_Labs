using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MPP_Lab3
{
    public enum TMessageType { Warning, Error, Fatal, Info }
    struct Message
    {
        public string text;
        public TMessageType messageType;
        public DateTime time;

        public override string ToString()
        {
            return time + " Type of message: " + messageType.ToString() + " Message: " + text + "\n";
        }
    }
    public class LogBuffer
    {
        private const int BUFFER_CAPACITY = 2;
        private const string FILE_PATH = "messages.txt";
        private const int TIMER_LIMIT = 3000;

        private object sync = new object();
        private Timer timer;

        private List<Message> messages = new List<Message>();

        public LogBuffer()
        {
            timer = new Timer((_) => { AppendFile(); }, null, TIMER_LIMIT, TIMER_LIMIT);
        }
        public void Add(string str, TMessageType type, DateTime time) 
        {
            Message message = new Message();
            message.text = str;
            message.messageType = type;
            message.time = time;

            messages.Add(message);

            if (messages.Count >= BUFFER_CAPACITY)
            {
                Thread thread = new Thread(AppendFile);
                thread.Start();
            }
        }
        private void AppendFile()
        {
            lock (sync)
            {
                messages.ForEach((Message message) =>
                {
                    File.AppendAllText(FILE_PATH, message.ToString());
                });
                messages.Clear();
            }
        }
    }
}

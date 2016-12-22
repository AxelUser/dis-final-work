﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Utils;

namespace TaskManager.Service.Notification.Handler
{
    public class ReportServiceServer
    {
        TcpListener listener;
        CancellationTokenSource cts;
        ReportHandler handler;

        public ReportServiceServer(int port, string smtpLogin, string smtpPassword)
        {
            cts = new CancellationTokenSource();
            IPAddress localhost = IPAddress.Parse("127.0.0.1");
            listener = new TcpListener(localhost, port);            
            EmailSender sender = new EmailSender(smtpLogin, smtpPassword);
            handler = new ReportHandler(sender);
            
        }

        public void Start()
        {
            listener.Start();
            Listening(cts.Token, listener);
        }

        public void Stop()
        {
            cts.Cancel();
            handler.StopSendingTasks();
            listener.Stop();
        }

        private void Listening(CancellationToken ct, TcpListener listener)
        {
            while (!ct.IsCancellationRequested)
            {
                var getClientTask = listener.AcceptTcpClientAsync();
                try
                {
                    getClientTask.Wait(ct);
                }
                catch
                {

                }
                if (getClientTask.IsCompleted)
                {
                    var client = getClientTask.Result;
                    HandleClient(ct, client);
                }
            }
        }

        private void HandleClient(CancellationToken ct, TcpClient client)
        {
            if (!ct.IsCancellationRequested)
            {
                using (var stream = client.GetStream())
                {
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, true))
                    {
                        var readIntTask = Task.Run(() => reader.ReadInt32());
                        try
                        {
                            readIntTask.Wait(ct);
                        }
                        catch
                        {

                        }
                        if (readIntTask.IsCompleted && !readIntTask.IsFaulted)
                        {
                            int projectId = readIntTask.Result;
                            handler.RunSending(projectId);
                        }
                    }
                }
            }         
        }
    }
}

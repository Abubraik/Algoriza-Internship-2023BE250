﻿namespace Vezeeta.Sevices.Services.Interfaces
{
    public interface IMailService
    {
        void SendEmail(string type, string? username = "",
            string? password = "", string? token = "", string? link = "");
    }
}

﻿namespace FreeEducation.Web.Services.Interfaces;

public interface IClientCredentialTokenService
{
    Task<string> GetToken();
}
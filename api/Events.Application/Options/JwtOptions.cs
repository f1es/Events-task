﻿namespace Events.Application.Options;

public class JwtOptions
{
    public string Key { get; set; }
    public int ExpiresHours { get; set; }
}
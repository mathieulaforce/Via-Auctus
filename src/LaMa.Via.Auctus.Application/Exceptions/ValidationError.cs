﻿namespace LaMa.Via.Auctus.Application.Exceptions;

public record ValidationError(string PropertyName, string ErrorMessage);
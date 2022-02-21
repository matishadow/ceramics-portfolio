#!/usr/bin/env bash
dotnet tool restore
dotnet tool run KontentModelGenerator -p "eeb1558e-cd0b-01c5-3d76-947f5026930a" -o "./Models" -n "CeramicsPortfolio.Blazor.Models" -g true
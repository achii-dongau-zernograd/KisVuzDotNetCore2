#!/bin/bash
echo "Stopping kestrel-kisvuz.service..."
systemctl stop kestrel-kisvuz.service
echo "STOPPED"
echo "Git pulling..."
git pull
echo "Git pulled"
echo "Publishing ASP .NET Core MVC project"
dotnet publish
echo "Published"

#echo "Dotnet EF Database Updating..."
#dotnet ef database update --project KisVuzDotNetCore2
#echo "Updated"

echo "Starting kestrel-kisvuz.service..."
systemctl start kestrel-kisvuz.service
echo "STARTED"
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

cd ReportGenerator

ReportGenerator.exe -reports:"F:\GitHub\Net\ProyectoSoftware\PreferencesMicroService\UnitTest\coverage.opencover.xml" -targetdir:"F:\GitHub\Net\ProyectoSoftware\PreferencesMicroService\UnitTest\Reports"

cd..

cd Reports

index.html

pause
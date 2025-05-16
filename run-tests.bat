@echo off
echo Rodando testes antes de iniciar a aplicação...
dotnet test "CDBCalculator.Tests\CDBCalculator.Tests.csproj"

if %errorlevel% neq 0 (
    echo !!! TESTES FALHARAM - A APLICAÇÃO NÃO SERÁ INICIADA !!!
    pause
    exit /b %errorlevel%
)

echo Testes passaram! Iniciando a aplicação principal...
pause
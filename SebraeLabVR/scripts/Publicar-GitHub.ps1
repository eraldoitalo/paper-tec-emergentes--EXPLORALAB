param(
    [string]$Repository = 'sebraelab-unity-vr',
    [switch]$Public
)
$ErrorActionPreference = 'Stop'
function Invoke-Checked {
    param([string]$Program, [string[]]$Arguments)
    & $Program @Arguments
    if ($LASTEXITCODE -ne 0) { throw "$Program falhou (código $LASTEXITCODE)." }
}
Get-Command git, gh -ErrorAction Stop | Out-Null
Push-Location (Split-Path $PSScriptRoot -Parent)
try {
    Invoke-Checked 'gh' @('auth', 'status')
    if (!(Test-Path -LiteralPath '.git')) { Invoke-Checked 'git' @('init', '-b', 'main') }
    Invoke-Checked 'git' @('add', '--', '.')
    & git diff --cached --quiet
    if ($LASTEXITCODE -eq 1) {
        Invoke-Checked 'git' @('commit', '-m', 'Estrutura inicial Unity VR baseada na aula')
    } elseif ($LASTEXITCODE -ne 0) { throw 'Falha ao verificar alterações.' }
    $remotes = & git remote
    if ($LASTEXITCODE -ne 0) { throw 'Falha ao consultar remotes.' }
    if ($remotes -contains 'origin') { throw 'origin já existe. Confira o destino antes de publicar novamente.' }
    $visibility = if ($Public) { '--public' } else { '--private' }
    Invoke-Checked 'gh' @('repo', 'create', $Repository, $visibility, '--source=.', '--remote=origin', '--push')
} finally { Pop-Location }
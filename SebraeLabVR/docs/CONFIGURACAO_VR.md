# Configuração do dispositivo

O headset não foi especificado. O projeto fornece pacotes e rig; o loader OpenXR e perfis precisam ser configurados no Editor para o dispositivo real.

1. Em Project Settings > XR Plug-in Management, selecione a aba da plataforma e habilite OpenXR e Initialize XR on Startup.
2. Em OpenXR > Interaction Profiles, adicione o perfil correspondente ao controle (por exemplo Oculus Touch para hardware compatível). Rode Project Validation e corrija as pendências apresentadas pelo pacote.
3. Em Player, ative Input System Package (New), Linear Color Space e configure identificação da aplicação.
4. Em Editor Settings, use Version Control = Visible Meta Files e Asset Serialization = Force Text.
5. Gere a cena pelo menu SebraeLab e salve as configurações no Git.

## PC VR

Instale o módulo Windows Build Support se necessário, selecione Windows no Build Settings e mantenha o runtime OpenXR do headset ativo. Conecte o dispositivo e valide Play antes de gerar o executável.

## Headset Android autônomo

Instale Android Build Support, SDK/NDK e OpenJDK pelo Unity Hub. Troque a plataforma para Android. Configure OpenXR também na aba Android, o perfil e as features exigidas pelo fabricante, scripting backend IL2CPP, arquitetura ARM64 e package name próprio. Ajuste API e gráficos conforme Project Validation e requisitos do dispositivo. Habilite modo desenvolvedor/depuração USB e use Build And Run.

Não há APK gerado nem garantia de configuração para um modelo específico sem teste no hardware. A demonstração usa origem Floor e não possui locomoção: os cubos ficam à frente do usuário.
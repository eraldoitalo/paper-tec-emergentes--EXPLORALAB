# SebraeLab VR

Projeto base 3D de realidade virtual para a disciplina Tecnologias Emergentes, baseado no PDF “Aula Basica Unity”.

## Abrir

1. Instale Unity **2022.3.62f1 LTS** pelo Unity Hub. Essa versão é uma escolha deste projeto; o slide não fixa versão.
2. No Hub, use Add project from disk e selecione esta pasta (contém Assets, Packages e ProjectSettings).
3. Aguarde a resolução dos pacotes. Ative **Input System Package (New)** em Edit > Project Settings > Player > Active Input Handling e reinicie se solicitado.
4. Execute **SebraeLab > Criar cena demonstrativa**. A ferramenta salva Main.unity, o prefab de cubo, material do ponteiro e registra a cena no build. Não substitui Main.unity existente.
5. Configure o dispositivo conforme [docs/CONFIGURACAO_VR.md](docs/CONFIGURACAO_VR.md).
6. Com headset e runtime OpenXR ativos, pressione Play. Aponte um controle para um cubo e pressione o gatilho para alternar sua cor.

A cena e os prefabs são gerados no Editor no primeiro uso; ainda não estão gravados neste scaffold. Sem headset, a câmera permite inspecionar a cena no Game, mas não há navegação por teclado ou simulação dos controles implementada.

## Estrutura

```text
Assets/_Project/
  Audio/          sons
  Editor/         gerador de cena
  Materials/      materiais
  Models/         modelos 3D
  Prefabs/        objetos reutilizáveis
  Scenes/         cenas
  Scripts/
    Core/         lógica geral
    Interaction/  alternância de cor
    XR/           interação por controle
  Settings/       configurações de assets
  UI/             interface
Assets/ThirdParty/ assets importados
Packages/          dependências
ProjectSettings/   versão; demais configurações geradas pelo Editor
docs/              preparação e verificação
scripts/           publicação no GitHub
```

## Conteúdo da aula

A base cobre Scene, GameObject, Transform, hierarquia pai/filho, plano, dois cubos, prefab, Directional Light e câmera. A extensão VR adiciona XR Origin, rastreamento de cabeça, rastreamento dos controles e input de gatilho via OpenXR/Input System. XR Interaction Toolkit fica disponível para evolução com grab e teleporte, que ainda não foram implementados.

O slide cita AR Foundation, ARCore, ARKit e blockchain como contexto; eles não são requisitos desta base VR. Consulte [docs/VALIDACAO.md](docs/VALIDACAO.md).

## Assets SebraeLab

O pacote original está na pasta acima: SebraeLabUnityAssets.unitypackage.gz. Ele foi preservado e não é enviado ao repositório por padrão. Para utilizá-lo, faça uma cópia com extensão .unitypackage e importe em Assets > Import Package > Custom Package. Escolha os itens necessários e confira os direitos de uso antes de redistribuir. A cena de exemplo funciona com primitivas, sem depender do pacote.

## GitHub

Execute scripts/Publicar-GitHub.ps1 com Git e GitHub CLI instalados. O script cria um repositório privado por padrão e envia apenas esta pasta. Não inclui o PDF nem o pacote externo. Arquivos .meta devem ser versionados; Library e builds são ignorados. Nenhuma licença de redistribuição dos assets é presumida.

## Referências

- Material local: Tecnologias Emergentes - Aula Basica Unity.pdf, Kassio Sousa.
- https://docs.unity3d.com/Packages/com.unity.xr.openxr@1.14/manual/index.html
- https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.6/manual/index.html
- https://docs.unity3d.com/Packages/com.unity.xr.management@4.5/manual/index.html
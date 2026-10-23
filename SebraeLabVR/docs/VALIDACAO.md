# Verificação manual no Unity

- Abra com a versão indicada e confirme que o Package Manager resolve todas as dependências.
- Confirme Console sem erros de compilação.
- Gere a cena: verifique Chao, Cubos com duas instâncias do prefab, LuzDirecional e XR Origin.
- Verifique escala em metros, câmera fora dos cubos e chão em Y=0.
- Confirme Main em Scenes In Build.
- Em Play com headset, mova a cabeça: posição e rotação devem acompanhar o usuário.
- Mova os dois controles e confira os raios. Aponte para um cubo e acione o gatilho duas vezes: a cor deve alternar entre ciano e branco.
- Confirme altura no dispositivo usando tracking Floor e execute Project Validation antes do build.
- Abra novamente a cena salva e confirme que a configuração dos dois controles foi preservada.

## Limites da entrega

Esta é uma estrutura inicial com scripts e gerador de cena, não uma aplicação VR validada em hardware. Unity não está instalado no ambiente de preparação; compilação, resolução dos pacotes, execução e build precisam ser verificados no Editor. O headset e a plataforma final permanecem em aberto.
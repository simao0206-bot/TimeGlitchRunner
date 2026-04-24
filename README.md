# TimeGlitchRunner
Tentative of creating a game with unity

# Time Glitch Runner

## Informação Geral

Unidade Curricular: Tecnologias Multimédia  
Curso:Engenharia Informática  
Instituição: IPVC - ESTG  

Versão Unity: 6000.3.9f1  

Repositório:  
https://github.com/simao0206-bot/TimeGlitchRunner  

Elementos do grupo:
- Simão Gigante - nº 33403   

---

## Descrição do Projeto

Time Glitch Runner é um jogo 3D do género *endless runner*, desenvolvido em Unity, onde o jogador se desloca continuamente ao longo do eixo Z, atravessando diferentes épocas temporais.

O jogo integra um sistema de “glitch temporal” que altera dinamicamente o ambiente, os obstáculos, a velocidade e a estética do jogo, proporcionando uma experiência variável e progressivamente mais desafiante.

O projeto cumpre os requisitos do tema *Endless Runner*, incluindo movimento contínuo, colisões, pontuação, Game Over e reinício.

---

## Jogabilidade

O jogador move-se automaticamente para a frente e deve:

- Evitar obstáculos
- Saltar ou agachar
- Recolher cristais
- Sobreviver o máximo de tempo possível

A pontuação aumenta ao longo do tempo e com a recolha de colecionáveis.

---

## Modos de Jogo

### 🔹 Modo Cronológico
As épocas seguem uma sequência fixa:
- Pré-História → Medieval → Moderno → Futuro

### 🔹 Modo Anomalia
As épocas são escolhidas aleatoriamente após cada glitch temporal, criando imprevisibilidade.

---

## Sistema de Épocas

O jogo inclui quatro épocas distintas:

- **Pré-História** – velocidade reduzida, obstáculos naturais (rochas)
- **Medieval** – aumento de dificuldade e obstáculos variados
- **Moderno** – ambiente urbano com edifícios e passeios
- **Futuro** – visão top-down e ambiente simplificado

Cada época altera:
- Velocidade máxima do jogador
- Frequência de obstáculos
- Materiais e cores
- Nevoeiro (fog)
- Comportamento da câmara

---

## Controlos

| Ação        | Teclas                   |
|------------|---------------------------|
| Mover       | A / D ou Setas ← →       |
| Saltar      | W / Espaço / ↑           |
| Agachar     | S / Shift / ↓            |
| Reiniciar   | R                        |

---

##  Arquitetura e Scripts

O projeto encontra-se organizado em scripts com responsabilidades bem definidas:

### 🔹 Movimento do Jogador
- `PlayerMovement.cs`
  - Movimento contínuo com Rigidbody
  - Controlo lateral
  - Sistema de salto e agachamento
  - Aumento progressivo de velocidade (FixedUpdate)

---

### 🔹 Sistema de Épocas
- `TimeGlitch.cs`
  - Gestão das transições entre épocas
  - Aplicação de materiais e nevoeiro
  - Alteração de velocidade e dificuldade
  - Modo Cronológico vs Anomalia

---

### 🔹 Obstáculos
- `ObstacleSpawner.cs`
  - Geração procedural de obstáculos
  - Diferentes padrões (normal, largo, duplo)
- `ObstacleCollision.cs`
  - Deteção de colisões com o jogador
- `ObstacleDestroyer.cs`
  - Limpeza de objetos fora do campo de visão

---

### 🔹 Cristais (Colecionáveis)
- `CristalSpawner.cs`
  - Spawning em lanes fixas
  - Sistema de cristais normais e raros
- `Colecionavel.cs`
  - Lógica de recolha e pontuação

---

### 🔹 Ambiente
- `GroundSpawner.cs`
  - Geração contínua do chão
- `CidadeModernaSpawner.cs`
  - Sistema de edifícios no modo moderno
- `BuildingLoopRowIrregular.cs`
  - Reposicionamento de edifícios (loop infinito)
- `DecoradorEpoca.cs`
  - Elementos visuais por época

---

### 🔹 Câmara
- `CameraFollow.cs`
- `FollowPlayerZ.cs`
- `FollowPlayerZComOffset.cs`

Permitem:
- Seguimento do jogador
- Ajustes por época
- Vista top-down no futuro

---

### 🔹 Gestão do Jogo
- `GameManager.cs`
  - Pontuação
  - Estado do jogo (Game Over)
- `MenuManager.cs`
  - Navegação entre cenas
  - Seleção de modo de jogo

---

## Estrutura de Cenas

### 🔹 MenuScene
- Interface inicial
- Botões:
  - Modo Cronológico
  - Modo Anomalia
  - Controlos
  - Sair

---

### 🔹 GameScene
Contém:
- Player com múltiplos modelos (por época)
- Sistema de UI (pontuação, avisos, Game Over)
- Spawners:
  - Obstáculos
  - Cristais
  - Chão
- Sistema de épocas (`TimeGlitch`)
- Ambiente dinâmico

---

## Como Executar o Projeto

1. Abrir o Unity Hub  
2. Adicionar o projeto  
3. Abrir com **Unity 6000.3.9f1**  
4. Abrir a cena: Assets/Scenes/MenuScene
5. Carregar em **Play**

---

## Assets Multimédia

Foram utilizados:

- Assets urbanos 
- Modelos 3D de animais
- Materiais personalizados por época
- UI com TextMeshPro

Critérios de escolha:
- Baixo custo computacional
- Coerência visual com cada época
- Facilidade de integração

---

## Limitações

- Algumas colisões podem necessitar ajuste fino
- Visibilidade variável no modo moderno
- Spawning pode gerar situações mais difíceis
- Modelo do futuro simplificado (capsula)

---

## Versão

**v1.0**

Versão final do projeto, identificada com a tag `1.0` no repositório Git.

# Clube da Leitura

## Projeto
Desenvolvido durante o curso Backend da [Academia do Programador](https://www.academiadoprogramador.net) 2025

## Especifica��o do Projeto - Requisitos

### 1. M�dulo de Amigos

**Requisitos Funcionais:**
- O sistema deve permitir a inser��o de novos amigos
- O sistema deve permitir a edi��o de amigos j� cadastrados
- O sistema deve permitir excluir amigos j� cadastrados
- O sistema deve permitir visualizar amigos cadastrados
- O sistema deve permitir visualizar os empr�stimos do amigo.

**Regras de Neg�cio**

Campos obrigat�rios:
- Nome (m�nimo 3 caracteres, m�ximo 100)
- Nome do respons�vel (m�nimo 3 caracteres, m�ximo 100)
- Telefone (formato validado: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX)
- N�o pode haver amigos com o mesmo nome e telefone.
- N�o permitir excluir um amigo caso tenha empr�stimos vinculados

---

### 2. M�dulo de Caixas

**Requisitos Funcionais:**
- O sistema deve permitir cadastrar novas caixas
- O sistema deve permitir editar caixas existentes
- O sistema deve permitir excluir caixas
- O sistema deve permitir visualizar todas as caixas

**Regras de Neg�cio**

Campos obrigat�rios:
- Etiqueta (texto �nico, m�ximo 50 caracteres)
- Cor (sele��o de paleta ou hexadecimal)
- Dias de empr�stimo (n�mero, padr�o 7)
- N�o pode haver etiquetas duplicadas
- N�o permitir excluir uma caixa caso tenha revistas vinculadas
- Cada caixa define o prazo m�ximo para empr�stimo de suas revistas

---

### 3. M�dulo de Revistas

**Requisitos Funcionais:**
- O sistema deve permitir cadastrar novas revistas
- O sistema deve permitir editar revistas existentes
- O sistema deve permitir excluir revistas
- O sistema deve permitir visualizar todas as revistas
- O sistema deve mostrar o status atual (dispon�vel/emprestada/reservada)

**Regras de Neg�cio**

Campos obrigat�rios:
- T�tulo (2-100 caracteres)
- N�mero da edi��o (n�mero positivo)
- Ano de publica��o (data v�lida)
- Caixa (sele��o obrigat�ria)
- N�o pode haver revistas com mesmo t�tulo e edi��o
- Status poss�veis: Dispon�vel / Emprestada / Reservada
- Ao cadastrar, o status padr�o � "Dispon�vel"

---

### 4. M�dulo de Empr�stimos

**Requisitos Funcionais:**
- O sistema deve permitir registrar novos empr�stimos
- O sistema deve permitir registrar devolu��es
- O sistema deve permitir visualizar empr�stimos abertos e fechados

**Regras de Neg�cio**

Campos obrigat�rios:
- Amigo
- Revista (dispon�vel no momento)
- Data empr�stimo (autom�tica)
- Data devolu��o (calculada conforme caixa)
- Status poss�veis: Aberto / Conclu�do / Atrasado
- Cada amigo s� pode ter um empr�stimo ativo por vez
- Empr�stimos atrasados devem ser destacados visualmente
- A data de devolu��o � calculada automaticamente (data empr�stimo + dias da
caixa)

## Requisitos
- .NET SDK (recomendado .NET 8.0 ou superior) para compila��o e execu��o do projeto.

## Como Utilizar

#### Clone o Reposit�rio
```
git clone https://github.com/academiadoprogramador-backend/clube-da-leitura-2025.git
```

#### Navegue at� a pasta raiz da solu��o
```
cd clube-da-leitura-2025
```

#### Restaure as depend�ncias
```
dotnet restore
```

#### Navegue at� a pasta do projeto
```
cd ClubeDaLeitura.ConsoleApp
```

#### Execute o projeto
```
dotnet run
```
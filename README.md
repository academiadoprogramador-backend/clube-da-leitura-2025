# Clube da Leitura

## Projeto
Desenvolvido durante o curso Backend da [Academia do Programador](https://www.academiadoprogramador.net) 2025

## Especificação do Projeto - Requisitos

### 1. Módulo de Amigos

**Requisitos Funcionais:**
- O sistema deve permitir a inserção de novos amigos
- O sistema deve permitir a edição de amigos já cadastrados
- O sistema deve permitir excluir amigos já cadastrados
- O sistema deve permitir visualizar amigos cadastrados
- O sistema deve permitir visualizar os empréstimos do amigo.

**Regras de Negócio**

Campos obrigatórios:
- Nome (mínimo 3 caracteres, máximo 100)
- Nome do responsável (mínimo 3 caracteres, máximo 100)
- Telefone (formato validado: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX)
- Não pode haver amigos com o mesmo nome e telefone.
- Não permitir excluir um amigo caso tenha empréstimos vinculados

---

### 2. Módulo de Caixas

**Requisitos Funcionais:**
- O sistema deve permitir cadastrar novas caixas
- O sistema deve permitir editar caixas existentes
- O sistema deve permitir excluir caixas
- O sistema deve permitir visualizar todas as caixas

**Regras de Negócio**

Campos obrigatórios:
- Etiqueta (texto único, máximo 50 caracteres)
- Cor (seleção de paleta ou hexadecimal)
- Dias de empréstimo (número, padrão 7)
- Não pode haver etiquetas duplicadas
- Não permitir excluir uma caixa caso tenha revistas vinculadas
- Cada caixa define o prazo máximo para empréstimo de suas revistas

---

### 3. Módulo de Revistas

**Requisitos Funcionais:**
- O sistema deve permitir cadastrar novas revistas
- O sistema deve permitir editar revistas existentes
- O sistema deve permitir excluir revistas
- O sistema deve permitir visualizar todas as revistas
- O sistema deve mostrar o status atual (disponível/emprestada/reservada)

**Regras de Negócio**

Campos obrigatórios:
- Título (2-100 caracteres)
- Número da edição (número positivo)
- Ano de publicação (data válida)
- Caixa (seleção obrigatória)
- Não pode haver revistas com mesmo título e edição
- Status possíveis: Disponível / Emprestada / Reservada
- Ao cadastrar, o status padrão é "Disponível"

---

### 4. Módulo de Empréstimos

**Requisitos Funcionais:**
- O sistema deve permitir registrar novos empréstimos
- O sistema deve permitir registrar devoluções
- O sistema deve permitir visualizar empréstimos abertos e fechados

**Regras de Negócio**

Campos obrigatórios:
- Amigo
- Revista (disponível no momento)
- Data empréstimo (automática)
- Data devolução (calculada conforme caixa)
- Status possíveis: Aberto / Concluído / Atrasado
- Cada amigo só pode ter um empréstimo ativo por vez
- Empréstimos atrasados devem ser destacados visualmente
- A data de devolução é calculada automaticamente (data empréstimo + dias da
caixa)

## Requisitos
- .NET SDK (recomendado .NET 8.0 ou superior) para compilação e execução do projeto.

## Como Utilizar

#### Clone o Repositório
```
git clone https://github.com/academiadoprogramador-backend/clube-da-leitura-2025.git
```

#### Navegue até a pasta raiz da solução
```
cd clube-da-leitura-2025
```

#### Restaure as dependências
```
dotnet restore
```

#### Navegue até a pasta do projeto
```
cd ClubeDaLeitura.ConsoleApp
```

#### Execute o projeto
```
dotnet run
```
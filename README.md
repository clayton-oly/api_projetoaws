# API de Gerenciamento de Conteúdo

## Descrição
A API de Gerenciamento de Conteúdo é uma aplicação desenvolvida em .NET que fornece endpoints para realizar operações de cadastro, atualização, remoção e consulta de informações de temas, usuários e postagens. Utiliza o banco de dados PostgreSQL para armazenamento dos dados.

## Recursos Principais
- **Cadastro de Temas:** Permite adicionar novos temas ao sistema.
- **Cadastro de Usuários:** Permite registrar novos usuários no sistema.
- **Cadastro de Postagens:** Permite criar novas postagens associadas a um tema e a um usuário.
- **Atualização de Temas, Usuários e Postagens:** Permite atualizar as informações de temas, usuários e postagens existentes.
- **Remoção de Temas, Usuários e Postagens:** Permite remover temas, usuários e postagens do sistema.
- **Consulta de Temas, Usuários e Postagens:** Fornece endpoints para buscar informações detalhadas de temas, usuários e postagens individualmente ou listar todos os registros cadastrados.

## Documentação da API
Acesse a [documentação interativa da API](https://api-projetoaws.onrender.com/swagger) para explorar os endpoints disponíveis, os modelos de dados utilizados e realizar testes diretamente na interface.

## Tecnologias Utilizadas
- **Plataforma de Desenvolvimento:** .NET
- **Framework Web:** ASP.NET Core
- **Banco de Dados:** PostgreSQL
- **Documentação da API:** Swagger

## Como Utilizar
Para utilizar a API, você pode seguir os seguintes passos:

1. Clone este repositório para sua máquina local.
2. Instale todas as dependências do projeto utilizando o gerenciador de pacotes de sua escolha.
3. Configure a conexão com o banco de dados PostgreSQL.
4. Execute o projeto localmente.
5. Acesse a documentação interativa da API para explorar os endpoints disponíveis e realizar testes.

## Exemplo de Uso
Aqui estão alguns exemplos de uso da API:

```bash
# Criar um novo tema
curl -X POST "https://api-projetoaws.onrender.com/api/temas" -H "Content-Type: application/json" -d '{"nome": "Tecnologia"}'

# Registrar um novo usuário
curl -X POST "https://api-projetoaws.onrender.com/api/usuarios" -H "Content-Type: application/json" -d '{"nome": "Alice", "email": "alice@example.com"}'

# Criar uma nova postagem associada a um tema e a um usuário
curl -X POST "https://api-projetoaws.onrender.com/api/postagens" -H "Content-Type: application/json" -d '{"titulo": "Introdução à programação", "conteudo": "Lorem ipsum dolor sit amet.", "idTema": 1, "idUsuario": 1}'

# Atualizar as informações de um tema existente
curl -X PUT "https://api-projetoaws.onrender.com/api/temas/1" -H "Content-Type: application/json" -d '{"nome": "Ciência da Computação"}'

# Remover um usuário do sistema
curl -X DELETE "https://api-projetoaws.onrender.com/api/usuarios/1"

# Buscar informações de uma postagem específica
curl -X GET "https://api-projetoaws.onrender.com/api/postagens/1"

# Listar todos os temas cadastrados
curl -X GET "https://api-projetoaws.onrender.com/api/temas"

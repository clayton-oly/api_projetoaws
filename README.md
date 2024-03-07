API Blog pessoal
Descrição
A API de Gerenciamento de Conteúdo é uma aplicação desenvolvida em .NET que fornece endpoints para realizar operações de cadastro, atualização, remoção e consulta de informações de temas, usuários e postagens. Utiliza o banco de dados PostgreSQL para armazenamento dos dados.

Recursos Principais
Cadastro de Temas: Permite adicionar novos temas ao sistema.
Cadastro de Usuários: Permite registrar novos usuários no sistema.
Cadastro de Postagens: Permite criar novas postagens associadas a um tema e a um usuário.
Atualização de Temas, Usuários e Postagens: Permite atualizar as informações de temas, usuários e postagens existentes.
Remoção de Temas, Usuários e Postagens: Permite remover temas, usuários e postagens do sistema.
Consulta de Temas, Usuários e Postagens: Fornece endpoints para buscar informações detalhadas de temas, usuários e postagens individualmente ou listar todos os registros cadastrados.
Documentação da API
Acesse a documentação interativa da API para explorar os endpoints disponíveis, os modelos de dados utilizados e realizar testes diretamente na interface.

Tecnologias Utilizadas
Plataforma de Desenvolvimento: .NET
Framework Web: ASP.NET Core
Banco de Dados: PostgreSQL
Documentação da API: Swagger
Endpoints Principais
POST /api/temas: Cria um novo tema no sistema.
POST /api/usuarios: Registra um novo usuário no sistema.
POST /api/postagens: Cria uma nova postagem no sistema associada a um tema e a um usuário.
PUT /api/temas/{id}: Atualiza as informações de um tema existente com base no ID.
PUT /api/usuarios/{id}: Atualiza as informações de um usuário existente com base no ID.
PUT /api/postagens/{id}: Atualiza as informações de uma postagem existente com base no ID.
DELETE /api/temas/{id}: Remove um tema do sistema com base no ID.
DELETE /api/usuarios/{id}: Remove um usuário do sistema com base no ID.
DELETE /api/postagens/{id}: Remove uma postagem do sistema com base no ID.
GET /api/temas/{id}: Retorna as informações de um tema específico com base no ID.
GET /api/usuarios/{id}: Retorna as informações de um usuário específico com base no ID.
GET /api/postagens/{id}: Retorna as informações de uma postagem específica com base no ID.
GET /api/temas: Retorna uma lista de todos os temas cadastrados no sistema.
GET /api/usuarios: Retorna uma lista de todos os usuários cadastrados no sistema.
GET /api/postagens: Retorna uma lista de todas as postagens cadastradas no sistema.

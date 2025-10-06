
🚀 Como rodar localmente com Docker

Pré-requisitos

Docker instalado

Docker Compose instalado

Git para clonar o repositório


> 💡 No Windows/Mac instale o Docker Desktop.

> 💡 No Linux instale o Docker Engine e o plugin docker compose.



# Passos para rodar BackEnd

1. Clone o repositório:

  git clone <url-do-repo>
  cd <nome-da-pasta>


2. Construa e suba os containers:
 
  docker-compose build
  
  docker-compose up


4. Verifique os serviços em execução:

  docker compose ps

5. Isso iniciará automaticamente:

  API em http://localhost:8080

  Banco de dados PostgreSQL em localhost:5432

  Adminer (console SQL opcional) em http://localhost:8081


---

⚠️ Esse é o mínimo necessário para rodar o projeto.
Se alguém não tiver Docker instalado, pode seguir os links acima na parte de pré-requisitos para instalar primeiro.


---

# Passos para rodar Frontend (Angular)

Passos para rodar

1. Acesse o diretório do frontend:

  cd frontend/developer-store

2. Instale as dependências:
  npm install

3. Inicie o servidor local:
  npm start
   
4. Acesse a aplicação:
  http://localhost:4200

Execução Rápida (Resumo)
# Subir backend e banco

  docker-compose build
  
  docker compose up -d

# Rodar frontend
cd frontend/developer-store
npm install
npm start

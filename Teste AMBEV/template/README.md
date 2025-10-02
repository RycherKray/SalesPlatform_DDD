
🚀 Como rodar localmente com Docker

Pré-requisitos

Docker instalado

Docker Compose instalado

Git para clonar o repositório


> 💡 No Windows/Mac instale o Docker Desktop.

> 💡 No Linux instale o Docker Engine e o plugin docker compose.



Passos para rodar

1. Clone o repositório:

git clone <url-do-repo>
cd <nome-da-pasta>


2. Construa e suba os containers:

docker compose up --build


3. Verifique os serviços em execução:

docker compose ps


4. Acesse a aplicação:

API: http://localhost:8080

Swagger (se habilitado): http://localhost:8080/swagger




Parar os serviços

docker compose down


---

⚠️ Esse é o mínimo necessário para rodar o projeto.
Se alguém não tiver Docker instalado, pode seguir os links acima na parte de pré-requisitos para instalar primeiro.


---



## Sobre o Projeto:
O **CashflowAPI** é um projeto criado para oferecer uma maneira prática e eficiente de organizar suas finanças pessoais. Com esta **API**, você pode registrar suas despesas ao longo do mês e consultá-las sempre que necessário, garantindo um controle financeiro mais claro e acessível.

Nosso principal diferencial é a possibilidade de **exportar relatórios** mensais em formatos como **Excel e PDF** a qualquer momento, proporcionando uma análise detalhada de suas finanças. Além disso, oferecemos suporte a dois idiomas, permitindo que você escolha entre **Português do Brasil e Inglês**, tornando a interação com a API ainda mais personalizada e amigável.

O projeto foi desenvolvido com foco em qualidade e robustez  incluindo uma cobertura robusta de testes unitários implementados com o **xUnit**, garantindo a confiabilidade e o funcionamento correto de suas funcionalidades.

Utilizando ferramentas modernas como o **AutoMapper**, para simplificar o mapeamento de entidades, e o **Fluent Validation**, que assegura validações consistentes e eficientes dos dados recebidos nas requisições. A integração com o **Entity Framework Core** como ORM (Mapeamento Objeto-Relacional) permite uma manipulação segura e performática dos dados.

Para completar, o projeto também conta com uma documentação interativa e detalhada, desenvolvida com o **Swagger**, permitindo que os usuários explorem todos os endpoints disponíveis de forma intuitiva. Também adotamos os princípios do **Domain-Driven Design** (DDD), que conferem ao projeto alta legibilidade, escalabilidade e facilidade de manutenção. Tudo isso resulta em um código limpo, modular e preparado para evoluir junto com as suas necessidades.

### Features:
* Registro de despesas.
* Exportação de relatórios em Excel e PDF.
* Testes unitários com xUnit.
* MySQLServer.

### Construído com:
![badge-dot-net]
![badge-windows]
![badge-visual-studio]
![badge-mysql]
![badge-swagger]

### Requisitos

* Visual Studio versão 2022+ ou Visual Studio Code
* Windows 10+ ou Linux/MacOS com [.NET SDK][dot-net-sdk] instalado
* MySql Server

### Instalação

1. Clone o repositório:
    ```sh
    git clone https://github.com/welissonArley/CashFlow.git
    ```

2. Preencha as informações no arquivo `appsettings.Development.json`.

3. Execute a API e aproveite o seu teste.

<!-- Links -->
[dot-net-sdk]: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

<!-- Badges -->
[badge-dot-net]: https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge
[badge-windows]: https://img.shields.io/badge/Windows-0078D4?logo=windows&logoColor=fff&style=for-the-badge
[badge-visual-studio]: https://img.shields.io/badge/Visual%20Studio-5C2D91?logo=visualstudio&logoColor=fff&style=for-the-badge
[badge-mysql]: https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=for-the-badge
[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge

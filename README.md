# Funcionarios-IPG

Desenvolvemos uma aplicação dinâmica em ASP.NET CORE MVC com várias páginas interligadas com o objetivo fazer Gestão das Tarefas dos Funcionários e dos Professores do Instituto Politécnico da Guarda.<br/>Esta aplicação apresentar um design coerente de fácil manipulação dos dados contidos na base de dados da aplicação.<br/>
A aplicação garante a segurança e integridade dos dados introduzidos pelo utilizador evitando ataques tais como o SQL INJECTION.<br/>
A aplicação respeita a confidencialidade dos dados ao garantir que cada utilizador possa apenas ver e manipular os dados para os quais tem autorização.<br/>
Foram realizado vários testes automáticos de forma a garantir que a aplicação vai de encontro aos requisitos esperados.
<br/><br/>
<img src="https://github.com/agostinhopina95/Funcionarios-IPG/blob/master/Projecto%20Final/DiagramaER_Versao1.7.png?raw=true" ></img>
<br/><br/><br/><br/>
<strong>Instalação</strong> no Package Manager Console:
<br/>
<b>PM> </b>Update-Database -Context IPGFuncionariosDbContext<br/>
<b>PM> </b>Update-Database -Context ApplicationDbContext

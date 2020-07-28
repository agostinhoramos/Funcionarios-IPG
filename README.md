# Funcionarios-IPG

We developed a dynamic ASP.NET CORE MVC application with several interconnected pages in order to manage the schedules of the employee and teachers of the Polytechnic Institute of Guarda.<br/>
<br/>
The application should manage the schedules of employees and teachers, displaying for each teacher, theirs own schedule according to the subject he teaches, It's also possible to the teacher schedule the exams, the same thing for the employee, he can recommend a change of schedule, and he can also view the day that he missed.<br/>
<br/>
The admin of system have a maximum permission to make any change into the system, also responsible to make a register of teacher and employee in the system. After register of teacher or employee, they will receive a message of account confirmation.<br/>
<br/><br/>
<img src="https://github.com/agostinhopina95/Funcionarios-IPG/blob/master/Projecto%20Final/DiagramaER_Versao1.7.png?raw=true" ></img>
<br/><br/><br/><br/>
<strong>Instalação</strong> no Package Manager Console:
<br/>
<b>PM> </b>Update-Database -Context IPGFuncionariosDbContext<br/>
<b>PM> </b>Update-Database -Context ApplicationDbContext

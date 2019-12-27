using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models {
    public class SeedData {
        public static void Populate(IPGFuncionariosDbContext db) {

            SeedFuncionario(db);

        
       
            
            if (db.Professor.Any()) {
                return;
            }

            db.Professor.AddRange(
                /* Dados dos professores do ESTG --> http://www.estg.ipg.pt/utc.aspx?id=5 */
                new Professor { Nome = "Noel de Jesus Lopes", Contacto = "234341216", Email = "noel@sal.ipg.pt", Gabinete = "27" },
                new Professor { Nome = "Paulo Jorge Nunes", Contacto = "243654732", Email = "pnunes@sal.ipg.pt", Gabinete = "21" },
                new Professor { Nome = "José Alberto Quitério Figueiredo", Contacto = "235446372", Email = "jfig@ipg.pt", Gabinete = "21" },
                new Professor { Nome = "António Mário Martins", Contacto = "235362735", Email = "amrmartins@ipg.pt", Gabinete = "47" },
                new Professor { Nome = "Beatriz de Jesus Rebelo", Contacto = "234257634", Email = "bjrebelo@ipg.pt", Gabinete = "4" },
                new Professor { Nome = "Carlos Carreto", Contacto = "253729564", Email = "ccarreto@ipg.pt", Gabinete = "17" },
                new Professor { Nome = "Carlos Jorge Gonçalves Brigas", Contacto = "936594630", Email = "brigas@ipg.pt", Gabinete = "33" },
                new Professor { Nome = "Celestino Gonçalves", Contacto = "946925405", Email = "celestin@ipg.pt", Gabinete = "42" },
                new Professor { Nome = "Fernando Rodrigues", Contacto = "225745473", Email = "fmr@ipg.pt", Gabinete = "61" },
                new Professor { Nome = "José Carlos Fonseca", Contacto = "925493548", Email = "josefonseca@ipg.pt", Gabinete = "62" },
                new Professor { Nome = "Maria Clara Silveira", Contacto = "246395630", Email = "mclara@ipg.pt", Gabinete = "63" },
                new Professor { Nome = "Natália Fernandes Gomes", Contacto = "935241743", Email = "ngomes@ipg.pt", Gabinete = "64" },

                /* Dados dos professores do ESTH --> http://www.esth.ipg.pt/utc.aspx?id=1 */
                new Professor { Nome = "Adriano Azevedo Costa", Contacto = "253465284", Email = "outro1@email.com", Gabinete = "65" },
                new Professor { Nome = "Elsa Maria Costa Ventura Ramos", Contacto = "245436650", Email = "outro2@email.com", Gabinete = "66" },
                new Professor { Nome = "Fernando Manuel Florim Ribeiro de Lemos", Contacto = "243654507", Email = "outro3@email.com", Gabinete = "67" },
                new Professor { Nome = "Gonçalo Poeta Fernandes", Contacto = "943676786", Email = "outro4@email.com", Gabinete = "68" },
                new Professor { Nome = "Manuel António Brites Salgado", Contacto = "950768556", Email = "outro5@email.com", Gabinete = "69" },

                /* Dados dos professores do ESS --> http://www.ess.ipg.pt/utc.aspx?id=1 */
                new Professor { Nome = "Abílio Madeira Figueiredo", Contacto = "950754365", Email = "outro6@email.com", Gabinete = "70" },
                new Professor { Nome = "Agostinha Esteves Melo Corte", Contacto = "246505436", Email = "outro7@email.com", Gabinete = "71" },
                new Professor { Nome = "Ana Maria Jorge", Contacto = "246507686", Email = "outro8@email.com", Gabinete = "72" },
                new Professor { Nome = "António Manuel Almeida Tavares Sequeira", Contacto = "254356507", Email = "outro9@email.com", Gabinete = "73" },
                new Professor { Nome = "António Manuel Martins Batista", Contacto = "246507654", Email = "outro10@email.com", Gabinete = "74" },
                new Professor { Nome = "Ermelinda Maria Bernardo Gonçalves Marques", Contacto = "236540768", Email = "outro11@email.com", Gabinete = "75" },
                new Professor { Nome = "Ezequiel Martins Carrondo", Contacto = "930768766", Email = "outro12@email.com", Gabinete = "76" },

                /* Dados dos professores do ESECD --> http://www.esecd.ipg.pt/utc.aspx?id=1 */
                new Professor { Nome = "Carla Helena Henriques C.T. Ravasco Nobre", Contacto = "245465076", Email = "outro13@email.com", Gabinete = "77" },
                new Professor { Nome = "Cristina Rita Ferreira Arala Chaves", Contacto = "246507436", Email = "outro14@email.com", Gabinete = "78" },
                new Professor { Nome = "Maria Helena Teixeira Pinto", Contacto = "207543654", Email = "outro15@email.com", Gabinete = "79" },
                new Professor { Nome = "Maria João Marques Alves da Costa", Contacto = "245076354", Email = "outro16@email.com", Gabinete = "80" },
                new Professor { Nome = "Mário José da Silva Meleiro", Contacto = "246432546", Email = "outro17@email.com", Gabinete = "81" },
                new Professor { Nome = "Rosa Branca Almeida Figueiredo", Contacto = "254076686", Email = "outro18@email.com", Gabinete = "82" },
                new Professor { Nome = "Rui Manuel Formoso Nobre dos Santos", Contacto = "246543686", Email = "outro19@email.com", Gabinete = "83" }
            );

            db.SaveChanges();
        }
        private static void SeedFuncionario(IPGFuncionariosDbContext db)
        {
            if (db.Funcionario.Any()) return;

            db.Funcionario.AddRange(
                new Funcionario { Nome = "Lina Sousa", Telefone = "234567890", Email = "lina@gmail.com", Genero = "F", Morada = "Rua Xanana Gusmão ", DataNascionento="01/09/2010 00:00:00 "},
                new Funcionario { Nome = "Lara Lima", Telefone = "912344567", Email = "lima@gmail.com", Genero = "F", Morada = "Rua Páiva", DataNascionento = " 01/09/2012 00:00:00" },
                new Funcionario { Nome = "João Rita", Telefone = "923456211", Email = "joaorita@gmail.com", Genero = "m", Morada = "Rua da Alegria " , DataNascionento = "01/09/2011 00:00:00" }
                );
            db.SaveChanges();
        }

    }
}

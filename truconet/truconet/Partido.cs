﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace truconet
{
    public class Partido
    {
        public static int num = 0;
        private int id;
        private string descripcion;
        private Carta muestra;
        private int identificador;
        private List<Jugador> participantes= new List<Jugador>();
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private List<Jugador> ganadores = new List<Jugador>();
       // private int[,] puntajeFinal = new int[2, 3]; DA UN ERROR. NO SE ADMITEN MATRICES MULTIDIMENSIONALES
        Random rnd = new Random();
        private List<Carta> masoJuego;
        private List<Carta> maso;

      

        //Constructor

        public Partido(string desc, List<Jugador> pParticipantes, List<Carta> maso)
        {
            this.id = num;
            num = num + 1;
            this.participantes = pParticipantes;
            this.fechaInicio = new DateTime();
            this.descripcion = desc;
            this.maso = new List<Carta>(maso);
            this.masoJuego = new List<Carta>(maso);

        }
        public Partido()
        {

        }

        #region Metodos_accesores

        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        

        //public int[,] PuntajeFinal
        //{
        //    get { return puntajeFinal; }
        //    set { puntajeFinal = value; }
        //}


        public List<Jugador> Ganadores
        {
            get { return ganadores; }
            set { ganadores = value; }
        }

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; }
        }

        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }

        public List<Jugador> Participantes
        {
            get { return participantes; }
            set { participantes = value; }
        }

        public int Identificador
        {
            get { return identificador; }
            set { identificador = value; }
        }

        public int numParticipantes()
        {
            return this.Participantes.Count();
        }

        public Carta Muestra
        {
            get { return muestra; }
            set { muestra = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }

        }

        public List<Carta> MasoJuego
        {
            get { return masoJuego; }
            set { masoJuego = value; }
        }

     
        #endregion



        public void barajar()
        {
            //copio el maso al juego
            this.MasoJuego = new List<Carta>(this.maso);
            foreach (Carta tmpCarta in MasoJuego)
            {
                int tmpRnd = (int)rnd.NextDouble() * MasoJuego.Count();
                tmpCarta.Cod = tmpRnd;
            }
            MasoJuego.Sort(ordenarMaso);
        }


        private int ordenarMaso(Carta c1, Carta c2)
        {
            return c1.Cod.CompareTo(c2.Cod);
        }

        public void repartirCartas()
        {
            barajar();
            //Reparto 3 cartas a cada jugador.
            for (int i = 0; i < 3; i++)
            {
                foreach (Jugador jug in participantes)
                {
                    masoJuego[0].Jugador = jug;
                    jug.agregarCarta(MasoJuego[0]);
                    MasoJuego.RemoveAt(0);
                }
            }
            //Entrego la muestra
            muestra = masoJuego[0];
            MasoJuego.RemoveAt(0);
            this.setTipoCarta(); //Reviso los tipos de carta asignados
        }

        public void setTipoCarta()
        {
            int[] pieza = { 2, 4, 5, 10, 11, 12 };
            foreach (Jugador jug in participantes)
            {
                foreach (Carta card in jug.getCartas())
                {
                    if (card.Palo == muestra.Palo) //si son del mismo palo me fijo el numero
                    {
                        if (pieza.Contains(card.Numero))
                        {
                            card.Categoria = 1; //Es pieza
                            //Seteo Puntaje
                            switch (card.Numero)
                            {
                                case 2:
                                    card.Puntaje = 30;
                                    break;
                                case 4:
                                    card.Puntaje = 29;
                                    break;
                                case 5:
                                    card.Puntaje = 28;
                                    break;
                                case 10:
                                    card.Puntaje = 27;
                                    break;
                                case 11:
                                    card.Puntaje = 27;
                                    break;
                                case 12:
                                    if (pieza.Contains(muestra.Numero))
                                    {
                                        switch (muestra.Numero)
                                        {
                                            case 2:
                                                card.Puntaje = 30;
                                                break;
                                            case 4:
                                                card.Puntaje = 29;
                                                break;
                                            case 5:
                                                card.Puntaje = 28;
                                                break;
                                            case 10:
                                                card.Puntaje = 27;
                                                break;
                                            case 11:
                                                card.Puntaje = 27;
                                                break;
                                        }    
                                    };
                                    break;

                            }
                        }
                    }
                    else //Si no son del mismo palo evaluo si es mata, fio o comun
                    {
                        if ((card.Numero == 7 && card.Palo == 1) || (card.Numero == 7 && card.Palo == 4) || (card.Numero == 1 && card.Palo == 3) || (card.Numero == 1 && card.Palo == 4))
                        {
                            card.Categoria = 2; //Es mata
                        }
                        if (card.Numero == 2 || card.Numero == 3 || card.Numero == 1)
                        {
                            card.Categoria = 3; //Es FIO
                        }
                        if (card.Categoria == 0 || card.Categoria == 0)
                        {
                            card.Categoria = 4; //Es comun
                        }
                    }
                }
            }
        }
        




    }
}

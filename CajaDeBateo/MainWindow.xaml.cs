﻿using CajaDeBateo.ComunicacionArduino;
using CajaDeBateo.ControlDeUsuarios;
using CajaDeBateo.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CajaDeBateo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button[] principal,tarjeta,creditos,agregarCreditos,configuracion;
        ControladorMenu principalC, tarjetaC, creditosC, agregarCreditosC, configuracionC;
        ControladorMenus controlador;

        private void Reset(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.F1)
            {
                ardC.Reset();
                stkUSerControlContainer.Children.Remove(controlDeVista);
                uscPrincipal = new UserControl1();
                controlDeVista = uscPrincipal;
                stkUSerControlContainer.Children.Add(controlDeVista);
            }
                
        }

        private UserControl1 uscPrincipal;
        private Control controlDeVista;
        int puertoSeleccionado;
        string[] puertos;
        ArduinoComunication ardC;
        public MainWindow()
        {
            InitializeComponent();
            InicializaBotones();
            uscPrincipal = new UserControl1();
            controlDeVista = uscPrincipal;
            stkUSerControlContainer.Children.Add(controlDeVista);
            SeleccionaArduino sA = new SeleccionaArduino();
            sA.ShowDialog();
            puertoSeleccionado = sA.Index;
            puertos = sA.EP;
            //ardC.RespuestaRecivida += new EventHandler(Recepcion);
            try
            {
                ardC= new ArduinoComunication(puertoSeleccionado, puertos);
            } catch (SensorNotFoundExceptio e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        //private void Recepcion(object sender, EventArgs e)
        //{
        //    string data = (string)sender;
        //    MessageBox.Show(data);
        //}

        void InicializaBotones()
        {
            principal = new Button[3];
            principal[0] = btnTarjetas;
            principal[1] = btnCreditos;
            principal[2] = btnConfiguracion;
            principalC = new ControladorMenu(ref principal, 3);

            tarjeta = new Button[3];
            tarjeta[0] = btnCrear;
            tarjeta[1] = btnActivar;
            tarjeta[2] = btnDesactivar;
            tarjetaC = new ControladorMenu(ref tarjeta, 3);

            creditos = new Button[4];
            creditos[0] = btnAgregarCreditos;
            creditos[1] = btnVerCreditos;
            creditos[2] = btnVerCreditosCompletos;
            creditos[3] = btnActualizarCreditos;
            creditosC = new ControladorMenu(ref creditos, 4);

            agregarCreditos = new Button[2];
            agregarCreditos[0] = btnAgregarMensual;
            agregarCreditos[1] = btnMasCreditos;
            agregarCreditosC = new ControladorMenu(ref agregarCreditos, 2);

            configuracion = new Button[2];
            configuracion[0] = btnConfigurarCreditosMensuales;
            configuracion[1] = btnAcercaNosotros;
            configuracionC = new ControladorMenu(ref configuracion, 2);

            //Configuracion de eventos
            btnTarjetas.Click += new RoutedEventHandler(btnClick);
            btnCreditos.Click += new RoutedEventHandler(btnClick);
            btnConfiguracion.Click += new RoutedEventHandler(btnClick);
            btnCrear.Click += new RoutedEventHandler(btnClick);
            btnActivar.Click += new RoutedEventHandler(btnClick);
            btnDesactivar.Click += new RoutedEventHandler(btnClick);
            btnAgregarCreditos.Click += new RoutedEventHandler(btnClick);
            btnVerCreditos.Click += new RoutedEventHandler(btnClick);
            btnVerCreditosCompletos.Click += new RoutedEventHandler(btnClick);
            btnActualizarCreditos.Click += new RoutedEventHandler(btnClick);
            btnAgregarMensual.Click += new RoutedEventHandler(btnClick);
            btnMasCreditos.Click += new RoutedEventHandler(btnClick);
            btnRegresar.Click += new RoutedEventHandler(btnClick);
            btnAcercaNosotros.Click += new RoutedEventHandler(btnClick);
            btnConfigurarCreditosMensuales.Click += new RoutedEventHandler(btnClick);
            controlador = new ControladorMenus(ref principalC, ref tarjetaC, ref creditosC, ref agregarCreditosC, ref configuracionC);
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            FrameworkElement feSource = e.Source as FrameworkElement;
            switch(feSource.Name)
            {
                case "btnTarjetas":
                    controlador.setTarjetas();
                break;
                case "btnCreditos":
                    controlador.setCreditos();
                    break;
                case "btnConfiguracion":
                    controlador.setConfiguracion();
                    break;
                case "btnRegresar":
                    controlador.setBack();
                    break;
                case "btnAgregarCreditos":
                    controlador.setAgregarCreditos();
                    break;
                case "btnCrear":
                    stkUSerControlContainer.Children.Remove(controlDeVista);
                    EscribirDebug escribir= new EscribirDebug(ref ardC);
                    controlDeVista = escribir;
                    stkUSerControlContainer.Children.Add(controlDeVista);
                    break;
                case "btnActivar":
                    stkUSerControlContainer.Children.Remove(controlDeVista);
                    Leer leer = new Leer(ref ardC);
                    controlDeVista = leer;
                    stkUSerControlContainer.Children.Add(controlDeVista);
                    break;
            }
        }
    }
}

using System.ServiceModel;

namespace WS_SOAP_DOTNET_GR69.Services
{
    [ServiceContract]
    public interface IConversionService
    {
        [OperationContract]
        bool Login(string usuario, string password);

        [OperationContract] double MetroAPie(double valor);
        [OperationContract] double KilometroAMilla(double valor);
        [OperationContract] double CentimetroAPulgada(double valor);
        [OperationContract] double PulgadaACentimetro(double valor);
        [OperationContract] double PieAMetro(double valor);

        [OperationContract] double KilogramoALibra(double valor);
        [OperationContract] double GramoAOnza(double valor);
        [OperationContract] double ToneladaAKilogramo(double valor);
        [OperationContract] double LibraAKilogramo(double valor);
        [OperationContract] double OnzaAGramo(double valor);

        [OperationContract] double CelsiusAFahrenheit(double valor);
        [OperationContract] double CelsiusAKelvin(double valor);
        [OperationContract] double CelsiusARankine(double valor);
        [OperationContract] double CelsiusAReaumur(double valor);
        [OperationContract] double FahrenheitACelsius(double valor);
    }
}

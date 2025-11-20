class RegistroCivil:
{
__denominacion: str
__domicilio: str
__actas: list
__actaActual = 100
__libroActual = 5
def __init__(self, denominacion, domicilio):
{
self.__denominacion = denominacion
self.__domicilio = domicilio
self.__actas=[]
}
def getActaActual(cls):
{
pass
}
def getLibroActual(cls):
{
pass
}
def inscribirPersona(self, persona, fecha):
{
pass
}
def mostrarActas(self):
{
pass
}
}
class Persona:
{
__dni = 0
__apellido = 'Juan'
def __init__(self, dni, nombre, apellido):
{
self.__dni = dni
self.__nombre = nombre
self.__apellido = apellido
}
}
class ActaNacimiento:
{
__fechaInscripcion: str
__numeroroLibro: int
__numeroActa: int
__persona: Persona
__registrocivil: RegistroCivil
def __init__(self, nroActa, nroLibro, fechaInscripcion, persona, registroCivil):
{
self.__numeroActa = nroActa
self.__numeroLibro = nroLibro
self.__fechaInscripcion = fechaInscripcion
self.__persona = persona
self.__registrocivil = registroCivil
}
}

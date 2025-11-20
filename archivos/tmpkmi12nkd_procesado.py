class Medico:
{
__dni: int
__matricula: int
__especialidad: str
__apellido: str
__nombre: str
__prescripciones: list
def __init__(self, dni, matricula, especialidad, apellido, nombre):
{
self.__dni=dni
self.__matricula=matricula
self.__especialidad=especialidad
self.__apellido=apellido
self.__nombre=nombre
}
def addPrescipcion(self, prescripcion):
{
self.__prescripciones.append(prescripcion)
}
}

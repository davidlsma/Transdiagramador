class Prescripcion:
{
__fecha: str
__diagnostico: str
__medicacion: str
__presentacion: str
__dosis: str
__paciente: object
__medico: object
def __init__(self, fecha, diagnostico, medicacion, presentacion, dosis, medico, paciente):
{
self.__fecha=fecha
self.__diagnostico=diagnostico
self.__medicacion=medicacion
self.__presentacion=presentacion
self.__dosis=dosis
self.__medico=medico
self.__paciente=paciente
self.__medico.addPrescipcion(self)
self.__paciente.addPrescripcion(self)
}
}
class Medico:
{
__dni = 123
__matricula: int
__especialidad: str
__apellido: str
__nombre: str
__prescripciones: list[Prescripcion]
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
class Paciente:
{
__dni: int
__apellido: str
__nombre: str
__prescripciones: list[Prescripcion]
def __init__(self, dni, apellido, nombre):
{
self.__dni=dni
self.__apellido=apellido
self.__nombre=nombre
}
def addPrescripcion(self, prescripcion):
{
self.__prescripciones.append(prescripcion)
}
}

class Persona:
{
__dni: int
__apellido: str
__nombre: str
def __init__(self, dni, apellido, nombre, codigoCargo=0, agrupamiento=0, catedra='', sueldo=0.0, fechaIngreso='', promedio=0.0, carrera=''):
{
self.__dni=dni
self.__apellido=apellido
self.__nombre=nombre
}
}
class Docente(Persona):
{
__codigoCargo: str
__agrupamiento: int
__catedra: str
__sueldo: float
def __init__(self, dni, apellido, nombre, fechaIngreso, promedio, carrera, codigoCargo, agrupamiento, catedra, sueldo):
{
super().__init__(dni, apellido, nombre, fechaIngreso, promedio, carrera, codigoCargo, agrupamiento,catedra, sueldo)
self.__codigoCargo=codigoCargo
self.__agrupamiento=agrupamiento
self.__catedra=catedra
self.__sueldo=sueldo
}
}

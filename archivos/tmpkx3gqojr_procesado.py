class Persona:
{
__dni: int
__apellido: str
__nombre: str
def __init__(self, dni, apellido, nombre):
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
class Alumno(Persona):
{
__fechaIngreso: str
__promedio: float
__carrera: str
def __init__(self, dni, apellido, nombre, fechaIngreso, promedio, carrera, codigoCargo, agrupamiento,catedra, sueldo):
{
super().__init__(dni, apellido, nombre, fechaIngreso, promedio, carrera, codigoCargo, agrupamiento,catedra, sueldo)
self.__fechaIngreso=fechaIngreso
self.__promedio=promedio
self.__carrera=carrera
}
}

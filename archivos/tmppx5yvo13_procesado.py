class Provincia:
{
__nombre: str
__cantidadDeHabitantes: int
__gobernador: Gobernador
def __init__(self, nombre, cantidadDeHabitantes, gobernador):
{
self.__nombre=nombre
self.__cantidadDeHabitantes=cantidadDeHabitantes
self.__gobernador=gobernador
}
}
class Gobernador:
{
__dni: int
__nombreApellido: str
__provincia: Provincia
def __init__(self, dni, nombreApellido, provincia):
{
self.__dni=dni
self.__nombreApellido=nombreApellido
self.__provincia=provincia
}
}

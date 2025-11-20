class Sala:
{
__numeroSala: int
__aforo: int
def __init__(self, numeroSala, aforo):
{
self.__numeroSala=numeroSala
self.__aforo=aforo
}
def mostrarSala(self):
{
pass
}
}
class Cine:
{
__codigo: str
__denominacion: str
__empresaPropietaria: str
__direccion: str
__provincia: str
__salas: list[Sala]
def __init__(self, codigo, denominacion, empresaPropietaria, direccion, provincia):
{
self.__codigo=codigo
self.__denominacion=denominacion
self.__empresaPropietaria=empresaPropietaria
self.__direccion=direccion
self.__provincia=provincia
self.__salas.append(Sala())
}
def mostrarCine(self):
{
pass
}
}
